using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.DATALAYER.Models.DTO
{
    public class LoanRequestResponse
    {      
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }        
              
    }
    public class BankOneLoanRequestResponse
    {
        public bool IsSuccessful { get; set; }
        public string CustomerIDInString { get; set; }
        public string Message { get; set; }
        public string TransactionTrackingRef { get; set; }
        public object Page { get; set; }
    }
}
