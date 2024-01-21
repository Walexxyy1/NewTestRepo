using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects
{
    public partial class OnboardingIssue
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string CustomerId { get; set; }
        public string PhoneNumber { get; set; }
        public string AccountNumber { get; set; }
        public string IssueDetails { get; set; }
    }
}
