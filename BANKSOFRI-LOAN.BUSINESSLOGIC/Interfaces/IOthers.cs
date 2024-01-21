using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO;
using BANKSOFRI_LOAN.DATALAYER.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces
{
   public interface IOthers
    {
        Task<ResponseObject> SaveOtherLoanApplication(OtherLoansDTO ol);
        Task<GetOtherLoansResponse> FetchOtherLoans();
        Task<GetOtherLoansResponse> FetchCustomerOtherLoans(string customerId);
    }
}
