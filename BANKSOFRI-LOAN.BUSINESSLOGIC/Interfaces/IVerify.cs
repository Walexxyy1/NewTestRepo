using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces
{
   public interface IVerify
    {
        Task<VerifyResponseObj> SubmitAddressVerificationRequest(VerifyRequestObj req);
        Task<CancelVerifyResponse> CancelAddressVerification(string Id);
        Task<GetVerificationResponse> GetVerificationResponse(GetVerificationRequesObj g);
    }
}
