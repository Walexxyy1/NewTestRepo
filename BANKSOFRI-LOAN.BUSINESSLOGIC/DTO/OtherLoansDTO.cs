using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.DTO
{
   public class OtherLoansDTO
    {
		public string CustomerId { get; set; }
		public string AccountNumber { get; set; }		
		public decimal LoanAmount { get; set; }
		public string LoanPurpose { get; set; }
		public string LoanType { get; set; }
	}
}
