using BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces
{
    public interface IMono
    {
        Task SaveCustomerMonoAccountId(MonoAccount ma);
        Task<MonoAccount> GetCustomerMonoAccount(string customerId);
    }
}
