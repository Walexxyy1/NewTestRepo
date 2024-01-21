using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces
{
    public interface ICRC
    {
        Task<CRCSCResponse> GetCRCCreditScoreAsync(string custBvn, string customerId);       
    }
}
