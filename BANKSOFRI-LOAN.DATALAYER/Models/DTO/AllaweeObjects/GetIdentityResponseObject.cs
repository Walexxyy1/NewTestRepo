using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.DATALAYER.Models.DTO.AllaweeObjects
{
   public class GetIdentityResponseObject
    {
        public string code { get; set; }
        public string message { get; set; }
        public IdentityData data { get; set; }
    }
    public class IdentityData
    {
        public string name { get; set; }
        public string phone { get; set; }
        public string gender { get; set; }
        public string dateOfBirth { get; set; }
        public object email { get; set; }
        public string address { get; set; }
        public object emails { get; set; }
        public List<string> addresses { get; set; }
        public string customerId { get; set; }
    }
}
