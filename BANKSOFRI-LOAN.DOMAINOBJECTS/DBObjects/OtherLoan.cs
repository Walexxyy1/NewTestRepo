using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects
{
    public partial class OtherLoan
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Bvn { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public decimal LoanAmount { get; set; }
        public string LoanPurpose { get; set; }
        public string LoanType { get; set; }
    }
}
