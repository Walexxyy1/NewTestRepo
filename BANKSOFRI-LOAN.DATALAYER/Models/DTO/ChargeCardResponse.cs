using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.DATALAYER.Models.DTO
{
    public class ChargeAuthorization
    {
        public string authorization_code { get; set; }
        public string bin { get; set; }
        public string last4 { get; set; }
        public string exp_month { get; set; }
        public string exp_year { get; set; }
        public string channel { get; set; }
        public string card_type { get; set; }
        public string bank { get; set; }
        public string country_code { get; set; }
        public string brand { get; set; }
        public bool reusable { get; set; }
        public string signature { get; set; }
        public string account_name { get; set; }
    }

    public class ChargeCustomer
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string customer_code { get; set; }
        public string phone { get; set; }
        public object metadata { get; set; }
        public string risk_action { get; set; }
        public string international_format_phone { get; set; }
    }

    public class ChargeCardData
    {
        public int amount { get; set; }
        public string currency { get; set; }
        public DateTime transaction_date { get; set; }
        public string status { get; set; }
        public string reference { get; set; }
        public string domain { get; set; }
        public string metadata { get; set; }
        public string gateway_response { get; set; }
        public string message { get; set; }
        public string channel { get; set; }
        public string ip_address { get; set; }
        public string log { get; set; }
        public string fees { get; set; }
        public ChargeAuthorization authorization { get; set; }
        public ChargeCustomer customer { get; set; }
        public string plan { get; set; }
        public int id { get; set; }
    }

    public class ChargeCardResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public ChargeCardData data { get; set; }
    }

}
