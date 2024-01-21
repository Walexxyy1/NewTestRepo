using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects
{
    public partial class SofriOtherLoanCustomer
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public decimal LoanRepaymentAmount { get; set; }
        public string SofriAccountNumber { get; set; }
        public string LoanId { get; set; }
    }
}
