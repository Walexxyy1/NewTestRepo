using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.DATALAYER.Models.DTO
{
    public class Authorization
    {
        public string authorization_code { get; set; }
    }

    public class ResponseData
    {
        public Authorization authorization { get; set; }
    }
    public class VerifytransactionResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public ResponseData data { get; set; }
    }
}
