using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.DATALAYER.DTO
{
    public class AllaweeResponse
    {
        public string code { get; set; }
        public string message { get; set; }
        public CreditData data { get; set; }
    }
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class PaymentHistory
        {
            public string month { get; set; }
            public int overdueDays { get; set; }
        }

        public class CreditData
        {
            public string accountNo { get; set; }
            public string institutionName { get; set; }
            public string creditType { get; set; }
            public string currency { get; set; }
            public DateTime? date { get; set; }
            public DateTime maturityDate { get; set; }
            public int loanAmount { get; set; }
            public double installmentAmount { get; set; }
            public DateTime? lastPaymentDate { get; set; }
            public double outstandingBalance { get; set; }
            public int overdueAmount { get; set; }
            public string status { get; set; }
            public string performanceStatus { get; set; }
            public DateTime? dateReported { get; set; }
            public DateTime? lastUpdated { get; set; }
            public object delinquencyStatus { get; set; }
            public List<PaymentHistory> paymentHistory { get; set; }
            public int totalAccounts { get; set; }
            public int totalBorrowed { get; set; }
            public int totalOutstanding { get; set; }
            public int totalOverdue { get; set; }
            public int performing { get; set; }
            public int performingAmount { get; set; }
            public int nonPerforming { get; set; }
            public int nonPerformingAmount { get; set; }
            public int highestLoanAmount { get; set; }
            public int paidOff { get; set; }
            public int paidOffAmount { get; set; }
            public int? delinquencies { get; set; }
            public int totalDishonouredCheques { get; set; }
            public List<Enquiry> enquiries { get; set; }
            public int score { get; set; }
            public object smartScores { get; set; }
            public string rating { get; set; }
            public List<string> reasons { get; set; }
            public List<Account> accounts { get; set; }
            public List<Summary> summary { get; set; }
            public List<Score> scores { get; set; }
        }

        public class Account
        {
            public string source { get; set; }
            public List<AccountsData> data { get; set; }
        }
        public class AccountsData
        {
            public string accountNo { get; set; }
            public string institutionName { get; set; }
            public string creditType { get; set; }
            public string currency { get; set; }
            public DateTime? date { get; set; }
            public DateTime maturityDate { get; set; }
            public int loanAmount { get; set; }
            public double installmentAmount { get; set; }
            public DateTime? lastPaymentDate { get; set; }
            public double outstandingBalance { get; set; }
            public int overdueAmount { get; set; }
            public string status { get; set; }
            public string performanceStatus { get; set; }
            public DateTime? dateReported { get; set; }
            public DateTime? lastUpdated { get; set; }
            public object delinquencyStatus { get; set; }
            public List<PaymentHistory> paymentHistory { get; set; }
        }

        public class Enquiry
        {
            public string institutionName { get; set; }
            public DateTime date { get; set; }
        }

        public class Summary
        {
            public string source { get; set; }
            public SummaryData data { get; set; }
        }
        public class SummaryData
        {
            public int totalAccounts { get; set; }
            public int totalBorrowed { get; set; }
            public int totalOutstanding { get; set; }
            public int totalOverdue { get; set; }
            public int performing { get; set; }
            public int performingAmount { get; set; }
            public int nonPerforming { get; set; }
            public int nonPerformingAmount { get; set; }
            public int highestLoanAmount { get; set; }
            public int paidOff { get; set; }
            public int paidOffAmount { get; set; }
            public int? delinquencies { get; set; }
            public int totalDishonouredCheques { get; set; }
            public List<Enquiry> enquiries { get; set; }
        }
        public class Score
        {
            public string source { get; set; }
            public ScoreData data { get; set; }
        }
        public class ScoreData
        {
            public int score { get; set; }
            public object smartScores { get; set; }
            public string rating { get; set; }
            public List<string> reasons { get; set; }
        }
       


    
}
