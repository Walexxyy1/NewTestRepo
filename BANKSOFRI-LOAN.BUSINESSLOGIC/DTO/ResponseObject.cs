using BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.DTO
{
    public class ResponseObject
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
    }
    public class BonusHistoryResponse:ResponseObject
    {
       public List<PromoCodeUser> History { get; set; }
    }
}
