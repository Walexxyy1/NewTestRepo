using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.CardModels
{
    public partial class Log
    {
        public int Id { get; set; }
        public DateTime LogTime { get; set; }
        public string Type { get; set; }
        public string Details { get; set; }
    }
}
