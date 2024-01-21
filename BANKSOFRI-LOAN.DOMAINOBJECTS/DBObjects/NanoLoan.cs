using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects
{
    public partial class NanoLoan
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string LoanReferenceId { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string SofriAccountNumber { get; set; }
        public int Tenure { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal InterestRate { get; set; }
        public decimal InterestAmount { get; set; }
        public decimal TotalPayable { get; set; }
        public DateTime RepaymentStartDate { get; set; }
        public decimal LoanBalance { get; set; }
        public DateTime RepaymentEndDate { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
        public string NextRepaymentDate { get; set; }
        public bool IsOverDue { get; set; }
        public string PhoneNumber { get; set; }
        public string BankOneReferenceId { get; set; }
        public DateTime? ValidTill { get; set; }
    }
}
