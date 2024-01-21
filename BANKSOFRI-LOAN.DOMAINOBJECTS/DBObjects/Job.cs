using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects
{
    public partial class Job
    {
        public Job()
        {
            JobParameters = new HashSet<JobParameter>();
            States = new HashSet<State>();
        }

        public int Id { get; set; }
        public int? StateId { get; set; }
        public string StateName { get; set; }
        public string InvocationData { get; set; }
        public string Arguments { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpireAt { get; set; }

        public virtual ICollection<JobParameter> JobParameters { get; set; }
        public virtual ICollection<State> States { get; set; }
    }
}
