using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO;
using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO.MonoObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces
{
   public interface IMoint
    {
        ResponseObject GetAccountIdFromMono(AddAccountIdRequestDTO req);
        MonoAccountDetailsResponse GetCustomerMonoAccountDetails(MonoAccountDetailsRequest req);
    }
}
