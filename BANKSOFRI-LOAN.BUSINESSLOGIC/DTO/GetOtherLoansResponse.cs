using BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.DTO
{
   public class GetOtherLoansResponse : ResponseObject
    {
      public List<OtherLoan> loans { get; set; }
    }
}
