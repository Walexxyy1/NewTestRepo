using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects
{
    public partial class CustomerDetail
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MaritalStatus { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string KycScore { get; set; }
        public string Bvn { get; set; }
        public string EmployerName { get; set; }
        public string Industry { get; set; }
        public int EmploymentDuration { get; set; }
        public decimal Income { get; set; }
        public string Type { get; set; }
        public bool IncomeVerified { get; set; }
        public string LevelOfEducation { get; set; }
        public string ResidenceStatus { get; set; }
        public string EmploymentType { get; set; }
        public decimal LoanAmount { get; set; }
        public string LoanPurpose { get; set; }
    }
}
