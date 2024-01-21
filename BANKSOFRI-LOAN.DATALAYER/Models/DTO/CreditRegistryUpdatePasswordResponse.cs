using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.DATALAYER.Models.DTO
{
   public class CreditRegistryUpdatePasswordResponse
    {
        public string SessionCode { get; set; }
        public List<object> Errors { get; set; }
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Agent_ID { get; set; }
        public string Agent_Name { get; set; }
        public string Subscriber_ID { get; set; }
        public string Subscriber_Name { get; set; }
        public string Permission { get; set; }
        public bool Authorized { get; set; }
        public bool Subscriber_Status { get; set; }
        public string Email { get; set; }
        public bool PasswordReset { get; set; }
        public bool UseSessionCode { get; set; }
        public string SessionCodeHash { get; set; }
        public string PasswordHash { get; set; }
    }
    public class Login
    {
        public string Agent_ID { get; set; }
        public string Agent_Name { get; set; }
        public string Subscriber_ID { get; set; }
        public string Subscriber_Name { get; set; }
        public string Permission { get; set; }
        public bool Authorized { get; set; }
        public bool Subscriber_Status { get; set; }
        public string Email { get; set; }
        public bool PasswordReset { get; set; }
        public bool UseSessionCode { get; set; }
        public string SessionCodeHash { get; set; }
        public string PasswordHash { get; set; }
        public string Message { get; set; }
    }

    public class Subscription
    {
        public string Subscriber_Name { get; set; }
        public string Agent_Name { get; set; }
        public bool Authorized { get; set; }
        public bool Admin { get; set; }
        public bool Dispute_Agent { get; set; }
        public bool DisableNoMatchReport { get; set; }
        public string Agent_ID { get; set; }
        public string Subscriber_ID { get; set; }
        public string Permission { get; set; }
        public string Email { get; set; }
        public DateTime Last_Updated { get; set; }
        public string Last_Updated_By { get; set; }
    }

    public class UpdatePasswordRequestObject
    {
        public string EmailAddress { get; set; }
        public string SubscriberID { get; set; }
        public string ResetPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
