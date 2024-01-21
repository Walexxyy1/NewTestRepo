using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.DATALAYER.Models.DTO
{
   public class EligibilityResponseObject
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public bool IsValid { get; set; }
    }
}
