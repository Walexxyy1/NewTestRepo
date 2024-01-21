using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Logger;
using BANKSOFRI_LOAN.DATALAYER.Models.DTO;
using BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Logic
{
    public class MessagingService : INotice
    {
        private readonly IConfiguration _config;
        private readonly IMessage mi;
        private readonly ILoanService ls;
        ILogs _log;
        public MessagingService(IConfiguration config, IMessage msg, ILoanService _ls, ILogs log)
        {
            _config = config;
            mi = msg;
            ls = _ls;
            _log = log;
        }

        public void StartNotification()
        {
            _log.ServiceMsg("Starting Notification service", "INFO");
            Send48HoursNotification();
            Send24HoursNotification();
        }
        public void Send48HoursNotification()
        {
            _log.ServiceMsg("starting 48-hour notice service", "INFO");
            DateTime repaydate = DateTime.Today.AddDays(2);
            IEnumerable<NanoLoan> nl = ls.GetAllRepaymentDatesGettingDue(repaydate);
            _log.ServiceMsg("Total Customer details found: "+ nl.Count(), "INFO");
            if (nl != null)
            {
                foreach (NanoLoan n in nl)
                {
                    var d = ls.GetRepaymentScheduleDetails(n.LoanReferenceId, repaydate);
                    decimal repaydue = d.PrincipalDue + d.InterestDue;
                    string message = "Dear " + n.CustomerName + ", Your Loan Repayment of N" + Convert.ToDecimal(repaydue).ToString("#,##0.00") + " will be in 2 days. Please fund you Sofri Account to enjoy "
                        + _config.GetSection("SofriDiscount").Value + "% discount on your repayment";
                    SendSMSResponse ssr = mi.SendMessageToCustomer(new SendSMSRequestObject() { PhoneNumber = n.PhoneNumber, Message = message });
                    if (ssr.ResponseCode == "00")
                    {
                        _log.ServiceMsg("48-HOUR Notice successfully send to " + n.CustomerName, "INFO");
                    }
                    else
                    {
                        _log.ServiceMsg("48-HOUR Notice to " + n.CustomerName + " Failed! Details: " + ssr.ResponseMessage, "INFO");
                    }
                }
            }
            else { _log.ServiceMsg("No 48-Hour due Record found!", "INFO"); }
            _log.ServiceMsg("Stopping 48-hour Notice Service", "INFO");
        }
        public void Send24HoursNotification()
        {
            _log.ServiceMsg("starting 24-hour notice service", "INFO");
            DateTime repaydate = DateTime.Today.AddDays(1);
            IEnumerable<NanoLoan> nl = ls.GetAllRepaymentDatesGettingDue(repaydate);
            _log.ServiceMsg("Total Customer details found: " + nl.Count(), "INFO");
            if (nl != null)
            {
                foreach (NanoLoan n in nl)
                {
                    var d = ls.GetRepaymentScheduleDetails(n.LoanReferenceId, repaydate);
                    decimal repaydue = d.PrincipalDue + d.InterestDue;
                    string message = "Dear " + n.CustomerName + ", Your Loan Repayment of N" + Convert.ToDecimal(repaydue).ToString("#,##0.00") + " will be due tommorrow. Please fund you Sofri Account to enjoy "
                        + _config.GetSection("SofriDiscount").Value + "% discount on your repayment";
                    SendSMSResponse ssr = mi.SendMessageToCustomer(new SendSMSRequestObject() { PhoneNumber = n.PhoneNumber, Message = message });
                    if (ssr.ResponseCode == "00")
                    {
                        _log.ServiceMsg("24-HOUR Notice successfully send to " + n.CustomerName, "INFO");
                    }
                    else
                    {
                        _log.ServiceMsg("24-HOUR Notice to " + n.CustomerName + " Failed! Details: " + ssr.ResponseMessage, "INFO");
                    }
                }
            }
            else { _log.ServiceMsg("No 24-Hour due Record found!", "INFO"); }
            _log.ServiceMsg("Stopping 24-hour Notice Service", "INFO");
        }
    }
}
