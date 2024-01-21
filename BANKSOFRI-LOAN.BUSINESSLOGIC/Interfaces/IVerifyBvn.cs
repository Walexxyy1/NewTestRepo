using BANKSOFRI_LOAN.DATALAYER.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces
{
   public interface IVerifyBvn
    {
        VerifymeBVNResponse ValidateBVNOnVerifyMe(VerifyMeBVNValidationRequestObject vb);
    }
}
