using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.DATALAYER.Models.DTO
{
   public class AccountTierUpdateRequestObject
    {
        public string AccountNumber { get; set; }
        public int AccountTier { get; set; }
        public bool SkipAddressVerification { get; set; }
    }
    public class AccountTierUpdateResponseObject
    {
        public bool IsSuccessful { get; set; }
        public object CustomerIDInString { get; set; }
        public string Message { get; set; }
        public object TransactionTrackingRef { get; set; }
        public object Page { get; set; }
    }
}
