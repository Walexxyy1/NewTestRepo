using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.CardModels
{
    public partial class Authorised
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string UserName { get; set; }
        public string AppId { get; set; }
        public string AppKey { get; set; }
    }
}
