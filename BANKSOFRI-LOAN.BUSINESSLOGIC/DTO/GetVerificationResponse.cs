using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.DTO
{
    public class GetVerificationResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public string VerificationStatus { get; set; }
    }
    public class GetVerificationRequesObj
    {
        public string firstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
