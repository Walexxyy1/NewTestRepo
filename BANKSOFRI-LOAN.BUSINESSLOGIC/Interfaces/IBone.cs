using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO;
using BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces
{
   public interface IBone
    {
        Task<List<NanoLoan>> GetCutomerLoanAccounts(string customerID);
        LoanDisbursementResponse DisburseLoanViaBankOne(LoanDisbursementRequestDTO ld);       
        Task<CustomerDetailsDTO> GetByCustomerID(string customerID);
        bool AddCardValidation(string customerId);
        Task<LoanDisbursementResponse> LoanPrincipalCollectionFromSofriAccount(LoanCollectionRequestDTO ld);
        Task<LoanDisbursementResponse> LoanInterestCollectionFromSofriAccount(LoanCollectionRequestDTO ld);
        Task<LoanDisbursementResponse> CreditCustomerSofriAccountForRepayment(LoanCollectionRequestDTO ld);
        GetCustomerAccounts GetAccountBalance(string customerId);
        Task<LoanDisbursementResponse> DisburseCashBackViaBankOne(LoanCollectionRequestDTO ld);
        BankOneLoanCreationResponse CreateLoanOnBankOne(BankOneLoanCreationRequest req);
        AccountlienResponse PlaceLienOnAccount(AccountlienRequestObject alr);
        AccountlienResponse RemoveLienOnAccount(RemovelientOnAccountRequestObject alr);
        Task<long> CountActiveLoan(string customerID);
        string GetBankOneLoanAccountStatus(string customerID, string loanId);
        RepayLoanRespsonse RepaySofriNanoLoan(RepayLoanDTIO r);
    }
}
