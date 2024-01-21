using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.PaystackIntegration.DTO
{
    public class CustomField
    {
        public string value { get; set; }
        public string display_name { get; set; }
        public string variable_name { get; set; }
    }
    public class Metadata
    {
        public List<CustomField> custom_fields { get; set; }
    }
    public class Bank
    {
        public string code { get; set; }
        public string account_number { get; set; }
    }
    public class AddCardRequest
    {
        public string email { get; set; }
        public string amount { get; set; }
        public Metadata metadata { get; set; }
        public Bank bank { get; set; }
        public string birthday { get; set; }
    }
}
