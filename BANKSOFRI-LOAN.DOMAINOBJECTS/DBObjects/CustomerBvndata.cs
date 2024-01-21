using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects
{
    public partial class CustomerBvndata
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string CustomerId { get; set; }
        public string Bvn { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Middlename { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Birthdate { get; set; }
        public string Photo { get; set; }
        public string MaritalStatus { get; set; }
        public string LgaOfResidence { get; set; }
        public string LgaOfOrigin { get; set; }
        public string ResidentialAddress { get; set; }
        public string StateOfOrigin { get; set; }
        public string EnrollmentBank { get; set; }
        public string EnrollmentBranch { get; set; }
        public string NameOnCard { get; set; }
        public string Title { get; set; }
        public string LevelOfAccount { get; set; }
    }
}
