using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.DTO
{
    public class CRCResponseUpdated
    {
        public CRUConsumerHitResponse ConsumerHitResponse { get; set; }
    }

    public class CRUADDRESSES
    {
    }

    public class CRUBODY
    {
        public object AddressHistory { get; set; }
        public object Amount_OD_BucketCURR1 { get; set; }
        public CONSUMERRELATION CONSUMER_RELATION { get; set; }
        public object CREDIT_MICRO_SUMMARY { get; set; }
        public CREDITNANOSUMMARY CREDIT_NANO_SUMMARY { get; set; }
        public CREDITSCOREDETAILS CREDIT_SCORE_DETAILS { get; set; }
        public List<SEARCHRESULTLIST> SEARCHRESULTLIST { get; set; }
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

    public class CRUCONSUMERDETAILS
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

    public class CRUConsumerHitResponse
    {
        public BODY BODY { get; set; }
        public HEADER HEADER { get; set; }
        public string REQUESTID { get; set; }
    }

    public class CRUCONSUMERRELATION
    {
    }

    public class CRUCREDITNANOSUMMARY
    {
        public SUMMARY SUMMARY { get; set; }
    }

    public class CRUCREDITSCOREDETAILS
    {
        public CREDITSCORESUMMARY CREDIT_SCORE_SUMMARY { get; set; }
    }

    public class CRUCREDITSCORESUMMARY
    {
        public string CREDIT_RATING { get; set; }
        public int CREDIT_SCORE { get; set; }
        public string REASON_CODE1 { get; set; }
        public string REASON_CODE2 { get; set; }
        public string REASON_CODE3 { get; set; }
        public string REASON_CODE4 { get; set; }
    }
    public class CRUSEARCHRESULTLIST
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
    public class CRUHEADER
    {
        public REPORTHEADER REPORTHEADER { get; set; }
        public RESPONSETYPE RESPONSETYPE { get; set; }
        public SEARCHCRITERIA SEARCHCRITERIA { get; set; }
        public SEARCHRESULTLIST SEARCHRESULTLIST { get; set; }
    }

    public class CRUIDENTIFICATION
    {
        public object EXP_DATE { get; set; }
        public bool EXP_DATESpecified { get; set; }
        public string ID_DISPLAY_NAME { get; set; }
        public string ID_VALUE { get; set; }
        public string RUID { get; set; }
        public string SOURCE_ID { get; set; }
    }

    public class CRUIDENTIFIERS
    {
    }

    public class CRUMFCREDITNANOSUMMARY
    {
        public SUMMARY SUMMARY { get; set; }
    }

    public class CRUMGCREDITNANOSUMMARY
    {
        public SUMMARY SUMMARY { get; set; }
    }

    public class CRUNANOCONSUMERPROFILE
    {
        public CONSUMERDETAILS CONSUMER_DETAILS { get; set; }
    }

    public class CRUREASON
    {
    }

    public class CRUREPORTHEADER
    {
        public string MAILTO { get; set; }
        public string PRODUCTNAME { get; set; }
        public REASON REASON { get; set; }
        public string REPORTDATE { get; set; }
        public string REPORTORDERNUMBER { get; set; }
        public string USERID { get; set; }
    }

    public class CRURESPONSETYPE
    {
        public string CODE { get; set; }
        public string DESCRIPTION { get; set; }
    }

    public class CRUSEARCHCRITERIA
    {
        public object BRANCHCODE { get; set; }
        public string BVN_NO { get; set; }
        public object CFACCOUNTNUMBER { get; set; }
        public object DATEOFBIRTH { get; set; }
        public object GENDER { get; set; }
        public object NAME { get; set; }
        public object TELEPHONE_NO { get; set; }
    }

    public class CRUSEARCHRESULTITEM
    {
        public ADDRESSES ADDRESSES { get; set; }
        public string BUREAUID { get; set; }
        public string CONFIDENCESCORE { get; set; }
        public IDENTIFIERS IDENTIFIERS { get; set; }
        public string NAME { get; set; }
        public SURROGATES SURROGATES { get; set; }
    }

   

    public class CRUSUMMARY
    {
        public string HAS_CREDITFACILITIES { get; set; }
        public string LAST_REPORTED_DATE { get; set; }
        public string NO_OF_DELINQCREDITFACILITIES { get; set; }
    }

    public class CRUSURROGATES
    {
    }
}
