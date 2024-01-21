using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO;
using BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces
{
    public interface ICustomer
    {
        Task<bool> HasActiveNanoLoan(string customerId);
        Task SaveProvenirRequest(NanoLoanRequest r);
        Task<long> SaveCustomerLoan(NanoLoan nl);
        Task<GetLoanOffersResponse> GetCustomerLatestLoanOffers(string custID);
        Task SaveCreditBureau(CreditScoreData cbr);
        Task<CreditScoreData> GetMostRecentCreditBureauScore(string customerID, string bureaType);
        Task<long> SaveOrUdateCusomerLoanProcessingDetails(NanoLoanRequestDTO cd);
        Task<CustomerDetail> GetCustomerLoanProcessingDetailsFromDB(string custID);
        Task<string> CreateCustomer(CustomerDTO cd);
        Task<string> GetCustomerDetails(string email);
        Task<CustomerAndLoanDTO> GetCustomerType(string customerID);
        Task<bool> IsSofriAccountBalanceSufficient(string customerId, string accountnum, decimal Amount);
        Task<double> GetCustomerORR(string customerID);
        Task<long> GetApplicationId();
        Task<bool> IsAccountValid(string customerId);
        Task<string> GetCustomerCardStatus(string customerEmail);
        Task<bool> HasPendingNanoLoan(string custId);
        Task<ValidateNameOnCardResponse> ValidateCardDetails(ValidateCardDetailsDTO vnc);

    }
}
