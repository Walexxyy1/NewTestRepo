using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.CardModels
{
    public partial class CardTransaction
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Reference { get; set; }
        public string Status { get; set; }
        public string Trans { get; set; }
        public string Transactions { get; set; }
        public string Email { get; set; }
        public string AuthorisationCode { get; set; }
        public string CardBin { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
