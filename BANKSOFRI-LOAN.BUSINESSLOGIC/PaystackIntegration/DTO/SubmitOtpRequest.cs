﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.PaystackIntegration.DTO
{
    public class SubmitOtpRequest
    {
        public string otp { get; set; }
        public string reference { get; set; }
    }

    public class SubmitOtpResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public ResponseData data { get; set; }
    }
    public class SubmitOtpCustomField
    {
        public string display_name { get; set; }
        public string variable_name { get; set; }
        public string value { get; set; }
    }

    public class SubmitOtpMetadata
    {
        public List<SubmitOtpCustomField> custom_fields { get; set; }
    }

    public class Authorization
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

    public class Customer
    {
        public int id { get; set; }
        public object first_name { get; set; }
        public object last_name { get; set; }
        public string email { get; set; }
        public string customer_code { get; set; }
        public object phone { get; set; }
        public object metadata { get; set; }
        public string risk_action { get; set; }
    }

    public class ResponseData
    {
        public int amount { get; set; }
        public string currency { get; set; }
        public DateTime transaction_date { get; set; }
        public string status { get; set; }
        public string reference { get; set; }
        public string domain { get; set; }
        public SubmitOtpMetadata metadata { get; set; }
        public string gateway_response { get; set; }
        public object message { get; set; }
        public string channel { get; set; }
        public string ip_address { get; set; }
        public object log { get; set; }
        public int fees { get; set; }
        public Authorization authorization { get; set; }
        public Customer customer { get; set; }
        public object plan { get; set; }
    }
}
