using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects
{
    public partial class NanoLoanRepaymentSchedule
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string LoanReferenceId { get; set; }
        public DateTime RepaymentDate { get; set; }
        public decimal PrincipalDue { get; set; }
        public decimal InterestDue { get; set; }
        public int DaysOverDue { get; set; }
        public string Status { get; set; }
    }
}
