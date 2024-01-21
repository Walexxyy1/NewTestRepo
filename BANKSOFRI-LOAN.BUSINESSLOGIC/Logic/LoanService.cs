using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Logger;
using BANKSOFRI_LOAN.DATALAYER.Repositories;
using BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Logic
{
    public class LoanService : ILoanService
    {
        //IRepositoryOld repos;        
        ILogs _log;
        ApiauthorisationContext _ctx;
        public LoanService(ILogs log, ApiauthorisationContext ctx)
        {
            
            _log = log;
            _ctx = ctx;
        }
       
        decimal totalInterest = 0;
      
        public List<NanoLoan> GetCustomerLoans(string customerId)
        {
            List<NanoLoan> n = new List<NanoLoan>();
            try
            {
                n = _ctx.NanoLoans.Where(x => x.CustomerId == customerId).OrderByDescending(x => x.Id).ToList();                
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on GetCustomerLoans method! Details: " + ex.Message, "Error");
            }
            return n;
        }
       
        public async Task<long> CreateNewLienRecord(LienHistory lh)
        {
            long resp = 0;
            try
            {
                await _ctx.LienHistories.AddAsync(lh);
                resp = await _ctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
               _log.Logger("An error occured on CreateNewLienRecord method! Details: " + ex.Message, "Error");
            }
            return resp;
        }

        public long UpdateLienHistoryStatus(string customerId, string loanRefId, string removedate)
        {
            long resp = 0;
            try
            {
                LienHistory lh = GetLienHistory(customerId, loanRefId, removedate);
                if (lh != null)
                {
                    lh.LienStatus = "INACTIVE";
                    _ctx.LienHistories.Update(lh);
                    resp = _ctx.SaveChanges();                   
                }
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on UpdateLienHistoryStatus method! Details: " + ex.Message, "Error");
            }
            return resp;
        }

        public LienHistory GetLienHistory(string customerId, string loanRefId, string removedate)
        {
            LienHistory history = new LienHistory();
            try
            {
                history = _ctx.LienHistories.Where(x => x.CustomerId == customerId && x.LoanReferenceId == loanRefId && x.RemoveDate == DateTime.Parse(removedate) && x.LienStatus == "ACTIVE").FirstOrDefault(); 
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on GetLienHistory method! Details: " + ex.Message, "Error");
            }
            return history;
        }

        public List<LienHistory> GetAllLienHistoryDue(string removedate)
        {
            List<LienHistory> history = new List<LienHistory>();
            try
            {
                history = _ctx.LienHistories.Where(x => x.RemoveDate == DateTime.Parse(removedate) && x.LienStatus == "ACTIVE").ToList();
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on GetAllLienHistoryDue method! Details: " + ex.Message, "Error");
            }
            return history;
        }

        public long CreateRepaymentSchedule(string refId, int tenor, decimal LoanAmount, decimal rate)
        {
            long resp = 0;
            try
            {
                totalInterest = rate / 100 * LoanAmount;
                decimal repaymentPrincipal = LoanAmount / tenor;
                decimal repaymentInterest = totalInterest / tenor;
                if (tenor <= 30)
                {
                    _ctx.NanoLoanRepaymentSchedules.Add(new NanoLoanRepaymentSchedule() { Date = DateTime.Now, LoanReferenceId = refId, RepaymentDate = DateTime.Now.AddDays(tenor), PrincipalDue = LoanAmount, InterestDue = totalInterest, DaysOverDue = 0, Status = "UNPAID" });
                    resp = _ctx.SaveChanges();
                }
                else
                {
                    resp = Create(refId, tenor, LoanAmount, rate);
                }

            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on CreateRepaymentSchedule method! Details: " + ex.Message, "Error");
            }
            return resp;
        }
        public long UpdateLoanStatus(string refId, string Status, string BankOneRef, string remark)
        {
            long resp = 0;
            try
            {
                NanoLoan n = _ctx.NanoLoans.Where(x => x.LoanReferenceId == refId).FirstOrDefault();
                if (n != null)
                {
                    n.Status = Status;
                    n.Remark = remark;
                    n.BankOneReferenceId = BankOneRef;
                    _ctx.NanoLoans.Update(n);
                    resp = _ctx.SaveChanges();
                    if (resp > 0)
                    {
                        UpdateLoanOfferStatus(refId, "ACCEPTED");
                    }
                }

            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on UpdateLoanStatus method! Details: " + ex.Message, "Error");
            }
            return resp;
        }

        public void UpdateLoanOfferStatus(string refId, string status)
        {
            long resp = 0;
            try
            {
                // ProvenirDecisionData pdd = repo.Query<ProvenirDecisionData>("SELECT * FROM ProvenirDecisionData WHERE ReferenceID=@refno", new { refno = refId });
                ProvenirDecisionData pdd = _ctx.ProvenirDecisionData.Where(x => x.ReferenceId == refId).FirstOrDefault();   
                if (pdd != null)
                {
                    pdd.Status = status;
                    _ctx.ProvenirDecisionData.Update(pdd);
                    resp = _ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on UpdateLoanOfferStatus method! Details: " + ex.Message, "Error");
            }
        }

        public async Task<long> CreateNewLoan(NanoLoan n)
        {
            long resp = 0;
            try
            {
                _ctx.NanoLoans.Add(n);
                resp = _ctx.SaveChanges();
            }
            catch (Exception ex)
            {
               _log.Logger("An error occured on CreateNewLoan method! Details: " + ex.Message, "Error");
            }
            return resp;
        }

        public async Task<bool> IsReferenceNumberValid(string refnum)
        {
            bool resp = false;
            try
            {                
                NanoLoan n = _ctx.NanoLoans.Where(x => x.LoanReferenceId == refnum).FirstOrDefault();
                if (n == null)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                _log.Logger($"An error occured on IsReferenceNumberValid method! Details: { ex.Message}", "ERROR");
            }
            return resp;
        }

        public async Task<decimal> GetInterestRate(string emptype, int tenor, string risk)
        {
            Repository rp = new Repository();
            decimal rates = 0;
            try
            {
                //Rate r = rp.Query<Rate>("SELECT * FROM Rates WHERE EmploymentStatus=@es AND RiskLevel= @rl AND Tenor=@tn", new { es = emptype, rl = risk, tn = tenor });
                Rate r = _ctx.Rates.Where(x => x.EmploymentStatus == emptype && x.RiskLevel == risk && x.Tenor == tenor).FirstOrDefault();
                if (r != null)
                {
                    rates = (decimal)r.InterestRate;
                }
            }
            catch (Exception ex) 
            { 
                _log.Logger("An error occured on GetInterestRate method! Details: " + ex.Message, "Error"); 
            }

            return rates;
        }
        public long Create(string reff, int tenor, decimal LoanAmount, decimal rate)
        {
            long resp = 0;
            try
            {
                int j = tenor / 30;
                totalInterest = rate / 100 * LoanAmount;
                decimal repaymentPrincipal = LoanAmount / j;
                decimal repaymentInterest = totalInterest / j;

                for (int i = 0; i == j; i++)
                {
                    int days = 0;
                    if (i == 2)
                    {
                        days = 60;
                    }
                    else if (i == 2)
                    {
                        days = 90;
                    }
                    else
                    {
                        days = tenor;
                    }
                    _ctx.NanoLoanRepaymentSchedules.Add(new NanoLoanRepaymentSchedule() { Date = DateTime.Now, LoanReferenceId = reff, RepaymentDate = DateTime.Now.AddDays(days), PrincipalDue = repaymentPrincipal, InterestDue = repaymentPrincipal, DaysOverDue = 0, Status = "PENDING" });
                    resp += _ctx.SaveChanges();
                }
            }
            catch (Exception ex) { _log.Logger("An error occured on Create method! Details: " + ex.Message, "Error"); }

            return resp;
        }
        public IEnumerable<NanoLoan> GetAllUnDisburseLoans()
        {
            IEnumerable<NanoLoan> n = new List<NanoLoan>();
            try
            {
                n = _ctx.NanoLoans.Where(x => x.Status == "PENDING" || x.Status == "RLD").ToList();
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on GetAllUnDisburseLoan method! Details: " + ex.Message, "Error");
            }
            return n;
        }

        public IEnumerable<NanoLoan> GetAllRepaymentDueToday()
        {
            IEnumerable<NanoLoan> n = new List<NanoLoan>();
            try
            {
                 n = _ctx.NanoLoans.Where(x => x.NextRepaymentDate == DateTime.Today.ToShortDateString() && x.Status == "ACTIVE").ToList();
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on GetAllRepaymentDueToday method! Details: " + ex.Message, "Error");
            }
            return n;
        }

        public IEnumerable<SofriOtherLoanCustomer> GetSofriOtherloanCustomers()
        {
            IEnumerable<SofriOtherLoanCustomer> n = new List<SofriOtherLoanCustomer>();
            try
            {               
                 n = _ctx.SofriOtherLoanCustomers.ToList();
            }
            catch (Exception ex)
            {
                _log.Logger($"An error occured on GetSofriOtherloanCustomers method! Details: {ex.Message}", "Error");
            }
            return n;
        }

        public IEnumerable<NanoLoan> GetAllUnpaidRepaymentDue()
        {
            IEnumerable<NanoLoan> n = new List<NanoLoan>();
            try
            {
                n = _ctx.NanoLoans.Where(x => x.Status == "ACTIVE").ToList();
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on GetAllUnpaidRepaymentDue method! Details: " + ex.Message, "Error");
            }
            return n;
        }

        public IEnumerable<NanoLoan> GetAllRepaymentDatesGettingDue(DateTime repaymentDate)
        {
            IEnumerable<NanoLoan> n = new List<NanoLoan>();
            try
            {
                n = _ctx.NanoLoans.Where(x => x.NextRepaymentDate == repaymentDate.ToShortDateString() && x.Status == "ACTIVE").ToList();
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on GetAllRepaymentGettingDue method! Details: " + ex.Message, "Error");
            }
            return n;
        }
        public IEnumerable<NanoLoan> GetAllLoansWithOverDueStatus()
        {
            IEnumerable<NanoLoan> n = new List<NanoLoan>();
            try
            {
                n = _ctx.NanoLoans.Where(x => x.IsOverDue == true && x.Status == "ACTIVE").ToList();
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on GetAllLoansWithOverDueStatus method! Details: " + ex.Message, "Error");
            }
            return n;
        }

        public List<NanoLoanRepaymentSchedule> GetAllLoanRepaymentSchedule(string loanRefId)
        {
            List<NanoLoanRepaymentSchedule> schedule = new List<NanoLoanRepaymentSchedule>();
            try
            {
               schedule = _ctx.NanoLoanRepaymentSchedules.Where(x => x.LoanReferenceId == loanRefId).ToList();
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on GetAllLoanRepaymentSchedule method! Details: " + ex.Message, "Error");
            }
            return schedule;
        }

        public NanoLoanRepaymentSchedule GetRepaymentSchedule(string loanRefId)
        {
            NanoLoanRepaymentSchedule curepay = new NanoLoanRepaymentSchedule();
            try
            {                
                curepay = _ctx.NanoLoanRepaymentSchedules.Where(x => x.LoanReferenceId == loanRefId && x.RepaymentDate == DateTime.Today && x.Status == "UNPAID").FirstOrDefault();               
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on GetRepaymentSchedule method! Details: " + ex.Message, "Error");
            }
            return curepay;
        }
        public NanoLoanRepaymentSchedule GetOverdueRepaymentSchedule(string loanRefId)
        {
            NanoLoanRepaymentSchedule curepay = new NanoLoanRepaymentSchedule();
            try
            {
                curepay = _ctx.NanoLoanRepaymentSchedules.Where(x => x.LoanReferenceId == loanRefId && x.Status == "OVERDUE").FirstOrDefault();                
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on GetRepaymentSchedule method! Details: " + ex.Message, "Error");
            }
            return curepay;
        }
        public NanoLoanRepaymentSchedule GetRepaymentScheduleDetails(int Id)
        {
            NanoLoanRepaymentSchedule curepay = new NanoLoanRepaymentSchedule();
            try
            {
                curepay = _ctx.NanoLoanRepaymentSchedules.Where(x => x.Id == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on GetRepaymentScheduleDetails method! Details: " + ex.Message, "Error");
            }
            return curepay;
        }

        public NanoLoanRepaymentSchedule GetRepaymentScheduleDetails(string loanRefId, DateTime d)
        {
            NanoLoanRepaymentSchedule odrepay = new NanoLoanRepaymentSchedule();
            try
            {
                odrepay = _ctx.NanoLoanRepaymentSchedules.Where(x => x.LoanReferenceId == loanRefId && x.RepaymentDate == d).FirstOrDefault();               
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on GetRepaymentScheduleDetails method! Details: " + ex.Message, "Error");
            }
            return odrepay;
        }

        public List<NanoLoanRepaymentSchedule> GetRepaymentOverDueSchedule(string loanRefId)
        {
            List<NanoLoanRepaymentSchedule> odrepay = new List<NanoLoanRepaymentSchedule>();
            try
            {
                odrepay = _ctx.NanoLoanRepaymentSchedules.Where(x => x.LoanReferenceId == loanRefId && x.Status == "OVERDUE").ToList();               
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on GetRepaymentOverDueSchedule method! Details: " + ex.Message, "Error");
            }
            return odrepay;
        }

        public long UpdateLoanNextPaymentDate(NanoLoan n)
        {
            long resp = 0;
            try
            {
                _ctx.NanoLoans.Update(n);
                resp = _ctx.SaveChanges(); // repo.Update<NanoLoan>(n);
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on UpdateLoanNextPaymentDate method! Details: " + ex.Message, "Error");
            }
            return resp;
        }

        public long UpdateLoanRepaymentSchedule(NanoLoanRepaymentSchedule nrs)
        {
            long resp = 0;
            try
            {
                _ctx.NanoLoanRepaymentSchedules.Update(nrs);
                resp = _ctx.SaveChanges();
                //resp = repo.Update<NanoLoanRepaymentSchedule>(nrs);
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on UpdateLoanRepaymentSchedule method! Details: " + ex.Message, "Error");
            }
            return resp;
        }

        public long CreateRepaymentRecord(NanoLoanRepayment nr)
        {
            long resp = 0;
            try
            {
                _ctx.NanoLoanRepayments.Add(nr);
                resp = _ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on CreateRepaymentRecord method! Details: " + ex.Message, "Error");
            }
            return resp;
        }

        public NanoLoan GetNanoLoan(string loanId)
        {
            NanoLoan n = new NanoLoan();
            try
            {
               // n = repo.Query<NanoLoan>("SELECT * FROM NanoLoans WHERE LoanReferenceId=@refId", new { refId = loanId});
                n = _ctx.NanoLoans.Where(x => x.LoanReferenceId == loanId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on GetNanoLoan method! Details: " + ex.Message, "Error");
            }
            return n;
        }
        public void UpdateNanoLoanStatus(string loanId, string Status)
        {           
            try
            {
                NanoLoan n = GetNanoLoan(loanId);
                if (n != null)
                {
                    n.Status = Status;
                    _ctx.Update(n);
                    _ctx.SaveChanges();
                   // repo.Update(n);
                }                
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on UpdateNanoLoanStatus method! Details: " + ex.Message, "Error");
            }            
        }

        public IEnumerable<NanoLoan> GetLoansWithStatuses()
        {
            IEnumerable<NanoLoan> n = new List<NanoLoan>();
            try
            {
                n = _ctx.NanoLoans.Where(x => x.Status == "PENDING" || x.Status =="RLD" || x.Status == "ACTIVE").ToList();
               // n = (IEnumerable<NanoLoan>)repo.QueryList<NanoLoan>("SELECT * FROM NanoLoans WHERE Status=@stat OR Status=@stats", new { stat = "PENDING", stats = "ACTIVE" });
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on GetLoansWithStatuses method! Details: " + ex.Message, "Error");
            }
            return n;
        }

        public IEnumerable<NanoLoan> GetUnproccessedPendingLoans()
        {
            IEnumerable<NanoLoan> n = new List<NanoLoan>();
            try
            {
                n = _ctx.NanoLoans.Where(x => x.Status == "PENDING" && x.ValidTill < DateTime.Now).ToList();
            }
            catch (Exception ex)
            {
                _log.Logger("An error occured on GetUnproccessedPendingLoans method! Details: " + ex.Message, "Error");
            }
            return n;
        }
    }
}

