using BANKSOFRI_LOAN.DATALAYER.Models;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces
{
   public interface ICollect
    {
        Task<string> StartRepaymentCollection();
        Task<string> StartFailedRepaymentCollection();
        Task<List<CollectionDTO>> GetRecords();
        void SyncLoanStatusWithBankOne();
    }
}
