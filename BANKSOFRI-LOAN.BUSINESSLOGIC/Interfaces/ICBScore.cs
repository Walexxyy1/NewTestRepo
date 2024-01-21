using BANKSOFRI_LOAN.DATALAYER.Models.DTO.CRegistryObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces
{
   public interface ICBScore
    {
        Task<decimal> GetCreditScore(CRegisteryCreditScoreRequestDTO cs);
    }
}
