using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.CardModels
{
    public partial class IncomeDatum
    {
        public string IppisNum { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Mda { get; set; }
        public decimal MonthlyIncome { get; set; }
    }
}
