using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects
{
    public partial class ServiceLog
    {
        public int Id { get; set; }
        public DateTime LogTime { get; set; }
        public string Type { get; set; }
        public string Details { get; set; }
    }
}
