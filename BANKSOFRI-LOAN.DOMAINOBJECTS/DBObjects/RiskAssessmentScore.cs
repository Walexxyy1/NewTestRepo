using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects
{
    public partial class RiskAssessmentScore
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string RiskItem { get; set; }
        public decimal AssessmentScore { get; set; }
        public decimal WeightedAvgScore { get; set; }
    }
}
