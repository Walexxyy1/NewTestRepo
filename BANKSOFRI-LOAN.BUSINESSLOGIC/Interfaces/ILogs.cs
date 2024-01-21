using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces
{
    public interface ILogs
    {
        Task LogMessage(string logmessage, string logtype);
        void LogMessages(string logmessage, string logtype);
        void Logger(string logmessage, string logtype);
        void ServiceMsg(string logmessage, string logtype);
    }
}
