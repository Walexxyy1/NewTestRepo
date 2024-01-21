using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects
{
    public partial class ProvenirDecisionData
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string CustomerId { get; set; }
        public string ReferenceId { get; set; }
        public string LoanData { get; set; }
        public string Status { get; set; }
    }
}
