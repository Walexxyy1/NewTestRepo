using BANKSOFRI_LOAN.BUSINESSLOGIC.DTO;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using BANKSOFRI_LOAN.BUSINESSLOGIC.Interfaces;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.Logic
{
    public class EmailSender:IMailer
    {
        string _Host;
        int _Port;
        string _UserName;
        string _Password;
        bool _EnableSSL;
        string _addcardurl;
        //private readonly ILogs _log;
        public EmailSender(string Host, int Port, string UserName, string Password, bool EnableSSL, string addcardurl)
        {
            _Host = Host;
            _Port = Port;
            _UserName = UserName;
            _Password = Password;
            _EnableSSL = EnableSSL;
            _addcardurl = addcardurl;
           
        }

        public long SendEmail(MailObject mo)
        {
           
            long response = 0;
           
           var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_UserName));
            email.To.Add(MailboxAddress.Parse("hello@sofrisofri.com"));
            email.Subject = "DISBURSEMENT ERROR";
            email.Body = new TextPart(TextFormat.Html) { Text = $"<h1>Disbursement Error has occured</h1><br> <p> Dear Team, Customer application for Nanoloan was successful, but disbursement from bankOne failed!<br/> Customer ID: {mo.CustomerId}, Customer Name:{mo.FirstName}, Loan Amount: {mo.LoanAmount}  </p>" };
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect(_Host, _Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_UserName, _Password);
            smtp.Send(email);
            smtp.Disconnect(true);
            response = 1;
            return response;
        }
    }
}
