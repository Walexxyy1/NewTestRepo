using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.DTO
{
   public class FullLoanLiquidationDTO
    {
        public string CustomerId { get; set; }
        public string LoanId { get; set; }
    }
    public class PartialLoanLiquidationDTO : FullLoanLiquidationDTO
    {        
        public decimal LiquidationAmount { get; set; }
    }

    public class RepayLoanDTIO
    {
        public string CustomerId { get; set; }
        public string accountNumber { get; set; }       
        public double repaymentAmount { get; set; }
    }
    public class RepayLoanRequestObject : RepayLoanDTIO
    {
        public string principalNarration { get; set; }
        public string interestNarration { get; set; }
        public string feeNarration { get; set; }
        public string authtoken { get; set; }
        public string version { get; set; }
    }
    public class RepayLoanRespsonse
    {
        public bool IsSuccessful { get; set; }
        public object CustomerIDInString { get; set; }
        public string Message { get; set; }
        public object TransactionTrackingRef { get; set; }
        public object Page { get; set; }
    }
}
