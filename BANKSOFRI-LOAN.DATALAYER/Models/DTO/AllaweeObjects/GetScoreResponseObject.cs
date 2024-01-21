using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.DATALAYER.Models.DTO.AllaweeObjects
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Data
    {
        public int score { get; set; }
        public object smartScores { get; set; }
        public string rating { get; set; }
        public List<string> reasons { get; set; }
    }

    public class Score
    {
        public string source { get; set; }
        public Data data { get; set; }
    }
    public class DataScore
    {
        public List<Score> score { get; set; }
    }
    public class GetScoreResponseObject
    {
        public string code { get; set; }
        public string message { get; set; }
        public DataScore data { get; set; }
    }
  

    public class GetScoreRequestObject
    {
        public string fields { get; set; }
        public string customerId { get; set; }
        public string BVN { get; set; }
    }



}
