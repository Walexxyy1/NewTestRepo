using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Logger;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO;
using BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Logic
{
    public class OverDueCollectionService : IOverDue
    {
        IConfiguration _config;
        private readonly IPaystack pi;
        private readonly IBone bi;
        private readonly ILoanService ls;
        ILogs _log;

        public OverDueCollectionService(IConfiguration config, IPaystack pays, IBone bone, ILoanService _ls, ILogs log)
        {
            _config = config;
            pi = pays;
            bi = bone;
            ls = _ls;
            _log = log;
        }
        public async Task<string> StartOverDueCollection()
            {
                try
                {
                _log.ServiceMsg("Searching for OverDue Loans at ( " + DateTime.Now + ")!", "INFO");
                IEnumerable<NanoLoan> overdueloans = ls.GetAllLoansWithOverDueStatus();
                _log.ServiceMsg("Total OverDue Loans found ( " + overdueloans.Count() + ")!", "INFO");
                foreach (NanoLoan n in overdueloans)
                    {
                        string repaymentReference = Guid.NewGuid().ToString().Substring(0, 10).ToUpper();
                        List<NanoLoanRepaymentSchedule> rd = ls.GetRepaymentOverDueSchedule(n.LoanReferenceId);
                        foreach (NanoLoanRepaymentSchedule nr in rd)
                        {
                            decimal totalrepayment = nr.PrincipalDue + nr.InterestDue + OverDuePenalty(nr.Id);
                            _log.ServiceMsg("Calling Paystack API  to debit customer Account for repayment of loan with Id ( " + n.LoanReferenceId + ")!", "INFO");
                            string reply = await pi.Charge(new ChargeCardObject() { Email = n.Email, Amount = totalrepayment }, "LOAN COLLECTION");
                            _log.ServiceMsg("Response from Paystack API ( " + reply + ")!", "INFO");
                        if (reply == "SUCCESSFUL")
                            {
                                decimal totalpaid = nr.PrincipalDue + nr.InterestDue;
                                decimal newbalance = n.LoanBalance - totalpaid;
                                ls.CreateRepaymentRecord(new NanoLoanRepayment() { Date = DateTime.Now, LoanReferenceId = n.LoanReferenceId, LoanBalance = n.LoanBalance, AmountPaid = totalpaid, NewBalance = newbalance, RepaymentDiscountRate = 0, RepaymentDiscountAmount = 0, RepaymentReference = repaymentReference, Remark = "Over Due Loan collected successfully using PAYSTACK" });
                                if (newbalance <= 0)
                                {
                                    n.NextRepaymentDate = DateTime.Now.ToShortDateString();
                                    n.Status = "CLOSED";
                                }
                                else
                                {
                                    n.NextRepaymentDate = nr.RepaymentDate.AddDays(30).ToShortDateString();
                                }
                                n.LoanBalance = newbalance;
                                 ls.UpdateLoanNextPaymentDate(n);

                                nr.Status = "PAID";
                                 ls.UpdateLoanRepaymentSchedule(nr);

                                _log.ServiceMsg("OverDue loan with Id ( " + n.LoanReferenceId + ") successfully collected!", "INFO");
                        }
                            else
                            {
                                 nr.Status = "OVERDUE";
                                 nr.DaysOverDue = +1;
                                 ls.UpdateLoanRepaymentSchedule(nr);

                                 n.IsOverDue = true;
                                 ls.UpdateLoanNextPaymentDate(n);

                                 _log.ServiceMsg("OverDue loan Collection attempt failed for loan with Id ( " + n.LoanReferenceId + ")!", "INFO");
                            }
                        }
                    }
                    _log.Logger("Over due Loans Collection for today ( " + DateTime.Today + ") Completed Successfully!", "INFO");
                }
                catch (Exception ex)
                {
                   _log.Logger("An error occured on StartOverDueCollection method! Details: " + ex.Message, "Error");
                }
                return null;
            }

        decimal OverDuePenalty(int Id)
        {
            decimal penaltyrate = decimal.Parse(_config.GetSection("penaltyfee").Value);
            NanoLoanRepaymentSchedule nrs = ls.GetRepaymentScheduleDetails(Id);
            if (nrs.DaysOverDue > 2)
            {
                decimal penaltyAmt = penaltyrate / 100 * nrs.PrincipalDue;
                return penaltyAmt * nrs.DaysOverDue;
            }
            else
            {
                return 0;
            }
        }
    }
    }

