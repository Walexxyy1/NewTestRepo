using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects
{
    public partial class SofriPromo
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string PromoCode { get; set; }
        public string CodeType { get; set; }
        public DateTime StartDate { get; set; }
        public string Description { get; set; }
        public decimal AmountValue { get; set; }
        public DateTime Expiration { get; set; }
        public string UsedBy { get; set; }
        public string DateUsed { get; set; }
        public int? TotalUsers { get; set; }
        public int? TotalUsed { get; set; }
        public int? Balance { get; set; }
        public string Indentifier { get; set; }
        public string Status { get; set; }
        public string PromoOwnerCustomerId { get; set; }
        public string PromoOwnerAccountNumber { get; set; }
    }
}
