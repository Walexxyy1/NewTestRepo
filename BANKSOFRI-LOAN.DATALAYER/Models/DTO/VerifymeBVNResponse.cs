using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.DATALAYER.Models.DTO
{
   public class VerifymeBVNResponse
    {
        public string status { get; set; }
        public BvnData data { get; set; }
    }
    public class FieldMatches
    {
        public bool firsname { get; set; }
        public bool lastname { get; set; }
        public bool dob { get; set; }
    }

    public class BvnData
    {
        public string bvn { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string middlename { get; set; }
        public string phone { get; set; }
        public string gender { get; set; }
        public string birthdate { get; set; }
        public string photo { get; set; }
        public string maritalStatus { get; set; }
        public string lgaOfResidence { get; set; }
        public string lgaOfOrigin { get; set; }
        public string residentialAddress { get; set; }
        public string stateOfOrigin { get; set; }
        public string enrollmentBank { get; set; }
        public string enrollmentBranch { get; set; }
        public string nameOnCard { get; set; }
        public string title { get; set; }
        public string levelOfAccount { get; set; }
        public FieldMatches fieldMatches { get; set; }
    }
    public class VerifyMeBVNValidationRequestObject
    {
        public string CustomeId { get; set; }
        public string BVN { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string dob { get; set; }

    }
}
