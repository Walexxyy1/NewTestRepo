using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.DATALAYER.Models.DTO
{ 
    public class ChargeRequest
    {
        public string authorization_code { get; set; }
        public string email { get; set; }
        public string amount { get; set; }
    }

    public class PartialChargeRequest
    {
        public string authorization_code { get; set; }
        public string currency { get; set; }
        public string amount { get; set; }
        public string email { get; set; }
    }
}
