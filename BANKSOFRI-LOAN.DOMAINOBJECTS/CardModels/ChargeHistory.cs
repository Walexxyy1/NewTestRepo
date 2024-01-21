using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.CardModels
{
    public partial class ChargeHistory
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Email { get; set; }
        public string ReferenceId { get; set; }
        public decimal AmountCharge { get; set; }
        public string ChargedBy { get; set; }
        public string Status { get; set; }
    }
}
