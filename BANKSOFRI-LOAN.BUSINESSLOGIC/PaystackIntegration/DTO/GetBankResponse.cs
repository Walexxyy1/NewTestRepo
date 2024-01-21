using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.PaystackIntegration.DTO
{
    public class GetBankResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public List<data> data { get; set; }
    }
    public class data
    {
        public string name { get; set; }
        public string slug { get; set; }
        public string code { get; set; }
        public string longcode { get; set; }
        public string gateway { get; set; }
        public bool pay_with_bank { get; set; }
        public bool active { get; set; }
        public bool? is_deleted { get; set; }
        public string country { get; set; }
        public string currency { get; set; }
        public string type { get; set; }
        public int id { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
