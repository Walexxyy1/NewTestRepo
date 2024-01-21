using BANKSOFRI_LOAN.DATALAYER.Models.DTO.ProvenirObjects;

namespace BANKSOFRI_LOAN.DATALAYER.Models.DTO
{
    public class LoanProccessingResponseObject
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public string LoanRequestDecision { get; set; }
        public ProvenirCreditDecisionResponseObject LoanOffers { get; set; }
        public string RerenceId { get; set; }
        public string CardStatus { get; set; }
    }
}
