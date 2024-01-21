using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.DATALAYER.Models.DTO.Collection
{
   public class CollectionDTO
    {
        public DateTime Date { get; set; }
        public  string LoanReferenceId { get; set; }
		public  string CustomerID { get; set; }
		public  string CustomerName { get; set; }
		public  string Email { get; set; }
		public  string SofriAccountNumber { get; set; }		
		public decimal AccountBalance { get; set; }
	}
}
