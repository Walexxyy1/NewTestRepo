using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.CardModels
{
    public partial class CustomerInvite
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string InvitedBy { get; set; }
        public string InviteStatus { get; set; }
    }
}
