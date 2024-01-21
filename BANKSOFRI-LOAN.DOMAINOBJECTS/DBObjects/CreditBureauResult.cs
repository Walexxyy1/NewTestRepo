using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects
{
    public partial class CreditBureauResult
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string CustomerId { get; set; }
        public string CreditBureauDetails { get; set; }
        public int CreditScore { get; set; }
        public string CreditRemark { get; set; }
    }
}
