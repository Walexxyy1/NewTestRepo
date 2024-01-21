using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO;
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
   public class LiquidationService : ILiquidate
    {
        ILoanService ls;
        IPaystack pi;
        IBone bi;
        ICustomer _cs;
        private readonly IConfiguration _config;
        ILogs _log;
        
        public LiquidationService(ILoanService _ls, IPaystack _pi, IBone _bi, IConfiguration config, ICustomer cs, ILogs log)
        {
            ls = _ls;
            pi = _pi;
            bi = _bi;
            _config = config;
            _cs = cs;
            _log = log;
        }
        public async Task<ResponseObject> FullLiquidation(FullLoanLiquidationDTO fld)
        {
            ResponseObject respo = new ResponseObject();
            try {
                List<NanoLoan> allloans =  ls.GetCustomerLoans(fld.CustomerId);
                NanoLoan n = allloans.Where(x => x.LoanReferenceId == fld.LoanId).FirstOrDefault();
                if(n != null)
                {
                    if(n.Status == "ACTIVE")
                    {
                       // decimal liquidPrincipal = n.LoanBalance;
                        decimal interestRate = n.InterestRate;
                        decimal liquidInterestAmount = interestRate / 100 * n.LoanAmount;
                        decimal principalbalance = n.LoanBalance - liquidInterestAmount;
                        string cashbackrate = _config.GetSection("LiquidationCashbackRate").Value;
                        decimal cashback = CalculateCashBack(liquidInterestAmount, n.IsOverDue, cashbackrate);
                        NanoLoanRepaymentSchedule rd =  ls.GetRepaymentSchedule(n.LoanReferenceId);
                        if (await _cs.IsSofriAccountBalanceSufficient(fld.CustomerId, n.SofriAccountNumber, n.LoanBalance))
                        {
                            LoanDisbursementResponse principalresponse = await bi.LoanPrincipalCollectionFromSofriAccount(new LoanCollectionRequestDTO()
                            {
                                AccountNumber = n.SofriAccountNumber,
                                Amount = principalbalance.ToString(),
                                Narration = "LOAN LIQUIDATION PRINCIPAL REPAYMENT",
                                RetrievalReference = Guid.NewGuid().ToString().Substring(0, 10).ToUpper()
                            });
                            LoanDisbursementResponse interestresponse = await bi.LoanInterestCollectionFromSofriAccount(new LoanCollectionRequestDTO()
                            {
                                AccountNumber = n.SofriAccountNumber,
                                Amount = liquidInterestAmount.ToString(),
                                Narration = "LOAN LIQUIDATION INTEREST PAYMENT",
                                RetrievalReference = Guid.NewGuid().ToString().Substring(0, 10).ToUpper()
                            });
                            if (principalresponse.IsSuccessful && interestresponse.IsSuccessful)
                            {
                               await bi.DisburseCashBackViaBankOne(new LoanCollectionRequestDTO()
                                {
                                    AccountNumber = n.SofriAccountNumber,
                                    Amount = cashback.ToString(),
                                    Narration = "CASH BACK ON LOAN LIQUIDATION",
                                    RetrievalReference = Guid.NewGuid().ToString().Substring(0, 10).ToUpper()
                                });
                                decimal totalpay = n.LoanBalance;
                                decimal newloanbalance = 0;
                                 ls.CreateRepaymentRecord(new NanoLoanRepayment() { Date = DateTime.Now, LoanReferenceId = n.LoanReferenceId, LoanBalance = 0, AmountPaid = totalpay, NewBalance = newloanbalance, RepaymentDiscountRate = decimal.Parse(cashbackrate), RepaymentDiscountAmount = liquidInterestAmount, RepaymentReference = "P=" + principalresponse.Reference + " I=" + interestresponse.Reference, Remark = "Repayment collected successfully from Sofri Account" });
                                if (newloanbalance <= 0)
                                {
                                    n.NextRepaymentDate = DateTime.Now.ToShortTimeString();
                                    n.Status = "LIQUIDATED";
                                    n.Remark = "Loan fully liquidated on " + DateTime.Now;
                                }
                                else
                                {
                                    n.NextRepaymentDate = DateTime.Now.AddDays(30).ToShortTimeString();
                                }
                                 ls.UpdateLoanNextPaymentDate(n);
                                respo = new ResponseObject() { ResponseCode = "00", ResponseMessage = "SUCCESS! Loan liquidation was successful!" };
                            }
                        }
                        else
                        {
                            respo = new ResponseObject() { ResponseCode = "04", ResponseMessage = "Insufficient Account Balance! Please fund your Sofri Account and retry!" };
                        }
                    }
                    else
                    {
                        respo = new ResponseObject() { ResponseCode = "02", ResponseMessage = "The selected loan is not Active" };
                    }
                }
                else
                {
                    respo = new ResponseObject() { ResponseCode = "01", ResponseMessage = "The selected loan was not found!" };
                }
            } catch (Exception ex) { _log.Logger("An error occured on FullLiquidation method! Details: " + ex.Message, "Error"); }
            return respo;
        }

        public async Task<ResponseObject> PartialLiquidation(PartialLoanLiquidationDTO pld)
        {
            ResponseObject respo = new ResponseObject();
            try
            {
                List<NanoLoan> allloans = ls.GetCustomerLoans(pld.CustomerId);
                NanoLoan n = allloans.Where(x => x.LoanReferenceId == pld.LoanId).FirstOrDefault();
                if (n != null)
                {
                    if (n.Status != "CLOSED")
                    {                        
                        decimal interestRate = n.InterestRate;
                        decimal liquidInterestAmount = interestRate / 100 * pld.LiquidationAmount;
                        decimal liquidPrincipal = pld.LiquidationAmount - liquidInterestAmount;
                        string cashbackrate = _config.GetSection("LiquidationCashbackRate").Value;
                        decimal interestpayable = liquidInterestAmount - CalculateCashBack(liquidInterestAmount, n.IsOverDue, cashbackrate);                        
                        NanoLoanRepaymentSchedule rd =  ls.GetRepaymentSchedule(n.LoanReferenceId);
                        if (await _cs.IsSofriAccountBalanceSufficient(pld.CustomerId, n.SofriAccountNumber, pld.LiquidationAmount))
                        {
                            LoanDisbursementResponse principalresponse = await bi.LoanPrincipalCollectionFromSofriAccount(new LoanCollectionRequestDTO()
                            {
                                AccountNumber = n.SofriAccountNumber,
                                Amount = liquidPrincipal.ToString(),
                                Narration = "LOAN LIQUIDATION PRINCIPAL REPAYMENT",
                                RetrievalReference = Guid.NewGuid().ToString().Substring(0, 10).ToUpper()
                            });
                            LoanDisbursementResponse interestresponse = await bi.LoanInterestCollectionFromSofriAccount(new LoanCollectionRequestDTO()
                            {
                                AccountNumber = n.SofriAccountNumber,
                                Amount = interestpayable.ToString(),
                                Narration = "LOAN LIQUIDATION INTEREST PAYMENT",
                                RetrievalReference = Guid.NewGuid().ToString().Substring(0, 10).ToUpper()
                            });

                            if (principalresponse.IsSuccessful && interestresponse.IsSuccessful)
                            {
                                decimal totalpay = liquidPrincipal + interestpayable;
                                decimal newloanbalance = n.LoanBalance - totalpay;
                                 ls.CreateRepaymentRecord(new NanoLoanRepayment() { Date = DateTime.Now, LoanReferenceId = n.LoanReferenceId, LoanBalance = n.LoanBalance, AmountPaid = totalpay, NewBalance = newloanbalance, RepaymentDiscountRate = decimal.Parse(cashbackrate), RepaymentDiscountAmount = interestpayable, RepaymentReference = "P=" + principalresponse.Reference + " I=" + interestresponse.Reference, Remark = "Repayment collected successfully from Sofri Account" });
                                if (newloanbalance <= 0)
                                {
                                    n.NextRepaymentDate = DateTime.Now.ToShortTimeString();
                                    n.Status = "LIQUIDATED";
                                }
                                else
                                {
                                    n.NextRepaymentDate = rd.RepaymentDate.AddDays(30).ToShortTimeString();
                                }
                                 ls.UpdateLoanNextPaymentDate(n);

                                rd.Status = "PAID";
                                 ls.UpdateLoanRepaymentSchedule(rd);
                                respo = new ResponseObject() { ResponseCode = "00", ResponseMessage = "SUCCESS! Loan liquidation was successful!" };
                            }
                            else
                            {
                                respo = new ResponseObject() { ResponseCode = "02", ResponseMessage = "FAILED! Loan Liquidation was not successful! Reason:" + principalresponse.ResponseMessage };
                            }
                        }
                        else
                        {
                            respo = new ResponseObject() { ResponseCode = "02", ResponseMessage = "The selected loan is not Active" };
                        }
                    }
                    else
                    {
                        respo = new ResponseObject() { ResponseCode = "02", ResponseMessage = "The selected loan is not Active" };
                    }
                }
                else
                {
                    respo = new ResponseObject() { ResponseCode = "01", ResponseMessage = "The selected loan was not found!" };
                }
            }
            catch (Exception ex) { _log.Logger("An error occured on FullLiquidation method! Details: " + ex.Message, "Error"); }
            return respo;
        }

        decimal CalculateCashBack(decimal interest, bool isoverdue, string cbrate)
        {           
            if(!isoverdue)
            {
                decimal cashbackRate = decimal.Parse(cbrate);
                decimal cashback = cashbackRate / 100 * interest;
                return cashback;
            }
            else
            {
                return 0;
            }
        }
    }
}
