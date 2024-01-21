using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO.MetaObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces
{
   public interface IMeta
    {
        IdentityResponse IndentityVerification(MetaIdentityVerificationRequestObject miv);
    }
}
