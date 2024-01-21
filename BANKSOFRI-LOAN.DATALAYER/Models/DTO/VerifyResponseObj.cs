using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BANKSOFRI_LOAN.DATALAYER.Models.DTO
{
    public class ApplicantDetails
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string phone { get; set; }
        public string idType { get; set; }
        public string idNumber { get; set; }
        public string middlename { get; set; }
        public string photo { get; set; }
        public string gender { get; set; }
        public string birthdate { get; set; }
    }

    public class Neighbor
    {
        public string name { get; set; }
        public string comment { get; set; }
        public string phone { get; set; }
    }

    public class Status
    {
        public string status { get; set; }
        public string subStatus { get; set; }
        public string state { get; set; }
    }

    public class Data
    {
        public int id { get; set; }
        public ApplicantDetails applicant { get; set; }
        public string createdAt { get; set; }
        public string completedAt { get; set; }
        public string lattitude { get; set; }
        public string longitude { get; set; }
        public List<string> photos { get; set; }
        public Neighbor neighbor { get; set; }
        public Status status { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public string lga { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string reference { get; set; }
    }
    public class VerifyResponseObj
    {
        public string status { get; set; }
        public Data data { get; set; }
    }
    
    public class CancelVerifyResponse
    {
        public string status { get; set; }
        public string message { get; set; }
        public string code { get; set; }
        public string data { get; set; }
    }
}