using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects
{
    public partial class TestBvnrecord
    {
        public int Id { get; set; }
        public string Bvn { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Dob { get; set; }
        public string Gender { get; set; }
    }
}
