using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.DATALAYER.Models.DTO.ProvenirObjects
{
   public class ProvenirCreditDecisionResponseObject
    {
        public List<Offer> offers { get; set; }
        public string riskLevel { get; set; }
        public string clientId { get; set; }
        public string applicationId { get; set; }
        public string guid { get; set; }
        public string overallDecision { get; set; }
        public List<DeclineReason> declineReasons { get; set; }
        public List<Error> errors { get; set; }
        public string applicationXML { get; set; }
        public string ReferenceId { get; set; }
        public string OfferStatus { get; set; }
        public string EmploymentType { get; set; }
    }
    public class Offer
    {
        public string tenor { get; set; }
        public string rate { get; set; }
        public string maxAmount { get; set; }
    }

    public class DeclineReason
    {
        public string declineReason { get; set; }
    }

    public class Error
    {
        public string objectName { get; set; }
        public string comment { get; set; }
        public string message { get; set; }
        public DateTime timestamp { get; set; }
    }
}
