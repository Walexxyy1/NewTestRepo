using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.DTO.MonoObjects
{
   public class MonoStatementResponse
    {
        public Meta meta { get; set; }
        public List<ResponseData> data { get; set; }
    }
    public class ResponseData
    {
        public string _id { get; set; }
        public string type { get; set; }
        public int amount { get; set; }
        public string narration { get; set; }
        public DateTime date { get; set; }
        public int balance { get; set; }
        public string currency { get; set; }
    }

    public class ResponseMeta
    {
        public int count { get; set; }
        public int requested_length { get; set; }
        public int available_length { get; set; }
    }
}
