using BANKSOFRI_LOAN.DATALAYER.Models.DTO;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO.ProvenirObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces
{
   public interface ILoanApp
    {
        //Task<LoanProccessingResponseObject> ProccessLoanApplication(ProvenirCreditDecisionRequestObject pcd);
        Task<LoanRequestResponse> BookCustomerLoan(LoanRequestDTO lro);
        Task<LoanRequestResponse> ProcessNanoLoan(LoanRequestDTO lro);
        Task<LoanRequestResponse> RetryNanoLoanDisbursement();
        string LoanActivation(LoanRequestDTO lro, decimal loanAmount);
    }
}
