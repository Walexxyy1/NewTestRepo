using BANKSOFRI_LOAN.BUSINESSLOGIC.PaystackIntegration.DTO;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces
{
    public interface IPaystack
    {
        Task<GetBankResponse> LoadBanksFromPayStack();
        Task<ChargeResponse> VerifyTransaction(string custId, string loanId, string ReferenceId);
        Task<string> Charge(ChargeCardObject cco, string user);
        Task<DOMAINOBJECTS.CardModels.CardTransaction> GetAuthorization(string email);
        Task<InterestResponse> CalculateRepaymentInterest(string email, decimal interestdue);
        Task<string> PartialDebit(ChargeCardObject cco, string user);
    }
}
