using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.DATALAYER.Models.DTO.CRegistryObjects
{
    public class CRegisteryCreditScoreRequestDTO
    {
        public string CustomerId { get; set; }
        public string BVN { get; set; }
        public decimal EnquiryLoanAmount { get; set; }
        public string CreditScoreEnquiryReason { get; set; }
        public string RetroDate { get; set; }
    }
    public class CustomerCreditScoreRequestObj
    {
        public string SessionCode { get; set; }
        public List<string> CustomerRegistryIDList { get; set; }
        public string EnquiryReason { get; set; }
        public int HistoryLengthInMonths { get; set; }
    }

    public class CustomerCreditScoreResponseObj
    {
        public bool Success { get; set; }
        public List<object> Errors { get; set; }
        public DateTime RetroDate { get; set; }
        public DateTime EnquiryDate { get; set; }
        public int PositiveScoreFactorCount { get; set; }
        public int NegativeScoreFactorCount { get; set; }
        public string ScoreModel { get; set; }
        public string ScoreType { get; set; }
        public double EnquiryLoanAmount { get; set; }
        public List<SMARTScoreResult> SMARTScoreResults { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
    public class NegativeScoreFactor
    {
        public int Impact { get; set; }
        public string ScoreFactor { get; set; }
    }

    public class PositiveScoreFactor
    {
        public int Impact { get; set; }
        public string ScoreFactor { get; set; }
    }

    public class SMARTScore
    {
        public string Type { get; set; }
        public string PopulationSegment { get; set; }
        public int Score { get; set; }
        public List<object> NegativeScoreFactors { get; set; }
        public List<object> PositiveScoreFactors { get; set; }
    }

    public class SMARTScoreResult
    {
        public string AccountOwnerRegistryID { get; set; }
        public string Name { get; set; }
        public List<SMARTScore> SMARTScores { get; set; }
    }
}
