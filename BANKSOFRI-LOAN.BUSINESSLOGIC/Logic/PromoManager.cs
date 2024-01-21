using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Logic
{
    public class PromoManager : IPromo
    {
        
        IConfiguration _config;
        ILogs _logs;
        string token = null;
        string mfbCode = null;
        string URL = null;
        string promoGL = null;
        ApiauthorisationContext _ctx;
        public PromoManager(ILogs log, ApiauthorisationContext ctx, IConfiguration config)
        {           
            _logs = log;
            _config = config;
            _ctx = ctx;
            URL = _config.GetSection("bankOneAPIbaseurl").Value;
            mfbCode = _config.GetSection("institutionCode").Value;
            token = _config.GetSection("bankOneServiceCalltoken").Value;
            promoGL = _config.GetSection("SofriBonusGL").Value;
        }
        public async Task<ResponseObject> HandlePromotions(PromotionRequestObject prc)
        {
            try
            {
                bool reply = IsUserValid(prc.CustomerId, prc.PromoCode);
                if (reply)
                {
                    SofriPromo s = GetPromoDetails(prc.PromoCode);
                    if (s.Description != null)
                    {
                        if (s.Status == "AVAILABLE")
                        {
                            CustomerDetailsDTO cd = GetByCustomerID(prc.CustomerId);
                            string accountNumber = cd.Accounts.Select(x => x.NUBAN).FirstOrDefault();
                            PromoDisbursedResponse dr = DisburseBonusViaBankOne(prc.CustomerId, s.AmountValue.ToString(), accountNumber, prc.PromoCode, s.PromoOwnerAccountNumber, s.PromoOwnerCustomerId);
                            if (dr.IsSuccessful)
                            {
                                if (s.CodeType == "Multiple")
                                {
                                    int used = int.Parse(s.TotalUsed.ToString());
                                    int allused = used + 1;
                                    int bal = used - allused;

                                    s.Balance = bal;
                                    s.TotalUsed = allused;
                                    s.Status = (bal == 0) ? "USED" : "AVAILABLE";
                                }
                                else
                                {
                                    s.Status = "USED";
                                    s.UsedBy = prc.CustomerId;
                                    s.DateUsed = DateTime.Now.ToString();
                                }
                                long res = await SavePromoUsage(s);
                                await _ctx.PromoCodeUsers.AddAsync(new PromoCodeUser() { Date = DateTime.Now, CustomerId = prc.CustomerId, AccountNumber = accountNumber, PromoCode = prc.PromoCode });
                                await _ctx.SaveChangesAsync();
                                return new ResponseObject() { ResponseCode = "00", ResponseMessage = "CUSTOMER ACCOUNT CREDIT WITH PROMO CODE AMOUNT VALUE" };
                            }
                            else
                            {
                                return new ResponseObject() { ResponseCode = "01", ResponseMessage = "Something went wrong! Details: " + dr.ResponseMessage };
                            }
                        }
                        else
                        {
                            return new ResponseObject() { ResponseCode = "012", ResponseMessage = "THIS PROMO CODE HAS BEEN USED" };
                        }
                    }
                    else
                    {
                        return new ResponseObject() { ResponseCode = "012", ResponseMessage = "This promo Code has either expired or is invalid!" };
                    }
                }
                else
                {
                    return new ResponseObject() { ResponseCode = "014", ResponseMessage = "Your Account has already been credited with this promo Code value!" };
                }
            }
            catch (Exception ex)
            {
                _logs.Logger("An error occurred on HandlePromotions method in PromoManager class! Details: " + ex.Message, "ERROR");
                return new ResponseObject() { ResponseCode = "013", ResponseMessage = "AN ERROR OCCURED! DETAILS: " + ex.Message };
            }
            // return responseMessage;
        }

        public bool IsUserValid(string customerId, string procode)
        {
            PromoCodeUser prouse = new PromoCodeUser();
            bool usage = true;
            try
            {
                //string sql = "SELECT * FROM PromoCodeUsers WHERE CustomerId=@custId AND PromoCode=@code";
                prouse = _ctx.PromoCodeUsers.Where(x => x.CustomerId == customerId && x.PromoCode == procode).FirstOrDefault();               
                if (prouse != null)
                {
                    return false;
                }
               
            }
            catch (Exception ex)
            {                
                _logs.Logger("An error occurred on ValidateUsage method in PromoManager class! Details: " + ex.Message, "ERROR");
            }
            return usage;
        }

        public BonusHistoryResponse GetPromoUsage(string customerId)
        {
            List<PromoCodeUser> prouse = new List<PromoCodeUser>();
            BonusHistoryResponse resp = new BonusHistoryResponse();
            try
            {
               // string sql = "SELECT * FROM PromoCodeUsers WHERE CustomerId=@custId";
                prouse = _ctx.PromoCodeUsers.Where(x => x.CustomerId == customerId).ToList(); //(List<PromoCodeUser>)_repo.QueryList<PromoCodeUser>(sql, new { custId = customerId });
                if (prouse != null)
                {
                    return new BonusHistoryResponse() { ResponseCode = "00", ResponseMessage = "SUCCESS! Promo code usage fetched successfulyy!", History = prouse };
                }
                resp = new BonusHistoryResponse() { ResponseCode = "01", ResponseMessage = "No Promo code usage record found!" };                
            }
            catch (Exception ex)
            {
                resp = new BonusHistoryResponse() { ResponseCode = "02", ResponseMessage = "AN ERROR OCCURED! Detail: "+ex.Message };
                _logs.Logger("An error occurred on GetPromoUsage method in PromoManager class! Details: " + ex.Message, "ERROR");
            }
            return resp;
        }

        PromoDisbursedResponse DisburseBonusViaBankOne(string customerld, string bonus, string acctNum, string promocode, string sourceAccount, string customerId)
        {
            PromoDisbursedResponse resp = new PromoDisbursedResponse();
            string baseurl = _config.GetSection("bankOnethirdPartyAPIbaseurl").Value;
            try
            {
                var client = new RestClient();
                string transref = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
                decimal amt = decimal.Parse(bonus) * 100;               
                string URL = baseurl + "/CoreTransactions/LocalFundsTransfer";
                            
                var request = new RestRequest(URL, Method.POST);
                request.AddHeader("Accept", "application/json");
                request.RequestFormat = DataFormat.Json;
                Random rand = new Random();               
                request.AddJsonBody(new { Amount = amt, Fee = 0, FromAccountNumber = sourceAccount, ToAccountNumber = acctNum, RetrievalReference = transref, Narration = "Account Opening Bonus with promo Code " + promocode, AuthenticationKey = token });
                var response = client.Execute(request);
                resp = JsonConvert.DeserializeObject<PromoDisbursedResponse>(response.Content);
            }
            catch (Exception ex)
            {
                _logs.Logger("An error occured on LoanDisbursementViaBankOne in PromoManager method! Details: " + ex.Message, "Error");
            }
            return resp;
        }

        async Task<long> SavePromoUsage(SofriPromo sp)
        {
            long resp = 0;
            try
            {
                _ctx.Update(sp);
                resp = await _ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logs.Logger("An error occurred on SavePromoUsage method in SofriPromoMgr class! Details: " + ex.Message, "ERROR");
            }
            return resp;
        }

        public SofriPromo GetPromoDetails(string promoCode)
        {
            SofriPromo spmo = new SofriPromo();
            try
            {
                //string sql = "SELECT * FROM SofriPromo WHERE PromoCode = @promo";
                //SofriPromo sp = _repo.Query<SofriPromo>(sql, new { promo = promoCode });
                SofriPromo sp = _ctx.SofriPromos.Where(x => x.PromoCode == promoCode).FirstOrDefault();  
                if (sp.Expiration >= DateTime.Now)
                {
                    return sp;
                }
            }
            catch (Exception ex)
            {
                _logs.Logger("An error occurred on GetPromoDetails method in SofriPromoMgr class! Details: " + ex.Message, "ERROR");
            }
            return spmo;
        }
        public CustomerDetailsDTO GetByCustomerID(string customerID)
        {
            CustomerDetailsDTO customerResult = new CustomerDetailsDTO();
            try
            {
                string baseurl = URL + "/Account/GetAccountsByCustomerId/2?authtoken=" + token + "&customerId=" + customerID;
                var client = new RestClient(baseurl);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);

                customerResult = JsonConvert.DeserializeObject<CustomerDetailsDTO>(response.Content);
                var result = customerResult;
            }
            catch (Exception ex)
            {
                _logs.Logger("An error occurred on GetByCustomerID endpoint! Details: " + ex.Message, "ERROR");
            }
            return customerResult;
        }
    }
}
