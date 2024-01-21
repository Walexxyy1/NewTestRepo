using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.DTO
{
   public class CRCResponse
    {
        public ConsumerHitResponse ConsumerHitResponse { get; set; }
    }
    public class ADDRESSES
    {
        public object ADDRESS { get; set; }
    }

    public class BODY
    {
        public object AddressHistory { get; set; }
        public object Amount_OD_BucketCURR1 { get; set; }
        public CONSUMERRELATION CONSUMER_RELATION { get; set; }
        public object CREDIT_MICRO_SUMMARY { get; set; }
        public CREDITNANOSUMMARY CREDIT_NANO_SUMMARY { get; set; }
        public CREDITSCOREDETAILS CREDIT_SCORE_DETAILS { get; set; }
        public object ClassificationInsType { get; set; }
        public object ClassificationProdType { get; set; }
        public object ClosedAccounts { get; set; }
        public object ConsCommDetails { get; set; }
        public object ConsumerMergerDetails { get; set; }
        public object ContactHistory { get; set; }
        public object CreditDisputeDetails { get; set; }
        public object CreditFacilityHistory24 { get; set; }
        public object CreditProfileOverview { get; set; }
        public object CreditProfileSummaryCURR1 { get; set; }
        public object CreditProfileSummaryCURR2 { get; set; }
        public object CreditProfileSummaryCURR3 { get; set; }
        public object CreditProfileSummaryCURR4 { get; set; }
        public object CreditProfileSummaryCURR5 { get; set; }
        public object DMMDisputeSection { get; set; }
        public object DODishonoredChequeDetails { get; set; }
        public object DOJointHolderDetails { get; set; }
        public object DOLitigationDetails { get; set; }
        public object DisclaimerDetails { get; set; }
        public object EmploymentHistory { get; set; }
        public object GuaranteedLoanDetails { get; set; }
        public object InquiryHistoryDetails { get; set; }
        public object Inquiry_Product { get; set; }
        public object LegendDetails { get; set; }
        public object MFCREDIT_MICRO_SUMMARY { get; set; }
        public MFCREDITNANOSUMMARY MFCREDIT_NANO_SUMMARY { get; set; }
        public object MGCREDIT_MICRO_SUMMARY { get; set; }
        public MGCREDITNANOSUMMARY MGCREDIT_NANO_SUMMARY { get; set; }
        public object MIC_CONSUMER_PROFILE { get; set; }
        public NANOCONSUMERPROFILE NANO_CONSUMER_PROFILE { get; set; }
        public object RelatedToDetails { get; set; }
        public object ReportDetail { get; set; }
        public object ReportDetailAcc { get; set; }
        public object ReportDetailBVN { get; set; }
        public object ReportDetailMob { get; set; }
        public object ReportDetailsSIR { get; set; }
        public object SecurityDetails { get; set; }
        public object SummaryOfPerformance { get; set; }

    }

    public class ConsumerSearchResultResponse
    {
        public BODY BODY { get; set; }
        public HEADER HEADER { get; set; }
        public string REFERENCENO { get; set; }
        public string REQUESTID { get; set; }
    }

    public class HEADER
    {
        public REPORTHEADER REPORTHEADER { get; set; }
        public RESPONSETYPE RESPONSETYPE { get; set; }
        public SEARCHCRITERIA SEARCHCRITERIA { get; set; }
        public SEARCHRESULTLIST SEARCHRESULTLIST { get; set; }
    }

    public class IDENTIFIERS
    {
    }

    public class RESPONSETYPE
    {
        public string CODE { get; set; }
        public string DESCRIPTION { get; set; }
    }
    public class SEARCHRESULTLIST
    {
        public ADDRESSES ADDRESSES { get; set; }
        public string BUREAUID { get; set; }
        public string CONFIDENCESCORE { get; set; }
        public string DATEOFBIRTH { get; set; }
        public string GENDER { get; set; }
        public IDENTIFIERS IDENTIFIERS { get; set; }
        public string NAME { get; set; }
        public string PHONENUMBER { get; set; }
        public SURROGATES SURROGATES { get; set; }
    }

    public class SURROGATES
    {
    }
}
