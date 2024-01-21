using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects
{
    public partial class NanoLoanRequest
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string EmploymentType { get; set; }
        public decimal Income { get; set; }
        public decimal LoanAmount { get; set; }
    }
}
