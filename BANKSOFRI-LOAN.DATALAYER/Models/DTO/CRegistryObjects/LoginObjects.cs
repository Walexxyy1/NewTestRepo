using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.DATALAYER.Models.DTO.CRegistryObjects
{
    public class UpdatePasswordRequestObject
    {
        public string ResetPassword { get; set; }
        public string NewPassword { get; set; }       
    }
    public class LoginRequestObject
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string SubscriberID { get; set; }
    }
    public class LoginResponseObject
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public string EmailAddress { get; set; }
        public bool Authenticated { get; set; }
        public string SessionCode { get; set; }
        public string AgentID { get; set; }
        public string AgentName { get; set; }
        public string SubscriberID { get; set; }
        public string SubscriberName { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
    
}
