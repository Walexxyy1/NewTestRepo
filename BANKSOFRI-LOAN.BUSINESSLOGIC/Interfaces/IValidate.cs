using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces
{
   public interface IValidate
    {
        bool getUserAuth(string apiKey);
        bool getAuthorisedUser(string apikey, string appid);
    }
}
