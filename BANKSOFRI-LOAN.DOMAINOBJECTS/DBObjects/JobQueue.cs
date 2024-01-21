using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects
{
    public partial class JobQueue
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public string Queue { get; set; }
        public DateTime? FetchedAt { get; set; }
    }
}
