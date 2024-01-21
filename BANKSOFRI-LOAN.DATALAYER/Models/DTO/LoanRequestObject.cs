using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.DATALAYER.Models.DTO
{
   public class LoanRequestObject
    {
        public string TransactionTrackingRef { get; set; }
        public string LoanProductCode { get; set; }
        public string CustomerID { get; set; }
        public string LinkedAccountNumber { get; set; }
        public string CollateralDetails { get; set; }
        public string CollateralType { get; set; }
        public int ComputationMode { get; set; }
        public int Tenure { get; set; }
        public int Moratorium { get; set; }
        public DateTime InterestAccrualCommencementDate { get; set; }
        public int Amount { get; set; }
        public int InterestRate { get; set; }
        public int PrincipalPaymentFrequency { get; set; }
        public int InterestPaymentFrequency { get; set; }
    }
}
