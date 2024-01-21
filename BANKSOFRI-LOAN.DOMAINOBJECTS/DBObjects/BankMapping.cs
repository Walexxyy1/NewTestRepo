using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects
{
    public partial class BankMapping
    {
        public int Id { get; set; }
        public string InstitutionCode { get; set; }
        public string InstitutionName { get; set; }
        public int Category { get; set; }
        public string BOneCode { get; set; }
        public string BOneName { get; set; }
    }
}
