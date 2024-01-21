using BANKSOFRI_LOAN.DATALAYER.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.DTO
{
    public class GetUserTokenResponse : CreateCustomerResponse
    {
        public string Token { get; set; }
    }

    public class CreateCustomerResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public string UserAccessDetails { get; set; }
    }
    public class VerifyAddressResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public VerifyResponseObj details { get; set; }
    }
    public class CancelAddressResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public CancelVerifyResponse details { get; set; }
    }
}
