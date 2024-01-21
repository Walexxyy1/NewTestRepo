using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects
{
    public partial class CreditRiskAssessment
    {
        public int Id { get; set; }
        public string RiskItem { get; set; }
        public string Options { get; set; }
        public decimal Score { get; set; }
    }
}
