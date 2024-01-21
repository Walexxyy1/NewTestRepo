using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BANKSOFRI_LOAN.DATALAYER.Models.DTO.ProvenirObjects.ProvenirDecisionResponse;

namespace BANKSOFRI_LOAN.DATALAYER.Models.DTO
{
   public class GetLoanOffersResponse
    {
        public string CustomerID { get; set; }
        public List<DATALAYER.Models.DTO.ProvenirObjects.Offer> LoanOffers { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public string CardStatus { get; set; }
    }   
}
