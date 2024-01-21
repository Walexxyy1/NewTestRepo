using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BANKSOFRI_LOAN.DATALAYER.Models.DTO
{
    public class Applicant
    {
        public string idType { get; set; }
        public string idNumber { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string phone { get; set; }
        public string dob { get; set; }
    }
    public class VerifyRequestObj
    {
        public string street { get; set; }
        public string lga { get; set; }
        public string state { get; set; }
        public string landmark { get; set; }
        public Applicant applicant { get; set; }
    }
}