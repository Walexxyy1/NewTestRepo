using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.DTO
{
    public class NanoLoanRequestDTO
    {
		public  string CustomerId { get; set; }
		public  string FirstName { get; set; }
		public  string LastName { get; set; }
		public  string MaritalStatus { get; set; }
		public  string Gender { get; set; }
		public  string DateOfBirth { get; set; }
		public  string Street { get; set; }
		public  string City { get; set; }
		public  string State { get; set; }
		public  string PostalCode { get; set; }
		public  string Country { get; set; }
		public  string Email { get; set; }
		public  string Phone { get; set; }		
		public  string BVN { get; set; }
		public  string EmployerName { get; set; }
		public  string Industry { get; set; }
		public  int EmploymentDuration { get; set; }
		public  decimal Income { get; set; }
		public  string Type { get; set; }
		public  bool IncomeVerified { get; set; }
		public  string LevelOfEducation { get; set; }
		public  string ResidenceStatus { get; set; }
		public  string EmploymentType { get; set; }
		public  decimal LoanAmount { get; set; }
		public  string LoanPurpose { get; set; }
		public string startDate { get; set; }
	}
}
