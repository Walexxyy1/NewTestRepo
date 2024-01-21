using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.PaystackIntegration.DTO
{
    public class AddCardResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }
    public class Data
    {
        public string reference { get; set; }
        public string status { get; set; }
        public string display_text { get; set; }
    }
}
