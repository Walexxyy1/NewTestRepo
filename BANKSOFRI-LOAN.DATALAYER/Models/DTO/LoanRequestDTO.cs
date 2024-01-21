using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.DATALAYER.Models.DTO
{
   public class LoanRequestDTO
    {
        public string ReferenceID { get; set; }
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public virtual string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string LinkedAccountNumber { get; set; }   
        public int Tenure { get; set; }
        public decimal Amount { get; set; }
        public decimal Rate { get; set; }
    }
}
