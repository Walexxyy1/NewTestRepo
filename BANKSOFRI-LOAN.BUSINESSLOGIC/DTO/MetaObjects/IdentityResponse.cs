using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.DTO.MetaObjects
{
   public class IdentityResponse
    {
        public string ResponseCode { get; set; }
        public string Responsemessage { get; set; }
        
    }
    public class Response
    {
        public bool result { get; set; }
    }
}
