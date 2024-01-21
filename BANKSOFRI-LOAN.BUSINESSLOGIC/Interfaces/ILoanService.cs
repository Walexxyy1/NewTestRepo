
using BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces
{
   public interface ILoanService
    {        
        NanoLoan GetNanoLoan(string loanId);
        NanoLoanRepaymentSchedule GetOverdueRepaymentSchedule(string loanRefId);
        List<NanoLoan> GetCustomerLoans(string customerId);
        long CreateRepaymentSchedule(string refId, int tenor, decimal LoanAmount, decimal rate);        
        Task<long> CreateNewLoan(NanoLoan n);
        Task<decimal> GetInterestRate(string emptype, int tenor, string risk);
        long Create(string reff, int tenor, decimal LoanAmount, decimal rate);
        IEnumerable<NanoLoan> GetAllUnDisburseLoans();
        IEnumerable<NanoLoan> GetAllRepaymentDueToday();
        IEnumerable<NanoLoan> GetAllRepaymentDatesGettingDue(DateTime repaymentDate);
        IEnumerable<NanoLoan> GetAllLoansWithOverDueStatus();
        NanoLoanRepaymentSchedule GetRepaymentScheduleDetails(int Id);
        NanoLoanRepaymentSchedule GetRepaymentScheduleDetails(string loanRefId, DateTime d);
        NanoLoanRepaymentSchedule GetRepaymentSchedule(string loanRefId);
        List<NanoLoanRepaymentSchedule> GetRepaymentOverDueSchedule(string loanRefId);
        long UpdateLoanNextPaymentDate(NanoLoan n);
        long UpdateLoanRepaymentSchedule(NanoLoanRepaymentSchedule nrs);
        long CreateRepaymentRecord(NanoLoanRepayment nr);
        Task<bool> IsReferenceNumberValid(string refnum);
        Task<long> CreateNewLienRecord(LienHistory lh);
        long UpdateLienHistoryStatus(string customerId, string loanRefId, string removedate);
        List<LienHistory> GetAllLienHistoryDue(string removedate);
        IEnumerable<NanoLoan> GetAllUnpaidRepaymentDue();
        IEnumerable<SofriOtherLoanCustomer> GetSofriOtherloanCustomers();
        long UpdateLoanStatus(string refId, string Status, string BankOneRef, string remark);
        void UpdateNanoLoanStatus(string loanId, string Status);
        IEnumerable<NanoLoan> GetUnproccessedPendingLoans();
        IEnumerable<NanoLoan> GetLoansWithStatuses();
    }
}
