using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects
{
    public partial class LienHistory
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string CustomerId { get; set; }
        public string AccountNumber { get; set; }
        public decimal LienAmount { get; set; }
        public string LoanReferenceId { get; set; }
        public string LienReferenceId { get; set; }
        public DateTime RemoveDate { get; set; }
        public string LienStatus { get; set; }
    }
}
