using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.DTO
{
    public class Credit202Response
    {
        public List<SMARTScore> SMARTScores { get; set; }
        public List<ScoreFactor> ScoreFactors { get; set; }
        public bool Success { get; set; }
        public List<object> Errors { get; set; }
        public string InfoMessage { get; set; }
        public string TransactionID { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
   

    public class ScoreFactor
    {
        public string RegistryID { get; set; }
        public string ScoreFactorType { get; set; }
        public string ScoreFactorNarrative { get; set; }
    }

    public class SMARTScore
    {
        public string RegistryID { get; set; }
        public int GenericScore { get; set; }
    }


}
