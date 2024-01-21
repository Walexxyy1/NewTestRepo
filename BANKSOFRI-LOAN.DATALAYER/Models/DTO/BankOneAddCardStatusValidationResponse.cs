using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.DATALAYER.Models.DTO
{
    public class BankOneAddCardStatusValidationRequest
    {
        public string CustomerId { get; set; }
        public string Token { get; set; }
    }
    public class BankOneAddCardStatusValidationResponse
    {
        public bool HasCard { get; set; }
        public string Description { get; set; }
        public object ResponseCode { get; set; }
    }
}
