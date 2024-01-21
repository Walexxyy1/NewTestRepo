using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects
{
    public partial class AvgScoreSetup
    {
        public int Id { get; set; }
        public string RiskName { get; set; }
        public decimal WeightAvg { get; set; }
    }
}
