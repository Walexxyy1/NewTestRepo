using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.DTO
{
    public class CRCSCResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public CRCResponseUpdated ScoreDetails { get; set; }
    }
    public class CRCCreditScoreResponse
    {
        public ConsumerHitResponse ConsumerHitResponse { get; set; }
    }
    public class CRCADDRESSES
    {
    }

    public class CRCBODY
    {
        public string AddressHistory { get; set; }
        public string Amount_OD_BucketCURR1 { get; set; }
        public CONSUMERRELATION CONSUMER_RELATION { get; set; }
        public string CREDIT_MICRO_SUMMARY { get; set; }
        public CREDITNANOSUMMARY CREDIT_NANO_SUMMARY { get; set; }
        public CREDITSCOREDETAILS CREDIT_SCORE_DETAILS { get; set; }
        public string ClassificationInsType { get; set; }
        public string ClassificationProdType { get; set; }
        public string ClosedAccounts { get; set; }
        public string ConsCommDetails { get; set; }
        public string ConsumerMergerDetails { get; set; }
        public string ContactHistory { get; set; }
        public string CreditDisputeDetails { get; set; }
        public string CreditFacilityHistory24 { get; set; }
        public string CreditProfileOverview { get; set; }
        public string CreditProfileSummaryCURR1 { get; set; }
        public string CreditProfileSummaryCURR2 { get; set; }
        public string CreditProfileSummaryCURR3 { get; set; }
        public string CreditProfileSummaryCURR4 { get; set; }
        public string CreditProfileSummaryCURR5 { get; set; }
        public string DMMDisputeSection { get; set; }
        public string DODishonoredChequeDetails { get; set; }
        public string DOJointHolderDetails { get; set; }
        public string DOLitigationDetails { get; set; }
        public string DisclaimerDetails { get; set; }
        public string EmploymentHistory { get; set; }
        public string GuaranteedLoanDetails { get; set; }
        public string InquiryHistoryDetails { get; set; }
        public string Inquiry_Product { get; set; }
        public string LegendDetails { get; set; }
        public string MFCREDIT_MICRO_SUMMARY { get; set; }
        public MFCREDITNANOSUMMARY MFCREDIT_NANO_SUMMARY { get; set; }
        public string MGCREDIT_MICRO_SUMMARY { get; set; }
        public MGCREDITNANOSUMMARY MGCREDIT_NANO_SUMMARY { get; set; }
        public string MIC_CONSUMER_PROFILE { get; set; }
        public NANOCONSUMERPROFILE NANO_CONSUMER_PROFILE { get; set; }
        public string RelatedToDetails { get; set; }
        public string ReportDetail { get; set; }
        public string ReportDetailAcc { get; set; }
        public string ReportDetailBVN { get; set; }
        public string ReportDetailMob { get; set; }
        public string ReportDetailsSIR { get; set; }
        public string SecurityDetails { get; set; }
        public string SummaryOfPerformance { get; set; }
    }

    public class CONSUMERDETAILS
    {
        public string CITIZENSHIP { get; set; }
        public string DATE_OF_BIRTH { get; set; }
        public string FIRST_NAME { get; set; }
        public string GENDER { get; set; }
        public List<IDENTIFICATION> IDENTIFICATION { get; set; }
        public object IDENTIFICATION_DETAILS { get; set; }
        public string LAST_NAME { get; set; }
        public string NAME { get; set; }
        public string RUID { get; set; }
    }

    public class ConsumerHitResponse
    {
        public BODY BODY { get; set; }
        public CRCHEADER HEADER { get; set; }
        public string REQUESTID { get; set; }
    }

    public class CONSUMERRELATION
    {
    }

    public class CREDITNANOSUMMARY
    {
        public SUMMARY SUMMARY { get; set; }
    }

    public class CREDITSCOREDETAILS
    {
        public CREDITSCORESUMMARY CREDIT_SCORE_SUMMARY { get; set; }
    }

    public class CREDITSCORESUMMARY
    {
        public string CREDIT_RATING { get; set; }
        public int CREDIT_SCORE { get; set; }
        public string REASON_CODE1 { get; set; }
        public string REASON_CODE2 { get; set; }
        public string REASON_CODE3 { get; set; }
        public string REASON_CODE4 { get; set; }
    }

    public class CRCHEADER
    {
        public REPORTHEADER REPORTHEADER { get; set; }
        public RESPONSETYPE RESPONSETYPE { get; set; }
        public SEARCHCRITERIA SEARCHCRITERIA { get; set; }
        public SEARCHRESULTLIST SEARCHRESULTLIST { get; set; }
    }

    public class IDENTIFICATION
    {
        public object EXP_DATE { get; set; }
        public bool EXP_DATESpecified { get; set; }
        public string ID_DISPLAY_NAME { get; set; }
        public string ID_VALUE { get; set; }
        public string RUID { get; set; }
        public string SOURCE_ID { get; set; }
    }

    public class CRCIDENTIFIERS
    {
    }

    public class MFCREDITNANOSUMMARY
    {
        public SUMMARY SUMMARY { get; set; }
    }

    public class MGCREDITNANOSUMMARY
    {
        public SUMMARY SUMMARY { get; set; }
    }

    public class NANOCONSUMERPROFILE
    {
        public CONSUMERDETAILS CONSUMER_DETAILS { get; set; }
    }

    public class REASON
    {
    }

    public class REPORTHEADER
    {
        public string MAILTO { get; set; }
        public string PRODUCTNAME { get; set; }
        public REASON REASON { get; set; }
        public string REPORTDATE { get; set; }
        public string REPORTORDERNUMBER { get; set; }
        public string USERID { get; set; }
    }

    public class CRCRESPONSETYPE
    {
        public string CODE { get; set; }
        public string DESCRIPTION { get; set; }
    }
    public class SEARCHCRITERIA
    {
        public object BRANCHCODE { get; set; }
        public object BVN_NO { get; set; }
        public object CFACCOUNTNUMBER { get; set; }
        public string DATEOFBIRTH { get; set; }
        public string GENDER { get; set; }
        public string NAME { get; set; }
        public object TELEPHONE_NO { get; set; }
    }

    public class SEARCHRESULTITEM
    {
        public CRCADDRESSES ADDRESSES { get; set; }
        public string BUREAUID { get; set; }
        public string CONFIDENCESCORE { get; set; }
        public CRCIDENTIFIERS IDENTIFIERS { get; set; }
        public string NAME { get; set; }
        public CRCSURROGATES SURROGATES { get; set; }
    }

    public class CRCSEARCHRESULTLIST
    {
        public SEARCHRESULTITEM SEARCHRESULTITEM { get; set; }
    }

    public class SUMMARY
    {
        public string HAS_CREDITFACILITIES { get; set; }
        public string LAST_REPORTED_DATE { get; set; }
        public string NO_OF_DELINQCREDITFACILITIES { get; set; }
    }

    public class CRCSURROGATES
    {
    }
}
