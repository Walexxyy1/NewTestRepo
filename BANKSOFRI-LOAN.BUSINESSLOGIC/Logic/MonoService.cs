using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Logger;
using BANKSOFRI_LOAN.DATALAYER.Repositories;
using BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Logic
{
   public class MonoService : IMono
    {
        
       ILogs _log;
        ApiauthorisationContext _ctx;
        public MonoService( ILogs log, ApiauthorisationContext ctx)
        {
            _log = log;
           _ctx = ctx;
        }
        public async Task SaveCustomerMonoAccountId(MonoAccount ma)
        {           
            try
            {
                MonoAccount m = await GetCustomerMonoAccount(ma.CustomerId);
                if (m == null)
                {
                     await _ctx.AddAsync(ma);
                    await _ctx.SaveChangesAsync();  
                }
                else
                {
                    m.MonoAccountId = ma.MonoAccountId;                  
                    _ctx.Update(m);
                    await _ctx.SaveChangesAsync();  
                }
            }
            catch (Exception ex)
            {
              _log.Logger("An error occured on SaveCustomerMonoAccount method in MonoService! Details: " + ex.Message, "Error");
            }           
        }

        public async Task<MonoAccount> GetCustomerMonoAccount(string customerId)
        {
            MonoAccount response = new MonoAccount();
            try
            {
               
                response = await _ctx.MonoAccounts.Where(x => x.CustomerId == customerId).FirstOrDefaultAsync();   
            }
            catch (Exception ex)
            {
               _log.Logger("An error occured on GetCustomerMonoAccount method in MonoService! Details: " + ex.Message, "Error");
            }
            return response;
        }
    }
}
