using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects
{
    public partial class UserAuthentication
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string UserId { get; set; }
        public string TokenCode { get; set; }
        public DateTime TimeIssued { get; set; }
        public string Status { get; set; }
        public DateTime? TimeUsed { get; set; }
        public DateTime? ExpirationTime { get; set; }
    }
}
