using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using BANKSOFRI_LOAN.DATALAYER.Repositories;
using BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Logger
{
    
    public class AppLogger : ILogs
    {
        ApiauthorisationContext _ctx;
        public AppLogger(ApiauthorisationContext ctx)
        {
            _ctx = ctx;
        }
        public async Task LogMessage(string logmessage, string logtype)
        {
            ApiauthorisationContext context = new ApiauthorisationContext();
            context.Logs.Add(new Log() { LogTime = DateTime.Now, Type = logtype, Details = logmessage });
            await context.SaveChangesAsync();
        }
        public void LogMessages(string logmessage, string logtype)
        {
            ApiauthorisationContext context = new ApiauthorisationContext();
            context.Logs.Add(new Log() { LogTime = DateTime.Now, Type = logtype, Details = logmessage });
            context.SaveChanges();           
        }
        public void Logger(string logmessage, string logtype)
        {
            _ctx.Logs.Add(new Log() { LogTime = DateTime.Now, Details = logmessage, Type = logtype });
            _ctx.SaveChanges();           
        }
        public void ServiceMsg(string logmessage, string logtype)
        {
            _ctx.ServiceLogs.Add(new ServiceLog() { LogTime = DateTime.Now, Details = logmessage, Type = logtype });
            _ctx.SaveChanges();           
        }
    }
}
