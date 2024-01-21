using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects
{
    public partial class LoanProcessingData
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string CustomerId { get; set; }
        public string Offers { get; set; }
        public string ApplicationId { get; set; }
        public string Guid { get; set; }
        public string OverallDecision { get; set; }
        public string DeclineReasons { get; set; }
        public string Errors { get; set; }
        public string ApplicationXml { get; set; }
        public string RequestId { get; set; }
        public string OfferStatus { get; set; }
        public string EmploymentType { get; set; }
        public string RiskLevel { get; set; }
    }
}
