using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Logger;
using BANKSOFRI_LOAN.BUSINESSLOGIC.PaystackIntegration.DTO;
using BANKSOFRI_LOAN.DATALAYER.DomainObjects.Models;
using BANKSOFRI_LOAN.DATALAYER.DomainObjects.Repos;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;



namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Integrations
{
    public class PaystackAPIIntegration : IPaystack
    {
        ILogs log;
        IConfiguration _config;
        string chargerecurrenturl = null;
        string partialdebit = null;
        string token = null;
        string SubmitOtpUrl = null;
        DOMAINOBJECTS.CardModels.PaystackCardsContext _pctx;
        IRepo _repo;
        public PaystackAPIIntegration(IConfiguration config, ILogs _log, IRepo repo, DOMAINOBJECTS.CardModels.PaystackCardsContext pctx)
        {
            _config = config;
            _repo = repo;
            chargerecurrenturl = _config.GetSection("paystackrecurrentChargeendpoint").Value; 
            token = _config.GetSection("paystackKey").Value;
            SubmitOtpUrl = _config.GetSection("paystackSubmitOtpUrl").Value;
            partialdebit = _config.GetSection("paystackPartialDebitUrl").Value;
            log = _log;
            _pctx = pctx;
        }

        public async Task<GetBankResponse> LoadBanksFromPayStack()
        {
            GetBankResponse resp = new GetBankResponse();
            try
            {
                string url = _config.GetSection("listofbankksUrl").Value;
                string token = _config.GetSection("paystackKey").Value;
                var client = new RestClient(url);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", "", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);
                resp = JsonConvert.DeserializeObject<GetBankResponse>(response.Content.ToString());
            }
            catch (Exception ex)
            {
               log.Logger($"An error occured on LoadBanksFromPayStack method in PaystackAPIIntegration. Details: {ex.Message}", "Error");
            }
            return resp;
        }

        public async Task<ChargeResponse> VerifyTransaction(string custId, string loanId, string ReferenceId)
        {
            ChargeResponse reply = new ChargeResponse();
            try
            {
                log.ServiceMsg($"Verifying customer {custId} account debit via Paystack for repayment of loan {loanId}","INFO");
                string url = _config.GetSection("paystackVerifypaymentUrl").Value; 
                string token = _config.GetSection("paystackKey").Value; 
                var client = new RestClient(url + ReferenceId);
                var request = new RestRequest(Method.GET);
                request.RequestFormat = DataFormat.Json;
                client.Timeout = -1;
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddHeader("Content-Type", "application/json");
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                IRestResponse response = client.Execute(request);
                log.ServiceMsg($"Paystack transaction Verification response for customer {custId} details {response.Content}", "INFO");
                VerifytransactionResponse responsemessage = JsonConvert.DeserializeObject<VerifytransactionResponse>(response.Content.ToString());
                if (responsemessage != null && responsemessage.status)
                {
                    reply = new ChargeResponse()
                    {
                        status = responsemessage.status,
                        authcode = responsemessage.data.authorization.authorization_code
                    };
                }
                else reply = new ChargeResponse() { status = false };
            }
            catch (Exception ex)
            {
                log.Logger($"An Error occurred on paystack transaction verification API for customer {custId}! Details: {ex.Message}", "Error");
            }
            return reply;
        }

        public async Task<string> PartialDebit(ChargeCardObject cco, string user)
        {
            string authToken = null;
            try
            {
                var cd = await GetAuthorization(cco.Email);
                decimal chargeamt = cco.Amount * 100;
                var param = new PartialChargeRequest { authorization_code = cd.AuthorisationCode, email = cd.Email, amount = chargeamt.ToString(), currency = "NGN" };
                var reqbody = JsonConvert.SerializeObject(param);
                log.ServiceMsg($"Calling paystack API to debit customer {cco.CustomerId} commercial bank Account for the repayment of loan {cco.LoanRefId} paystack call request payload {reqbody}", "INFO");
                var client = new RestClient(partialdebit);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Accept", "application/json");                
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", reqbody, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                log.ServiceMsg($"paystack API customer ({cco.CustomerId}) account  debit response: {response.Content}", "INFO");
                ChargeCardResponse responsemessage = JsonConvert.DeserializeObject<ChargeCardResponse>(response.Content.ToString());
                if (responsemessage.status && responsemessage.data.status != "failed" && responsemessage.data.status != null)
                {
                    var resp = await VerifyTransaction(cco.CustomerId, cco.LoanRefId, responsemessage.data.reference);
                    if (resp.status)
                    {
                        var createHistory = new ChargeHistory()
                        {
                            Date = DateTime.Now,
                            Email = cd.Email,
                            ReferenceId = responsemessage.data.reference,
                            AmountCharge = cco.Amount,
                            ChargedBy = user,
                            Status = "SUCCESS"
                        };
                        await _repo.InsertAsync<ChargeHistory>(createHistory);
                        authToken = "SUCCESSFUL";
                    }
                    else
                    {
                        authToken = "Failed";
                    }
                }
                else
                {
                    authToken = "Failed";
                }

            }
            catch (Exception ex)
            {
                log.Logger($"An error occurred while trying to debit customer {cco.CustomerId} commercial bank account on paystack API: {ex.Message}", "ERROR");
            }
            return authToken;
        }

        public async Task<string> Charge(ChargeCardObject cco, string user)
        {
            string authToken = null;
            try
            {                
                var cd = await GetAuthorization(cco.Email);
                decimal chargeamt = cco.Amount * 100;
                var param = new ChargeRequest { authorization_code = cd.AuthorisationCode, email = cd.Email, amount = chargeamt.ToString() };
                var reqbody = JsonConvert.SerializeObject(param);
                log.ServiceMsg($"Calling paystack API to debit customer {cco.CustomerId} commercial bank Account for the repayment of loan {cco.LoanRefId} paystack call request payload {reqbody}", "INFO");
                var client = new RestClient(chargerecurrenturl);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Accept", "application/json");                
                request.AddHeader("Authorization", "Bearer " + token);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", reqbody, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                log.ServiceMsg($"paystack API customer ({cco.CustomerId}) account  debit response: {response.Content}", "INFO");
                ChargeCardResponse responsemessage = JsonConvert.DeserializeObject<ChargeCardResponse>(response.Content.ToString());               
                if (responsemessage.status && responsemessage.data.status != "failed" && responsemessage.data.status != null)
                {
                    var resp = await VerifyTransaction(cco.CustomerId, cco.LoanRefId, responsemessage.data.reference);
                    if (resp.status)
                    {
                        var createHistory = new ChargeHistory()
                        {
                            Date = DateTime.Now,
                            Email = cd.Email,
                            ReferenceId = responsemessage.data.reference,
                            AmountCharge = cco.Amount,
                            ChargedBy = user,
                            Status = "SUCCESS"
                        };
                        await _repo.InsertAsync<ChargeHistory>(createHistory);
                        authToken = "SUCCESSFUL";
                    }
                    else
                    {
                        authToken = responsemessage.data.gateway_response;
                    }
                }
                else
                {
                    authToken = "Failed";
                }

            }
            catch (Exception ex)
            {
                log.Logger($"An error occurred while trying to debit customer {cco.CustomerId} commercial bank account on paystack API: {ex.Message}", "ERROR");
            }
            return authToken;
        }

        public async Task<DOMAINOBJECTS.CardModels.CardTransaction> GetAuthorization(string email)
        {
            DOMAINOBJECTS.CardModels.CardTransaction cdetails = new DOMAINOBJECTS.CardModels.CardTransaction();
            try
            {               
                cdetails = _pctx.CardTransactions.Where(x => x.Email == email).OrderByDescending(x => x.Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                log.Logger("An error occurred while trying to fetch auth code for an integrated card from Database.: " + ex.Message, "Error");
            }
            return cdetails;
        }

        public async Task<InterestResponse> CalculateRepaymentInterest(string email, decimal interestdue)
        {
            DOMAINOBJECTS.CardModels.CardTransaction cdetails = new DOMAINOBJECTS.CardModels.CardTransaction();
            string sofricardbin = _config.GetSection("SofriCardBin").Value; 

            try
            {                
                cdetails = await GetAuthorization(email);
                if (cdetails != null)
                {
                    string sofricard = sofricardbin.Substring(0, 6);
                    if (cdetails.CardBin.StartsWith(sofricard))
                    {
                        decimal sofricarddiscount = decimal.Parse(_config.GetSection("SofriCardDiscount").Value);
                        decimal discount = sofricarddiscount / 100 * interestdue;
                        decimal discountedAmount = interestdue - discount;
                        return new InterestResponse { rate = sofricarddiscount, rateAmount = discountedAmount };
                    }

                }
            }
            catch (Exception ex)
            {
                log.Logger("An error occurred while trying to fetch auth code for an integrated card from Database.: " + ex.Message, "Error");
            }
            return new InterestResponse { rate = 0, rateAmount = 0 }; ; ;
        }
    
    
    }
}
