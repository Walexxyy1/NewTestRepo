using BANKSOFRI_LOAN.DATALAYER.DTO;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO.AllaweeObjects;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces
{
    public  interface IAllInt
    {
        GetIdentityResponseObject GetCustomerIdentity(string bvn);
        GetScoreResponseObject GetCustomerCreditScore(GetScoreRequestObject ccs);
    }
}
