using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects
{
    public partial class TransactionDetail
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string TransactionType { get; set; }
        public string CustNo { get; set; }
        public string SourceAccount { get; set; }
        public string DestinationAccountNo { get; set; }
        public decimal Amount { get; set; }
        public string Narration { get; set; }
        public string ReferenceNo { get; set; }
        public string DestinationBankCode { get; set; }
        public string SourceAccountName { get; set; }
        public string DestinationAccountName { get; set; }
        public string LookupReference { get; set; }
        public string CurrencyCode { get; set; }
    }
}
