using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects
{
    public partial class DailyBillsPayment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string AccountNumber { get; set; }
        public string Narration { get; set; }
        public decimal Amount { get; set; }
    }
}
