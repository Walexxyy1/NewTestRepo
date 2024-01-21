using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects
{
    public partial class AddressVerificationDetail
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string Middlename { get; set; }
        public string Gender { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Lattitude { get; set; }
        public string Longitude { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Lga { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Reference { get; set; }
        public string Status { get; set; }
    }
}
