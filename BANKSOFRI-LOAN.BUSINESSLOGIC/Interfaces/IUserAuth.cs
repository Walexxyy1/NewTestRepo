using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO;
using BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces
{
   public interface IUserAuth
    {
        Task<string> GetAuthToken(AuthDTO uat);
        Task<string> UserAuthTokenValidation(AuthValidationDTO avd);
        Task<long> UpdateUserTokenStatus(UserAuthentication au);
    }
}
