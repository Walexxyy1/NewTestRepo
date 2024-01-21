using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.DTO
{
    public class PromoDisbursedResponse
    {
        public bool IsSuccessful { get; set; }
        public string ResponseMessage { get; set; }
        public string ResponseCode { get; set; }
        public string Reference { get; set; }
        public string Status { get; set; }
    }
    public class PromoDisbursedRequest
    {
        public string RetrievalReference { get; set; }
        public string AccountNumber { get; set; }
        public string NibssCode { get; set; }
        public string Amount { get; set; }
        public int Fee { get; set; }
        public string Narration { get; set; }
        public string Token { get; set; }
        public string GLCode { get; set; }
    }
}
