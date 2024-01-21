using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO;
using BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Integrations
{
    public class BankOneIntegration : IBone
    {
        IConfiguration _config;
        string bearerToken = null;
        string institutionCode = null;
        string baseurl = null;
        string glCR = null;
        string glDR = null;
        string paystackglcode = null;
        string chashbackGL = null;
        string interestGLDR = null;
        string url = null;
        ILogs _log;
        string apiversion = null;
        private readonly ILoanService _loan;

        public BankOneIntegration(IConfiguration config, ILoanService loan, ILogs log)
        {
            _config = config;
            bearerToken = _config.GetSection("bankOneServiceCalltoken").Value;
            institutionCode = _config.GetSection("institutionCode").Value;
            baseurl = _config.GetSection("bankOnethirdPartyAPIbaseurl").Value;
            glCR = _config.GetSection("NanoLoanPrincipalGLCR").Value;
            glDR = _config.GetSection("NanoLoanGLDR").Value;
            paystackglcode = _config.GetSection("NanoLoanPaystackGLCodeCR").Value;
            chashbackGL = _config.GetSection("NanoLoanPrincipalGLCR").Value;
            interestGLDR = _config.GetSection("NanoLoanInterestGLCode").Value;
            url = _config.GetSection("bankOneAPIbaseurl").Value;
            apiversion = _config.GetSection("apiVersion").Value;
            _log = log;
            _loan = loan;

        }

        public LoanDisbursementResponse DisburseLoanViaBankOne(LoanDisbursementRequestDTO ld)
        {
            LoanDisbursementResponse resp = new LoanDisbursementResponse();
            try
            {
                string transref = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
                decimal amt = decimal.Parse(ld.Amount) * 100;
                string requestBody = JsonConvert.SerializeObject(new LoanDisbursementRequest
                {
                    RetrievalReference = transref,
                    AccountNumber = ld.AccountNumber,
                    Amount = amt.ToString(),
                    Narration = ld.Narration,
                    Fee = 0,
                    NibssCode = institutionCode,
                    GLCode = glCR,
                    Token = bearerToken
                });
                string url = baseurl + "/CoreTransactions/Credit";
                var client = new RestClient(url);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddJsonBody(requestBody);
                IRestResponse response = client.Execute(request);
                resp = JsonConvert.DeserializeObject<LoanDisbursementResponse>(response.Content.ToString());
                _log.Logger("Loan Disbursement to Account Number :" + ld.AccountNumber + " response: " + response.Content.ToString(), "INFO");
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on LoanDisbursementViaBankOne method! Details: " + ex.Message, "Error");
            }
            return resp;
        }

        public async Task<LoanDisbursementResponse> LoanPrincipalCollectionFromSofriAccount(LoanCollectionRequestDTO ld)
        {
            LoanDisbursementResponse resp = new LoanDisbursementResponse();
            try
            {
                decimal amt = decimal.Parse(ld.Amount) * 100;
                string requestBody = JsonConvert.SerializeObject(new LoanDisbursementRequest
                {
                    RetrievalReference = ld.RetrievalReference,
                    AccountNumber = ld.AccountNumber,
                    Amount = amt.ToString(),
                    Narration = ld.Narration,
                    Fee = 0,
                    NibssCode = institutionCode,
                    GLCode = glDR,
                    Token = bearerToken
                });

                string url = baseurl + "/CoreTransactions/Debit";
                var client = new RestClient(url);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddJsonBody(requestBody);
                IRestResponse response = client.Execute(request);

                resp = JsonConvert.DeserializeObject<LoanDisbursementResponse>(response.Content.ToString());
                _log.Logger("Loan Principal Collection Response : " + response.Content.ToString(), "INFO");
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on LoanCollectionFromSofriAccount method! Details: " + ex.Message, "Error");
            }
            return resp;
        }
        public async Task<LoanDisbursementResponse> DisburseCashBackViaBankOne(LoanCollectionRequestDTO ld)
        {
            LoanDisbursementResponse resp = new LoanDisbursementResponse();
            try
            {
                string transref = Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
                decimal amt = decimal.Parse(ld.Amount) * 100;
                string requestBody = JsonConvert.SerializeObject(new LoanDisbursementRequest
                {
                    RetrievalReference = transref,
                    AccountNumber = ld.AccountNumber,
                    Amount = amt.ToString(),
                    Narration = ld.Narration,
                    Fee = 0,
                    NibssCode = institutionCode,
                    GLCode = chashbackGL,
                    Token = bearerToken
                });
                string url = baseurl + "/CoreTransactions/Credit";
                var client = new RestClient(url);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddJsonBody(requestBody);
                IRestResponse response = client.Execute(request);
                resp = JsonConvert.DeserializeObject<LoanDisbursementResponse>(response.Content.ToString());
                _log.Logger("Loan Repayment Cashback Response : " + response.Content.ToString(), "INFO");
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on LoanDisbursementViaBankOne method! Details: " + ex.Message, "Error");
            }
            return resp;
        }
        public async Task<LoanDisbursementResponse> LoanInterestCollectionFromSofriAccount(LoanCollectionRequestDTO ld)
        {
            LoanDisbursementResponse resp = new LoanDisbursementResponse();
            decimal amt = decimal.Parse(ld.Amount) * 100;
            try
            {
                string requestBody = JsonConvert.SerializeObject(new LoanDisbursementRequest
                {
                    RetrievalReference = ld.RetrievalReference,
                    AccountNumber = ld.AccountNumber,
                    Amount = amt.ToString(),
                    Narration = ld.Narration,
                    Fee = 0,
                    NibssCode = institutionCode,
                    GLCode = interestGLDR,
                    Token = bearerToken
                });

                string url = baseurl + "/CoreTransactions/Debit";
                var client = new RestClient(url);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddJsonBody(requestBody);
                IRestResponse response = client.Execute(request);
                resp = JsonConvert.DeserializeObject<LoanDisbursementResponse>(response.Content.ToString());
                _log.Logger("Loan Interest Collection Response : " + response.Content.ToString(), "INFO");
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on LoanInterestCollectionFromSofriAccount method! Details: " + ex.Message, "Error");
            }
            return resp;
        }

        public async Task<LoanDisbursementResponse> CreditCustomerSofriAccountForRepayment(LoanCollectionRequestDTO ld)
        {
            LoanDisbursementResponse resp = new LoanDisbursementResponse();
            try
            {
                decimal amt = decimal.Parse(ld.Amount) * 100;
                string requestBody = JsonConvert.SerializeObject(new LoanDisbursementRequest
                {
                    RetrievalReference = ld.RetrievalReference,
                    AccountNumber = ld.AccountNumber,
                    Amount = amt.ToString(),
                    Narration = ld.Narration,
                    Fee = 0,
                    NibssCode = institutionCode,
                    GLCode = paystackglcode,
                    Token = bearerToken
                });

                string url = baseurl + "/CoreTransactions/Credit";
                var client = new RestClient(url);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddJsonBody(requestBody);
                IRestResponse response = client.Execute(request);
                resp = JsonConvert.DeserializeObject<LoanDisbursementResponse>(response.Content.ToString());
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on CreditCustomerSofriAccountForRepayment method! Details: " + ex.Message, "Error");
            }
            return resp;
        }

        public async Task<List<NanoLoan>> GetCutomerLoanAccounts(string customerID)
        {           
            List<NanoLoan> loanAccts = new List<NanoLoan>();
            _log.Logger("GetCutomerLoanAccounts endpoint called with customer Id:" + customerID, "INFO");
            try
            {
                string appurl = _config.GetSection("bankOneAPIbaseurl").Value;
                var client = new RestClient(appurl);
                var request = new RestRequest("/Loan/GetLoansByCustomerId/2", Method.GET);
                request.AddQueryParameter("authtoken", bearerToken);
                request.AddQueryParameter("institutionCode", institutionCode);
                request.AddQueryParameter("customerID", customerID);
                var response = client.Execute(request);
                GetCustomerLoansResponse cd = JsonConvert.DeserializeObject<GetCustomerLoansResponse>(response.Content);
                if (cd.IsSuccessful)
                {
                    foreach (Message m in cd.Message)
                    {
                        NanoLoan n = _loan.GetNanoLoan(m.AccountOpenningTrackingRef);
                        if (n != null)
                        {
                            n.Status = m.RealLoanStatus;
                            loanAccts.Add(n);
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                _log.Logger("An error occurred on GetCutomerLoanAccounts endpoint! Details: " + ex.Message, "ERROR");
            }
            return loanAccts;
        }

        public string GetBankOneLoanAccountStatus(string customerID, string loanId)
        {
            string loanStatus = null;
            _log.Logger("GetCutomerLoanAccounts endpoint called with customer Id:" + customerID, "INFO");
            try
            {
                string appurl = _config.GetSection("bankOneAPIbaseurl").Value;
                var client = new RestClient(appurl);
                var request = new RestRequest("/Loan/GetLoansByCustomerId/2", Method.GET);
                request.AddQueryParameter("authtoken", bearerToken);
                request.AddQueryParameter("institutionCode", institutionCode);
                request.AddQueryParameter("customerID", customerID);
                var response = client.Execute(request);
                GetCustomerLoansResponse cd = JsonConvert.DeserializeObject<GetCustomerLoansResponse>(response.Content);
                if (cd.IsSuccessful)
                {
                    Message msg = cd.Message.Where(x => x.AccountOpenningTrackingRef == loanId).FirstOrDefault();
                    loanStatus = msg.RealLoanStatus.ToString();
                }
            }
            catch (Exception ex)
            {
                _log.Logger("An error occurred on GetBankOneLoanAccountStatus endpoint! Details: " + ex.Message, "ERROR");
            }
            return loanStatus;
        }

        public async Task<CustomerDetailsDTO> GetByCustomerID(string customerID)
        {
            CustomerDetailsDTO customerResult = new CustomerDetailsDTO();
            List<Account> acct = new List<Account>();
            _log.Logger("GetByCustomerID endpoint called with customer Id:" + customerID, "INFO");
            try
            {
                string appurl = _config.GetSection("bankOneAPIbaseurl").Value;
                var client = new RestClient(appurl);
                var request = new RestRequest("/Account/GetAccountsByCustomerId/2", Method.GET);
                request.AddQueryParameter("authtoken", bearerToken);
                request.AddQueryParameter("customerID", customerID);
                var response = client.Execute(request);
                GetCustomerDetails cd = JsonConvert.DeserializeObject<GetCustomerDetails>(response.Content);
                if (cd.customerID != null)
                {
                    foreach (Account a in cd.Accounts)
                    {
                        if (a.accountType == "SavingsOrCurrent" && a.accountStatus == "Active")
                        {
                            a.accountNumber = a.NUBAN;
                            acct.Add(a);
                        }
                    }
                    customerResult = new CustomerDetailsDTO()
                    {
                        name = cd.name,
                        address = cd.address,
                        age = cd.age,
                        BVN = cd.BVN,
                        email = cd.email,
                        dateOfBirth = cd.dateOfBirth,
                        gender = cd.gender,
                        customerID = cd.customerID,
                        phoneNumber = cd.phoneNumber,
                        state = cd.state,
                        localGovernmentArea = cd.localGovernmentArea,
                        Accounts = acct
                    };
                }
            }
            catch (Exception ex)
            {
                _log.Logger($"An error occurred on GetByCustomerID endpoint Customer ID {customerID}! Details:  {ex.Message}", "ERROR");
            }
            return customerResult;
        }

        public async Task<long> CountActiveLoan(string customerID)
        {
            long totalActiveloan = 0;
            List<Account> acct = new List<Account>();
            _log.Logger("GetByCustomerID endpoint called with customer Id:" + customerID, "INFO");
            try
            {
                string appurl = _config.GetSection("bankOneAPIbaseurl").Value;
                var client = new RestClient(appurl);
                var request = new RestRequest("/Account/GetAccountsByCustomerId/2", Method.GET);
                request.AddQueryParameter("authtoken", bearerToken);
                request.AddQueryParameter("customerID", customerID);
                var response = client.Execute(request);
                GetCustomerDetails cd = JsonConvert.DeserializeObject<GetCustomerDetails>(response.Content);
                if (cd.customerID != null)
                {
                    foreach (Account a in cd.Accounts)
                    {
                        if (a.accountType == "loan" && a.accountStatus == "Active")
                        {
                            a.accountNumber = a.NUBAN;
                            acct.Add(a);
                        }
                    }                    
                }
                totalActiveloan = acct.Count;
            }
            catch (Exception ex)
            {
                _log.Logger($"An error occurred on GetByCustomerID endpoint Customer ID {customerID}! Details:  {ex.Message}", "ERROR");
            }
            return totalActiveloan;
        }

        public GetCustomerAccounts GetAccountBalance(string customerId)
        {
            string appurl = _config.GetSection("bankOneAPIbaseurl").Value;
            GetCustomerAccounts customerResult = new GetCustomerAccounts();
            var client = new RestClient(appurl);
            var request = new RestRequest("/Account/GetActiveSavingsAccountsByCustomerID2/2", Method.GET);
            request.AddQueryParameter("authtoken", bearerToken, false);
            request.AddQueryParameter("customerId", customerId);
            var response = client.Execute(request);
            List<AccountDetails> Accounts = JsonConvert.DeserializeObject<List<AccountDetails>>(response.Content);
            customerResult.Accounts = Accounts;
            return customerResult;
        }

        public bool AddCardValidation(string customerId)
        {
            bool reply = false;
            try
            {
                string url = baseurl + "/CoreTransactions/CustomerHasAuthorisedCard";
                var client = new RestClient(url);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                var body = JsonConvert.SerializeObject(new BankOneAddCardStatusValidationRequest
                {
                    CustomerId = customerId,
                    Token = bearerToken
                });
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                BankOneAddCardStatusValidationResponse rep = JsonConvert.DeserializeObject<BankOneAddCardStatusValidationResponse>(response.Content.ToString());
                reply = rep.HasCard;
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on AddCardValidation method! Details: " + ex.Message, "Error");
            }
            return reply;
        }

        public BankOneLoanCreationResponse CreateLoanOnBankOne(BankOneLoanCreationRequest req)
        {
            BankOneLoanCreationResponse result = new BankOneLoanCreationResponse();
            _log.Logger($"Attempting loan creation and disbursement for customer with id {req.CustomerID} via BankOne on CreateLoanOnBankOne endpoint called with payload:{JsonConvert.SerializeObject(req)}", "INFO");
            try
            {
                string appurl = _config.GetSection("bankOneAPIbaseurl").Value;
                string requestBody = JsonConvert.SerializeObject(req);
                var client = new RestClient(appurl);
                var request = new RestRequest("/LoanApplication/LoanCreationApplication2/2", Method.POST);
                request.AddQueryParameter("authtoken", bearerToken);
                request.AddJsonBody(requestBody);
                var response = client.Execute<CustomerDetailsDTO>(request);
                result = JsonConvert.DeserializeObject<BankOneLoanCreationResponse>(response.Content);
                _log.Logger($"Loan creation and disbursement Response for customer with id {req.CustomerID} BankOne Response:{JsonConvert.SerializeObject(response.Content)}", "INFO");
            }
            catch (Exception ex)
            {
                _log.Logger($"An error occurred on CreateLoanOnBankOne method in BankOneIntegration class! Details: { ex.Message}", "ERROR");
            }
            return result;
        }

        public AccountlienResponse PlaceLienOnAccount(AccountlienRequestObject alr)
        {
            AccountlienResponse responseobject = new AccountlienResponse();
            try
            {
                string url = baseurl;
                string token = bearerToken;
                string version = _config.GetSection("apiVersion").Value;
                alr.AuthenticationCode = token;
                var client = new RestClient(url + "/Account/PlaceLien");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                var param = JsonConvert.SerializeObject(alr);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("version", version);
                request.AddJsonBody(param);
                IRestResponse response = client.Execute(request);
                responseobject = JsonConvert.DeserializeObject<AccountlienResponse>(response.Content);
                Console.WriteLine(response.Content);
            }
            catch (Exception ex)
            {
                _log.Logger($"An error occurred on PlaceLienOnAccount method in BankOneIntegration class! Details: { ex.Message}", "ERROR");
            }
            return responseobject;
        }
        public AccountlienResponse RemoveLienOnAccount(RemovelientOnAccountRequestObject alr)
        {
            AccountlienResponse responseobject = new AccountlienResponse();
            try
            {
                string url = baseurl;
                string token = bearerToken;
                string version = _config.GetSection("apiVersion").Value;
                alr.AuthenticationCode = token;
                var client = new RestClient(url + "/Account/UnPlaceLien");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                var param = JsonConvert.SerializeObject(alr);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("version", version);
                request.AddJsonBody(param);
                IRestResponse response = client.Execute(request);
                responseobject = JsonConvert.DeserializeObject<AccountlienResponse>(response.Content);
                Console.WriteLine(response.Content);
            }
            catch (Exception ex)
            {
                _log.Logger($"An error occurred on RemoveLienOnAccount method in BankOneIntegration class! Details: { ex.Message}", "ERROR");
            }
            return responseobject;
        }

        public RepayLoanRespsonse RepaySofriNanoLoan(RepayLoanDTIO r)
        {
            //bool reply = false;
            RepayLoanRespsonse reply = new RepayLoanRespsonse();            
            try
            {
                RepayLoanRequestObject requestobject = new RepayLoanRequestObject() { accountNumber = r.accountNumber, principalNarration = "Principal amount repayment", repaymentAmount = double.Parse(r.repaymentAmount.ToString()), interestNarration = "Interest on loan repayment", feeNarration = "Fee payment", authtoken = bearerToken, version = apiversion };
                string fullurl = url + "/LoanAccount/RepayLoan/2";
                var client = new RestClient(fullurl);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                var body = JsonConvert.SerializeObject(requestobject);
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                reply = JsonConvert.DeserializeObject<RepayLoanRespsonse>(response.Content.ToString());                
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on RepaySofriNanoLoan method! Details: " + ex.Message, "Error");
            }
            return reply;
        }
    }
}
