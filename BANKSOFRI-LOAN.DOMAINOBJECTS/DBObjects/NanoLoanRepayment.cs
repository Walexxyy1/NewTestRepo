using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects
{
    public partial class NanoLoanRepayment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string LoanReferenceId { get; set; }
        public decimal LoanBalance { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal NewBalance { get; set; }
        public decimal? RepaymentDiscountRate { get; set; }
        public decimal? RepaymentDiscountAmount { get; set; }
        public string RepaymentReference { get; set; }
        public string Remark { get; set; }
    }
}
