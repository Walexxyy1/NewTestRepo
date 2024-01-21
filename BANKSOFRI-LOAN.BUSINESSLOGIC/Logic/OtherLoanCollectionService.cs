using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Logger;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO;
using BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Logic
{
    public class OtherLoanCollectionService : IOtherloans
    {
        ILoanService ls;
        IPaystack pi;
        IBone bi;       
        private readonly ILogs _log;
        public OtherLoanCollectionService(ILoanService _ls, IPaystack _pi, IBone _bi, ILogs log)
        {
            ls = _ls;
            pi = _pi;
            bi = _bi;          
            _log = log;
        }
        public async Task<string> StartOtherSofriLoanFailedRepaymentCollection()
        {
            try
            {
                _log.ServiceMsg("Searching for Other Sofri unpaid matured loan Repayments", "INFO");
                 IEnumerable<SofriOtherLoanCustomer> dueloans = ls.GetSofriOtherloanCustomers();
                _log.ServiceMsg($"Total customer accounts in debit as at {DateTime.Now} found ({ dueloans.Count()} )!", "INFO");
                foreach (SofriOtherLoanCustomer n in dueloans)
                {
                    GetCustomerAccounts gca = bi.GetAccountBalance(n.CustomerId);                    
                    AccountDetails acct = gca.Accounts.Where(x => x.NUBAN == n.SofriAccountNumber).FirstOrDefault();
                    decimal deductAmount = (decimal)Math.Abs(acct.AvailableBalance);
                    if (acct.AvailableBalance < 1)
                    {
                        _log.ServiceMsg($"Calling Paystack API to fund customer {n.CustomerId} sofri account for loan repayment {n.LoanId}", "INFO");
                        await ExternalCollection(n, deductAmount);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on StartOtherSofriLoanFailedRepaymentCollection method! Details: " + ex.Message, "Error");
            }
            return null;
        }

        async Task ExternalCollection(SofriOtherLoanCustomer solc, decimal accountbalance)
        { 
            string reply = await pi.Charge(new ChargeCardObject() { Email = solc.Email, Amount = accountbalance, CustomerId = solc.CustomerId, LoanRefId = solc.LoanId }, "LOAN COLLECTION");
            _log.ServiceMsg($"Paystack Response for Customer Account Debit for loan {solc.LoanId} repayment { reply }!", "INFO");
            if (reply == "SUCCESSFUL")
            {
                _log.ServiceMsg($"Repayment successfully taken from Customer Account via Paystack { solc.LoanId }!", "INFO");
                LoanDisbursementResponse acctCredResponse = await bi.CreditCustomerSofriAccountForRepayment(new LoanCollectionRequestDTO()
                {
                    AccountNumber = solc.SofriAccountNumber,
                    Amount = accountbalance.ToString(),
                    Narration = "Fund Account for Repayment",
                    RetrievalReference = Guid.NewGuid().ToString().Substring(0, 10).ToUpper()
                });
                if (acctCredResponse.IsSuccessful)
                {
                    _log.ServiceMsg($"Customer: {solc.CustomerId} Sofri Account {solc.SofriAccountNumber} funded successfully in preparation for loan repayment collection { solc.LoanId }!", "INFO");
                    string referenceId = Guid.NewGuid().ToString().Substring(0, 10).ToUpper();
                    AccountlienResponse alr = bi.PlaceLienOnAccount(new AccountlienRequestObject() { AccountNo = solc.SofriAccountNumber, Amount = accountbalance.ToString(), Reason = "Loan Repayment", AuthenticationCode = "", ReferenceID = referenceId });
                    if (alr.RequestStatus == "true")
                    {
                        _log.ServiceMsg($"Lien Successfully placed on Customer: {solc.CustomerId} Sofri Account {solc.SofriAccountNumber} in preparation for loan repayment collection { solc.LoanId }!", "INFO");
                        await ls.CreateNewLienRecord(new LienHistory()
                        {
                            Date = DateTime.Today,
                            CustomerId = solc.CustomerId,
                            AccountNumber = solc.SofriAccountNumber,
                            LienAmount = accountbalance,
                            LienReferenceId = referenceId,
                            LoanReferenceId = solc.LoanId,
                            LienStatus = "ACTIVE",
                            RemoveDate = DateTime.Today.AddDays(1)
                        });
                    }

                   
                }
            }
           
        }
    }
}
