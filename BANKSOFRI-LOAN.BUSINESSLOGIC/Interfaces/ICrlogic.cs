using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO;
using BANKSOFRI_LOAN.DATALAYER.Models;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO.CRegistryObjects;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO.ProvenirObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces
{
   public interface ICrlogic
    {
        Task<LoanProccessingResponseObject> ProccessCustomerLoanRequest(NanoLoanRequestDTO cd);
        Task<double> TestORRCalculation(NanoLoanRequestDTO cd);
    }
}
