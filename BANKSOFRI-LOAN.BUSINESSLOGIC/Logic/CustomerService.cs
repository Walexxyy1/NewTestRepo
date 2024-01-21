using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Logger;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Security;
using BANKSOFRI_LOAN.DATALAYER.DomainObjects.Repos;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO.ProvenirObjects;
using BANKSOFRI_LOAN.DATALAYER.Repositories;
using BANKSOFRI_LOAN.DOMAINOBJECTS.CardModels;
using BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BANKSOFRI_LOAN.DATALAYER.Models.DTO.ProvenirObjects.ProvenirDecisionResponse;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Logic
{
    public class CustomerService : ICustomer
    {
       
        EncryptionService enc;
        IProvenir prd;
        ILogs _log;

        private static Random random;
        private readonly IBone _b;       
        ApiauthorisationContext _ctx;
        PaystackCardsContext _cctx;
        public CustomerService( IProvenir _prd, IBone b, ApiauthorisationContext ctx, ILogs log, PaystackCardsContext cctx)
        {           
            enc = new EncryptionService();
            prd = _prd;
            random = new Random();
            _b = b;           
            _ctx = ctx;
            _log = log;
            _cctx = cctx;
        }

        public async Task<ValidateNameOnCardResponse> ValidateCardDetails(ValidateCardDetailsDTO vnc)
        {
           
            ValidateNameOnCardResponse cld = new ValidateNameOnCardResponse();            
            try
            {
                CustomerDetailsDTO res = await _b.GetByCustomerID(vnc.CustomerId);
                if(res.customerID != null)
                {
                    if(res.name.Contains(vnc.FirstName) || res.name.Contains(vnc.LastName))
                    {
                        DateTime repaytime = DateTime.Now.AddMonths(3);
                        if(DateTime.Parse(vnc.ExpirationDate) > repaytime)
                        {
                            cld = new ValidateNameOnCardResponse() { ResponseCode = "00", ResponseMessage = "Success!", Description = "Card Details validated for tokenization" };
                        }
                        else
                        {
                            cld = new ValidateNameOnCardResponse() { ResponseCode = "02", ResponseMessage = "Failed!", Description = "Card will soon Expire!" };
                        }
                    }
                    else
                    {
                        cld = new ValidateNameOnCardResponse() { ResponseCode = "03", ResponseMessage = "Failed!", Description = "Customer Name not on Card!" };
                    }
                }
                else
                {
                    cld = new ValidateNameOnCardResponse() { ResponseCode = "01", ResponseMessage = "Failed!", Description = "Invalid customer Id" };
                }
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on ValidateCardDetails method! Details: " + ex.Message, "Error");
            }
            return cld;
        }

        public async Task<bool> HasPendingNanoLoan(string custId)
        {
            bool response = false;           
            try
            {
                //NanoLoan n = repo.Query<NanoLoan>("SELECT * FROM NanoLoans WHERE CustomerID=@cust AND Status=@status", new { cust = custId, status = "PENDING" });
                NanoLoan n = _ctx.NanoLoans.Where(x => x.CustomerId == custId && x.Status == "PENDING").FirstOrDefault();  
                if(n != null)
                {
                    response = true;
                }
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on HasPendingNanoLoan method in CustomerService Class! Details: " + ex.Message, "Error");
            }
            return response;
        }
        public async Task<bool> HasActiveNanoLoan(string customerId)
        {
            long resp = await _b.CountActiveLoan(customerId);
            if (resp > 0)
            {
                return true;
            }
            return false;
        }
        public async Task<string> GetCustomerCardStatus(string customerEmail)
        {
            string resp = null;
            try
            {
                string sql = "SELECT * FROM CardTransactions WHERE Email=@email ";
                List<CardTransaction> cb = _cctx.CardTransactions.Where(x => x.Email == customerEmail).ToList();
                if (cb != null)
                {
                    var cc = cb.Where(x => x.DateCreated == DateTime.Today).FirstOrDefault();
                    if (cc != null)
                    {
                        return "Valid Card";
                    }
                    else
                    {
                        return "Invalid Card";
                    }
                }
                else
                {
                    return "Invalid Card";
                }
            }
            catch (Exception ex)
            {
               _log.Logger("An error occured on GetCustomerCardStatus method in CustomerService Class! Details: " + ex.Message, "Error");
            }
            return resp;
        }

        public async Task<bool> IsAccountValid(string customerId)
        {
            bool response = false;
            try
            {
                var custAcct = await _b.GetByCustomerID(customerId);
                Account acct = custAcct.Accounts.Where(x => x.accountType == "SavingsOrCurrent").FirstOrDefault();
                string accountgrade = acct.kycLevel;
                if (accountgrade == "3")
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
               _log.Logger("An error occured on IsAccountValid method! Details: " + ex.Message, "Error");
            }
            return response;
        }
        public async Task<bool> IsSofriAccountBalanceSufficient(string customerId, string accountnum, decimal Amount)
        {
            bool response = false;
            try
            {
                var custAcct = _b.GetAccountBalance(customerId);
                AccountDetails accd = custAcct.Accounts.Where(x => x.NUBAN == accountnum).FirstOrDefault();
                decimal accountbalance = decimal.Parse(accd.WithdrawableAmount.ToString());
                if(accountbalance >= Amount)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on SaveCustomerLoan method! Details: " + ex.Message, "Error");
            }
            return response;
        }

        public async Task<long> SaveCustomerLoan(NanoLoan nl)
        {
            long response = 0;
            try
            {
               await _ctx.NanoLoans.AddAsync(nl);
                response = await _ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on SaveCustomerLoan method! Details: " + ex.Message, "Error");
            }
            return response;
        }

        public async Task<GetLoanOffersResponse> GetCustomerLatestLoanOffers(string custID)
        {           
            try
            {
                CustomerOffer cd = await prd.GetLatestCustomerProvenirData(custID);
                if (cd.CustomerId != null)
                {
                    CustomerDetail d = await GetCustomerLoanProcessingDetailsFromDB(custID);
                    List<DATALAYER.Models.DTO.ProvenirObjects.Offer> voffer = JsonConvert.DeserializeObject<List<DATALAYER.Models.DTO.ProvenirObjects.Offer>>(cd.Offers.ToString());                   
                    return new GetLoanOffersResponse() { CustomerID = custID, ResponseCode = "00", ResponseMessage = "Loan Offers fetched successfully!", LoanOffers = voffer, CardStatus = await GetCustomerCardStatus(d.Email) };
                }
                else
                {
                    return new GetLoanOffersResponse() { CustomerID = custID, ResponseCode = "01", ResponseMessage = "No loan offers was found for this customer" };
                }
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on GetCustomerLatestLoanOffers method! Details: " + ex.Message, "Error");
                return new GetLoanOffersResponse() { CustomerID = custID, ResponseCode = "011", ResponseMessage = "An error occurred! Details: " + ex.Message };
            }
            //return offers;
        }
      
        public async Task SaveCreditBureau(CreditScoreData cbr)
        {           
            try
            {
                await _ctx.CreditScoreData.AddAsync(cbr);
                await _ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on SaveCreditBureau method! Details: " + ex.Message, "Error");
            }           
        }

        public async Task<double> GetCustomerORR(string customerID)
        {
            double orrScore = 0;
            try
            {
                //string sql = "SELECT * FROM NanoLoans WHERE CustomerID=@custID AND Status = @stl OR Status=@stq";
                //List<NanoLoan> cb = (List<NanoLoan>)repo.QueryList<NanoLoan>(sql, new { custID = customerID, stl = "CLOSED", stq = "LIQUIDATED" });
                List<NanoLoan> cb = _ctx.NanoLoans.Where(x => x.CustomerId == customerID && x.Status == "CLOSED" || x.Status == "LIQUIDATED").ToList();
                if (cb != null)
                {
                    double totalscore = 0;
                    foreach (NanoLoan n in cb)
                    {
                        totalscore += await CalculateORR(n.LoanReferenceId);
                    }
                    orrScore = totalscore / cb.Count();
                }
                else
                {
                    return 0;
                }                
            }
            catch (Exception ex)
            {
                _log.Logger($"An error occured on GetCustomerORR method! Details: { ex.Message}", "ERROR");
            }
            return orrScore;
        }
        public async Task<long> GetApplicationId()
        {
            long applicationId = await CountAllRequests();
            try
            {
                if (applicationId == 0)
                {
                    string sql = "SELECT * FROM NanoLoans";
                    IEnumerable<NanoLoan> nlist = await _ctx.NanoLoans.ToListAsync();
                    long totalapp = nlist.Count();
                    applicationId = totalapp + 1;
                }
            }
            catch (Exception ex)
            {
                _log.Logger($"An error occured on GetApplicationId method! Details: { ex.Message}", "ERROR");
            }
            return applicationId;
        }
        public async Task<long> CountAllRequests()
        {
            long totalreq = 0;
            try
            {
                string sql = "SELECT * FROM NanoLoanRequests";
               // IEnumerable<NanoLoanRequest> nlist = (List<NanoLoanRequest>)repo.QueryList<NanoLoanRequest>(sql);
                IEnumerable<NanoLoanRequest> nlist = await _ctx.NanoLoanRequests.ToListAsync();
               long total = nlist.Count();
                totalreq = total + 1;
            }
            catch (Exception ex)
            {
                _log.Logger($"An error occured on CountAllRequests method! Details: { ex.Message}", "ERROR");
            }
            return totalreq;
        }

        public async Task<double> CalculateORR(string loanId)
        {
            double loanOrrScore = 0;
            try
            {
                IEnumerable<NanoLoanRepaymentSchedule> nrs = await GetPastRepaymentSchedule(loanId);
                if (nrs != null)
                {
                    foreach (NanoLoanRepaymentSchedule item in nrs)
                    {
                        loanOrrScore += ORRScores(item.DaysOverDue);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Logger($"An error ocurred on GetCustomerLoanProcessingDetailsFromDB. Details: {ex.Message}", "ERROR");
            }
            return loanOrrScore;
        }

        public async Task<IEnumerable<NanoLoanRepaymentSchedule>> GetPastRepaymentSchedule(string LoanRefId)
        {
            IEnumerable<NanoLoanRepaymentSchedule> nrs = new List<NanoLoanRepaymentSchedule>();
            try
            {
                //string sql = "SELECT * FROM NanoLoanRepaymentSchedule WHERE LoanReferenceId = @refId";
                //nrs = (List<NanoLoanRepaymentSchedule>)repo.QueryList<NanoLoanRepaymentSchedule>(sql, new { refId = LoanRefId });
                nrs = await _ctx.NanoLoanRepaymentSchedules.Where(x => x.LoanReferenceId == LoanRefId).ToListAsync();
            }
            catch (Exception ex)
            {
                _log.Logger("An error ocurred on GetCustomerLoanProcessingDetailsFromDB. Details: " + ex.Message, "Error");
            }
            return nrs;
        }

        double ORRScores(int dayoverdue)
        {
            double score = 0;
            if(dayoverdue == 0)
            {
                score = 100;
            }
            else if(dayoverdue == 1)
            {
                score = 93.33;
            }
            else if (dayoverdue == 2)
            {
                score = 93.33;
            }
            else if (dayoverdue == 3)
            {
                score = 80.00;
            }
            else if (dayoverdue == 4)
            {
                score = 73.33;
            }
            else if (dayoverdue == 5)
            {
                score = 66.67;
            }
            else if (dayoverdue == 6)
            {
                score = 60.00;
            }
            else if (dayoverdue == 7)
            {
                score = 53.33;
            }
            else if (dayoverdue == 8)
            {
                score = 46.67;
            }
            else if (dayoverdue == 9)
            {
                score = 40.00;
            }
            else if (dayoverdue == 10)
            {
                score = 33.33;
            }
            else if (dayoverdue == 11)
            {
                score = 26.67;
            }
            else if (dayoverdue == 12)
            {
                score = 20.00;
            }
            else if (dayoverdue == 13)
            {
                score = 13.33;
            }
            else if (dayoverdue == 14)
            {
                score = 6.67;
            }
            else 
            {
                score = 0.00;
            }
            return score;
        }

        public async Task<CustomerAndLoanDTO> GetCustomerType(string customerID)
        {
            string type = "New";
            CustomerAndLoanDTO cld = new CustomerAndLoanDTO();
           // List<NanoLoan> listn = new List<NanoLoan>();
            try
            {
                //string sql = "SELECT * FROM NanoLoans WHERE CustomerID=@custID";
                //IEnumerable<NanoLoan> cb = (List<NanoLoan>)repo.QueryList<NanoLoan>(sql, new { custID = customerID });
                IEnumerable<NanoLoan> cb = await _ctx.NanoLoans.Where(x => x.CustomerId == customerID).ToListAsync();
                if (cb.Count() > 0)
                {
                    type = "Repeat";                                   
                }
                cld = new CustomerAndLoanDTO()
                {
                    CustomerType = type,
                    LoanNumber = cb.Count() + 1
                };
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on GetCustomerType method! Details: " + ex.Message, "Error");
            }
            return cld;
        }

        public async Task<CreditScoreData> GetMostRecentCreditBureauScore(string customerID, string bureaType)
        {
            CreditScoreData cbr = new CreditScoreData();
            DateTime maxdate = DateTime.Now.AddDays(-29);
            try
            {
                //string sql = "SELECT * FROM CreditScoreData WHERE CustomerID=@custID AND Bureau=@bureau ORDER BY ID DESC";
                //List<CreditScoreData> cb = (List<CreditScoreData>)repo.QueryList<CreditScoreData>(sql, new { custID = customerID, bureau = bureaType });
                List<CreditScoreData> cb = _ctx.CreditScoreData.Where(x => x.CustomerId == customerID && x.Bureau == bureaType).ToList();
                var result = cb.Where(x => x.Date >= maxdate).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on GetMostRecentCreditBureauScore method! Details: " + ex.Message, "Error");
            }
            return cbr;
        }       

        public async Task<long> SaveOrUdateCusomerLoanProcessingDetails(NanoLoanRequestDTO r)
        {
            long resp = 0;
            if(r.BVN.StartsWith("0000"))
            {
                return 0;
            }
            try
            {
                CustomerDetail d = await GetCustomerLoanProcessingDetailsFromDB(r.CustomerId);
                if (d == null)
                {
                     await _ctx.CustomerDetails.AddAsync(new CustomerDetail() 
                    { 
                        CustomerId = r.CustomerId, 
                        FirstName = r.FirstName, 
                        LastName = r.LastName, 
                        Gender = r.Gender, 
                        MaritalStatus = r.MaritalStatus, 
                        DateOfBirth = r.DateOfBirth, 
                        Phone =r.Phone, 
                        Email =r.Email, 
                        Bvn = r.BVN, 
                        Street =r.Street,
                        State = r.State,
                        City = r.City, 
                        Country = r.Country, 
                        PostalCode =r.PostalCode, 
                        EmployerName = r.EmployerName, 
                        EmploymentDuration =r.EmploymentDuration, 
                        EmploymentType = r.EmploymentType,
                        Industry = r.Industry,
                        ResidenceStatus = r.ResidenceStatus, 
                        Income = r.Income, 
                        Type = r.Type, 
                        LevelOfEducation = r.LevelOfEducation, 
                        IncomeVerified =r.IncomeVerified, 
                        KycScore = "3", 
                        LoanAmount = r.LoanAmount, 
                        LoanPurpose = r.LoanPurpose 
                    });
                    resp = await _ctx.SaveChangesAsync();
                    if (resp > 0)
                    {
                        _log.Logger("A Customer with Customer ID: " + r.CustomerId + " was successfully created on the Database", "Info");
                    }
                }
                else
                {
                    d.CustomerId = r.CustomerId;
                    d.MaritalStatus = r.MaritalStatus;
                    d.Street = r.Street;
                    d.State = r.State;
                    d.City = r.City;
                    d.Country = r.Country;
                    d.PostalCode = r.PostalCode;
                    d.EmployerName = r.EmployerName;
                    d.EmploymentDuration = r.EmploymentDuration;
                    d.EmploymentType = r.EmploymentType;
                    d.Industry = r.Industry;
                    d.ResidenceStatus = r.ResidenceStatus;
                    d.Income = r.Income;
                    d.Type = r.Type;
                    d.LevelOfEducation = r.LevelOfEducation;
                    d.IncomeVerified = r.IncomeVerified;
                    d.KycScore = "3";
                    d.LoanAmount = r.LoanAmount;
                    d.LoanPurpose = r.LoanPurpose;
                    _ctx.Update(d);
                    resp = await _ctx.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _log.Logger("An error occurred on SaveOrUdateCusomerLoanProcessingDetails method in CustomerService class. Details:" + ex.Message, "Error");
            }
            return resp;
        }

        public async Task SaveProvenirRequest(NanoLoanRequest r)
        {            
            try
            {
               //await repo.InsertAsync<NanoLoanRequest>(r);
                await _ctx.NanoLoanRequests.AddAsync(r);
                await _ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _log.Logger("An error occurred on SaveProvenirRequest method in CustomerService class. Details:" + ex.Message, "Error");
            }            
        }


        public async Task<CustomerDetail> GetCustomerLoanProcessingDetailsFromDB(string custID)
        {
            CustomerDetail cd = new CustomerDetail();
            try
            {
                //string sql = "SELECT * FROM CustomerDetails WHERE CustomerID = @cId";
                //cd = repo.Query<CustomerDetail>(sql, new { cId = custID });
                cd = await _ctx.CustomerDetails.Where(x => x.CustomerId == custID).FirstOrDefaultAsync();  
            }
            catch (Exception ex)
            {
                _log.Logger("An error ocurred on GetCustomerLoanProcessingDetailsFromDB. Details: " + ex.Message, "Error");
            }
            return cd;
        }

        public async Task<string> CreateCustomer(CustomerDTO cd)
        {
            long resp = 0;
            string retUpass = null;
            try
            {
                string upasswrd = GenerateDefaultPassword();
                await _ctx.CustomersTables.AddAsync(new CustomersTable()
                {
                    FirstName = cd.FirstName,
                    LastName = cd.LastName,
                    Email = cd.Email,
                    PhoneNumber = cd.PhoneNumber,
                    Password = enc.Encrypt(upasswrd),
                    DateCreated = DateTime.Now
                });
                resp = await _ctx.SaveChangesAsync();
                if (resp > 0)
                {
                    retUpass = upasswrd;
                    _log.Logger("A Customer with Email ID: " + cd.Email + " was successfully created on the Database", "Info");
                }

            }
            catch (Exception ex)
            {
                _log.Logger(ex.Message, "Error");
            }
            return retUpass;
        }

        public async Task<string> GetCustomerDetails(string email)
        {
            CustomersTable cd = new CustomersTable();
            string custdetails = null;
            try
            {
                //cd = repo.Query<CustomersTable>("SELECT * FROM Customers WHERE Email = @mail", new { mail = email });
                cd = _ctx.CustomersTables.Where(x => x.Email == email).FirstOrDefault();    
                if (cd != null)
                {
                    custdetails = enc.Decrypt(cd.Password);
                }
            }
            catch (Exception ex)
            {
                _log.Logger(ex.Message, "Error");
            }
            return custdetails;
        }

        public static string GenerateDefaultPassword()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 12)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
