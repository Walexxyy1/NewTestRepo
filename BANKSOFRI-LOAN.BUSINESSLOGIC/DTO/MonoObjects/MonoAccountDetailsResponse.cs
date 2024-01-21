using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.DTO.MonoObjects
{
    public class MonoAccountDetailsRequest
    {
        public string AccountId { get; set; }
        public string CustomerId { get; set; }
    }
    public class MonoAccountDetailsResponse
    {
        public int average_income { get; set; }
        public int monthly_income { get; set; }
        public int yearly_income { get; set; }
        public int income_sources { get; set; }
    }
    public class MonoAccount
    {
        public string _id { get; set; }
        public Institution institution { get; set; }
        public string name { get; set; }
        public string accountNumber { get; set; }
        public string type { get; set; }
        public int balance { get; set; }
        public string currency { get; set; }
        public string bvn { get; set; }
    }

    public class Institution
    {
        public string name { get; set; }
        public string bankCode { get; set; }
        public string type { get; set; }
    }

    public class Meta
    {
        public string data_status { get; set; }
        public string auth_method { get; set; }
    }
}
