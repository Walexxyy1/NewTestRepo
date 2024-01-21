using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects
{
    public partial class Rate
    {
        public int Id { get; set; }
        public string EmploymentStatus { get; set; }
        public string RiskLevel { get; set; }
        public int? Tenor { get; set; }
        public decimal? BasisPoint { get; set; }
        public decimal? InterestRate { get; set; }
    }
}
