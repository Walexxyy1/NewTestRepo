using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects
{
    public partial class VerificationRequest
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string Street { get; set; }
        public string Lga { get; set; }
        public string State { get; set; }
        public string Landmark { get; set; }
        public string IdType { get; set; }
        public string IdNumber { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string Dob { get; set; }
        public string VerificationStatus { get; set; }
    }
}
