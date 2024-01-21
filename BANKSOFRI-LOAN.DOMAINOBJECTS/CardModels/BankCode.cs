using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.CardModels
{
    public partial class BankCode
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Gateway { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
}
