using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.DTO.MetaObjects
{
   public class MetaAuthenticatorResponse
    {
        public string access_token { get; set; }
        public int expiresIn { get; set; }
        public Payload payload { get; set; }
    }
    public class Payload
    {
        public User user { get; set; }
    }
    public class User
    {
        public string _id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
}
