using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.DATALAYER.Models.DTO
{
   public class SendSMSResponse
    {
        public string TicketId { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
    }
    public class SendSMSRequestObject
    {
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public string Reference { get; set; }        
    }
}
