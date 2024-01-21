using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects
{
    public partial class Reversal
    {
        public int Id { get; set; }
        public string RetrievalReference { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime RetrialTime { get; set; }
    }
}
