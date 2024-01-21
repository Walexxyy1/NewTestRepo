using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Logger;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO.Collection;
using BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Logic
{
    public class CollectionService : ICollect
    {
        ILoanService ls;
        IPaystack pi;
        IBone bi;
        ICustomer _cs;
        private readonly IConfiguration _config;

        private readonly ILogs _log;
        public CollectionService(ILoanService _ls, IPaystack _pi, IBone _bi, ICustomer cs, IConfiguration config, ILogs log)
        {
            ls = _ls;
            pi = _pi;
            bi = _bi;
            _cs = cs;
            _config = config;
            _log = log;
        }

        public async Task<string> StartRepaymentCollection()
        {
            try
            {
                _log.ServiceMsg($"Searching for Loan Repayments Due today at ({DateTime.Now})!", "INFO");
                IEnumerable<NanoLoan> dueloans = ls.GetAllRepaymentDueToday();
                _log.ServiceMsg($"Total loan Repayments due today {DateTime.Now} found ({dueloans.Count()})!", "INFO");
                foreach (NanoLoan n in dueloans)
                {
                    GetCustomerAccounts gca = bi.GetAccountBalance(n.CustomerId);
                    AccountDetails acct = gca.Accounts.Where(x => x.NUBAN == n.SofriAccountNumber).FirstOrDefault();
                    NanoLoanRepaymentSchedule rd = ls.GetRepaymentSchedule(n.LoanReferenceId);
                    decimal sofridiscount = decimal.Parse(_config.GetSection("SofriDiscount").Value);
                    decimal discountedAmt = sofridiscount / 100 * rd.InterestDue;
                    decimal discountedPayment = rd.InterestDue - discountedAmt;
                    decimal totalrepayment = rd.PrincipalDue + discountedPayment;
                    _log.ServiceMsg($"Checking if customer Sofri Account Balance is sufficient for Repayment of loan with Id {n.LoanReferenceId}", "INFO");
                    if (await _cs.IsSofriAccountBalanceSufficient(n.CustomerId, n.SofriAccountNumber, totalrepayment))
                    {
                        _log.ServiceMsg($"Customer {n.CustomerId} Account Number {n.SofriAccountNumber} balance is funded for the repayment of { n.LoanReferenceId }!", "INFO");

                        //principalresponse = await bi.LoanPrincipalCollectionFromSofriAccount(new LoanCollectionRequestDTO()
                        //{
                        //    AccountNumber = n.SofriAccountNumber,
                        //    Amount = rd.PrincipalDue.ToString(),
                        //    Narration = "LOAN PRINCIPAL REPAYMENT",
                        //    RetrievalReference = Guid.NewGuid().ToString().Substring(0, 10).ToUpper()
                        //});
                        //interestresponse = await bi.LoanInterestCollectionFromSofriAccount(new LoanCollectionRequestDTO()
                        //{
                        //    AccountNumber = n.SofriAccountNumber,
                        //    Amount = discountedPayment.ToString(),
                        //    Narration = "LOAN INTEREST PAYMENT",
                        //    RetrievalReference = Guid.NewGuid().ToString().Substring(0, 10).ToUpper()
                        //});

                        //if (principalresponse.IsSuccessful && interestresponse.IsSuccessful)
                        //{
                        //    _log.ServiceMsg("Repayment collected successfully from customer Sofri Account ( " + n.LoanReferenceId + ")!", "INFO");
                        //    totalpaid = rd.PrincipalDue + rd.InterestDue;
                        //    newbalance = n.LoanBalance - totalpaid;
                        //     ls.CreateRepaymentRecord(new NanoLoanRepayment() { Date = DateTime.Now, LoanReferenceId = n.LoanReferenceId, LoanBalance = n.LoanBalance, AmountPaid = discountedPayment, NewBalance = newbalance, RepaymentDiscountRate = sofridiscount, RepaymentDiscountAmount = discountedAmt, RepaymentReference = "P=" + principalresponse.Reference + " I=" + interestresponse.Reference, Remark = "Repayment collected successfully from Sofri Account" });
                        //    if (newbalance <= 0)
                        //    {
                        //        n.NextRepaymentDate = rd.RepaymentDate.ToShortDateString();
                        //        n.Status = "CLOSED";
                        //        n.Remark = "Repayment Completed! Loan Account closed!";
                        //    }
                        //    else
                        //    {
                        //        n.NextRepaymentDate = rd.RepaymentDate.AddDays(30).ToShortDateString();
                        //    }
                        //     ls.UpdateLoanNextPaymentDate(n);

                        //    rd.Status = "PAID";
                        //     ls.UpdateLoanRepaymentSchedule(rd);
                        //}
                        //else
                        //{
                        //    await ExternalCollection(n, rd, rd.InterestDue);
                        //}
                    }
                    else
                    {
                        _log.ServiceMsg($"Checking customer Sofri Account Balance is Insufficient for loan repayment. Calling Paystack API to fund customer sofri account {n.LoanReferenceId}", "INFO");
                        await ExternalCollection(n, rd, (decimal)acct.AvailableBalance);
                    }
                }
                _log.Logger(" Loan Repayment Collection for today ( " + DateTime.Today + ") Completed Successfully!", "INFO");
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on StartRepaymentCollection method! Details: " + ex.Message, "Error");
            }
            return null;
        }

        public async Task<string> StartFailedRepaymentCollection()
        {
            try
            {
                _log.ServiceMsg("Searching for NanoLoan customers with negative account balance", "INFO");
                IEnumerable<CollectionDTO> dueloans = await GetRecords();
                foreach (CollectionDTO n in dueloans)
                {
                    NanoLoanRepaymentSchedule rd = ls.GetOverdueRepaymentSchedule(n.LoanReferenceId);
                    _log.ServiceMsg($"Calling Paystack API to fund customer {n.CustomerID} sofri account with negative balance for loan repayment {n.LoanReferenceId}", "INFO");
                    if (rd != null)
                    {
                        await ExternalCollectionForFailedCollection(n, rd, n.AccountBalance);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on StartFailedRepaymentCollection method! Details: " + ex.Message, "Error");
            }
            return null;
        }

        public async Task<List<CollectionDTO>> GetRecords()
        {
            List<CollectionDTO> nlist = new List<CollectionDTO>();
            try
            {
                _log.ServiceMsg("Searching for NanoLoan customers with negative account balance", "INFO");
                IEnumerable<NanoLoan> dueloans = ls.GetAllUnpaidRepaymentDue();
                int totalcustomer = 0;
                foreach (NanoLoan n in dueloans)
                {
                    if (DateTime.Parse(n.NextRepaymentDate) < DateTime.Today)
                    {
                        GetCustomerAccounts gca = bi.GetAccountBalance(n.CustomerId);
                        AccountDetails acct = gca.Accounts.Where(x => x.NUBAN == n.SofriAccountNumber).FirstOrDefault();
                        if (acct.AvailableBalance < 1)
                        {
                            totalcustomer++;
                            nlist.Add(new CollectionDTO() { Date = n.Date, CustomerID = n.CustomerId, CustomerName = n.CustomerName, Email = n.Email, SofriAccountNumber = n.SofriAccountNumber, LoanReferenceId = n.LoanReferenceId, AccountBalance = (decimal)acct.AvailableBalance });
                        }
                    }
                }
                _log.ServiceMsg($"Total nanoloan customers with negative blance as today {DateTime.Today.ToShortDateString()} is ({ totalcustomer} )!", "INFO");
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on StartFailedRepaymentCollection method! Details: " + ex.Message, "Error");
            }
            return nlist;
        }

        async Task ExternalCollection(NanoLoan n, NanoLoanRepaymentSchedule rd, decimal accountbalance)
        {
            InterestResponse ir = await pi.CalculateRepaymentInterest(n.Email, rd.InterestDue);
            decimal totalpaiddue = rd.PrincipalDue + ir.rateAmount;
            decimal finalAmt = totalpaiddue - accountbalance;
            string reply = await pi.Charge(new ChargeCardObject() { Email = n.Email, Amount = finalAmt, CustomerId = n.CustomerId, LoanRefId = rd.LoanReferenceId }, "LOAN COLLECTION");
            _log.ServiceMsg($"Paystack Response for Customer Account Debit for loan {n.LoanReferenceId} repayment { reply }", "INFO");
            if (reply == "SUCCESSFUL")
            {
                _log.ServiceMsg($"Repayment successfully taken from Customer Account via Paystack { n.LoanReferenceId }!", "INFO");
                LoanDisbursementResponse acctCredResponse = await bi.CreditCustomerSofriAccountForRepayment(new LoanCollectionRequestDTO()
                {
                    AccountNumber = n.SofriAccountNumber,
                    Amount = totalpaiddue.ToString(),
                    Narration = "Fund Account for Repayment",
                    RetrievalReference = Guid.NewGuid().ToString().Substring(0, 10).ToUpper()
                });
                if (acctCredResponse.IsSuccessful)
                {
                    _log.ServiceMsg($"Customer: {n.CustomerId} Sofri Account {n.SofriAccountNumber} funded successfully in preparation for loan repayment collection { n.LoanReferenceId }!", "INFO");
                    string referenceId = Guid.NewGuid().ToString().Substring(0, 10).ToUpper();
                    AccountlienResponse alr = bi.PlaceLienOnAccount(new AccountlienRequestObject() { AccountNo = n.SofriAccountNumber, Amount = totalpaiddue.ToString(), Reason = "Loan Repayment", AuthenticationCode = "", ReferenceID = referenceId });
                    if (alr.RequestStatus == "true")
                    {
                        _log.ServiceMsg($"Lien Successfully placed on Customer: {n.CustomerId} Sofri Account {n.SofriAccountNumber} in preparation for loan repayment collection { n.LoanReferenceId }!", "INFO");
                        await ls.CreateNewLienRecord(new LienHistory()
                        {
                            Date = DateTime.Today,
                            CustomerId = n.CustomerId,
                            AccountNumber = n.SofriAccountNumber,
                            LienAmount = totalpaiddue,
                            LienReferenceId = referenceId,
                            LoanReferenceId = n.LoanReferenceId,
                            LienStatus = "ACTIVE",
                            RemoveDate = DateTime.Today.AddDays(1)
                        });
                    }

                    //principalresponse = await bi.LoanPrincipalCollectionFromSofriAccount(new LoanCollectionRequestDTO()
                    //{
                    //    AccountNumber = n.SofriAccountNumber,
                    //    Amount = rd.PrincipalDue.ToString(),
                    //    Narration = "LOAN PRINCIPAL REPAYMENT",
                    //    RetrievalReference = Guid.NewGuid().ToString().Substring(0, 10).ToUpper()
                    //});
                    //interestresponse = await bi.LoanInterestCollectionFromSofriAccount(new LoanCollectionRequestDTO()
                    //{
                    //    AccountNumber = n.SofriAccountNumber,
                    //    Amount = discountedPayment.ToString(),
                    //    Narration = "LOAN INTEREST PAYMENT",
                    //    RetrievalReference = Guid.NewGuid().ToString().Substring(0, 10).ToUpper()
                    //});

                    //if (principalresponse.IsSuccessful && interestresponse.IsSuccessful)
                    //{
                    //    decimal totalpay = rd.PrincipalDue + rd.InterestDue;
                    //    decimal newloanbalance = n.LoanBalance - totalpay;
                    //    ls.CreateRepaymentRecord(new NanoLoanRepayment() { Date = DateTime.Now, LoanReferenceId = n.LoanReferenceId, LoanBalance = n.LoanBalance, AmountPaid = discountedPayment, NewBalance = newloanbalance, RepaymentDiscountRate = 0, RepaymentDiscountAmount = 0, RepaymentReference = "P=" + principalresponse.Reference + " I=" + interestresponse.Reference, Remark = "Repayment collected successfully from Sofri Account" });
                    //    if (newloanbalance <= 0)
                    //    {
                    //        n.NextRepaymentDate = rd.RepaymentDate.ToShortDateString();
                    //        n.Status = "CLOSED";
                    //        n.Remark = "Repayment Completed! Loan Account closed!";

                    //    }
                    //    else
                    //    {
                    //        n.NextRepaymentDate = rd.RepaymentDate.AddDays(30).ToShortDateString();
                    //    }
                    //    ls.UpdateLoanNextPaymentDate(n);

                    //    rd.Status = "PAID";
                    //    ls.UpdateLoanRepaymentSchedule(rd);
                    //}
                }
            }
            else
            {
                rd.Status = "OVERDUE";
                rd.DaysOverDue = +1;
                ls.UpdateLoanRepaymentSchedule(rd);

                n.IsOverDue = true;
                ls.UpdateLoanNextPaymentDate(n);

                _log.ServiceMsg("Loan Repayment collection failed! Now marked as Overdue (" + n.LoanReferenceId + ")!", "INFO");
            }
        }

        async Task ExternalCollectionForFailedCollection(CollectionDTO n, NanoLoanRepaymentSchedule rd, decimal accountbalance)
        {
            InterestResponse ir = await pi.CalculateRepaymentInterest(n.Email, rd.InterestDue);
            decimal totalpaiddue = rd.PrincipalDue + ir.rateAmount;
            decimal finalAmt = totalpaiddue - accountbalance;
            string reply = await pi.Charge(new ChargeCardObject() { Email = n.Email, Amount = finalAmt, CustomerId = n.CustomerID, LoanRefId = rd.LoanReferenceId }, "LOAN COLLECTION");
            _log.ServiceMsg($"Paystack Response for Customer Account Debit for loan {n.LoanReferenceId} repayment { reply }", "INFO");
            if (reply == "SUCCESSFUL")
            {
                _log.ServiceMsg($"Repayment successfully taken from Customer Account via Paystack { n.LoanReferenceId }!", "INFO");
               await UpdateCollection(n, finalAmt.ToString());
            }
            if (reply == "Insufficient Funds")
            {
                string resp = await pi.PartialDebit(new ChargeCardObject() { Email = n.Email, Amount = finalAmt, CustomerId = n.CustomerID, LoanRefId = rd.LoanReferenceId }, "LOAN COLLECTION");

                if (resp == "SUCCESSFUL")
                {
                    _log.ServiceMsg($"Repayment successfully taken from Customer Account via Paystack { n.LoanReferenceId }!", "INFO");
                    await UpdateCollection(n, finalAmt.ToString());
                }
            }
        }
        public async Task UpdateCollection(CollectionDTO n, string totaldue)
            {
            LoanDisbursementResponse acctCredResponse = await bi.CreditCustomerSofriAccountForRepayment(new LoanCollectionRequestDTO()
            {
                AccountNumber = n.SofriAccountNumber,
                Amount = totaldue,
                Narration = "Fund Account for Repayment",
                RetrievalReference = Guid.NewGuid().ToString().Substring(0, 10).ToUpper()
            });
            if (acctCredResponse.IsSuccessful)
            {
                _log.ServiceMsg($"Customer: {n.CustomerID} Sofri Account {n.SofriAccountNumber} funded successfully in preparation for loan repayment collection { n.LoanReferenceId }!", "INFO");
                string referenceId = Guid.NewGuid().ToString().Substring(0, 10).ToUpper();
                AccountlienResponse alr = bi.PlaceLienOnAccount(new AccountlienRequestObject() { AccountNo = n.SofriAccountNumber, Amount = totaldue.ToString(), Reason = "Loan Repayment", AuthenticationCode = "", ReferenceID = referenceId });
                if (alr.RequestStatus == "true")
                {
                    _log.ServiceMsg($"Lien Successfully placed on Customer: {n.CustomerID} Sofri Account {n.SofriAccountNumber} in preparation for loan repayment collection { n.LoanReferenceId }!", "INFO");
                    await ls.CreateNewLienRecord(new LienHistory()
                    {
                        Date = DateTime.Today,
                        CustomerId = n.CustomerID,
                        AccountNumber = n.SofriAccountNumber,
                        LienAmount = decimal.Parse(totaldue),
                        LienReferenceId = referenceId,
                        LoanReferenceId = n.LoanReferenceId,
                        LienStatus = "ACTIVE",
                        RemoveDate = DateTime.Today.AddDays(1)
                    });
                }
            }
        }

        public void SyncLoanStatusWithBankOne()
        {
            IEnumerable<NanoLoan> loans = ls.GetLoansWithStatuses();
            if(loans.Count() > 0)
            {
                foreach (NanoLoan loan in loans)
                {
                    string loanstatus = bi.GetBankOneLoanAccountStatus(loan.CustomerId, loan.LoanReferenceId);
                    if(loanstatus != null)
                    {
                        ls.UpdateNanoLoanStatus(loan.LoanReferenceId, loanstatus);
                    }
                    if(loan.ValidTill.ToString() != "")
                    {
                        if (DateTime.Parse(loan.ValidTill.ToString()) < DateTime.Now && loan.Status == "PENDING")
                        {
                            ls.UpdateNanoLoanStatus(loan.LoanReferenceId, "FAILED");
                        }
                        
                    }
                }
            }
        }
}
}
