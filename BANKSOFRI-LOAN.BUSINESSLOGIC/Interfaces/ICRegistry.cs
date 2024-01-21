using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO.CRegistryObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces
{
   public interface ICRegistry
    {
        Task<Credit202Response> GetCustomerCreditScoreAsync(CRegisteryCreditScoreRequestDTO crsc);
    }
}
