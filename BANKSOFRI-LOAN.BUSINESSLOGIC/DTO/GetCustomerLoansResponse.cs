using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.DTO
{
   public class GetCustomerLoansResponse
    {
        public bool IsSuccessful { get; set; }
        public string CustomerIDInString { get; set; }
        public List<Message> Message { get; set; }
        public object TransactionTrackingRef { get; set; }
        public object Page { get; set; }
    }
    public class StatementPreference
    {
        public int Delivery { get; set; }
        public int Period { get; set; }
    }
    public class Message
    {
        public int NominalAccountType { get; set; }
        public string LastDateRestructured { get; set; }
        public int RestructuredLoanAmount { get; set; }
        public string RestructuredLoanAmountToNaira { get; set; }
        public int RestructuredTenure { get; set; }
        public string RestructuredCommencementDate { get; set; }
        public int LoanPaymentScheduleType { get; set; }
        public bool DisablePenalty { get; set; }
        public double SecurityDeposit { get; set; }
        public int ComputationMode { get; set; }
        public int ComputationModeMultiple { get; set; }
        public int LinkedCurrentAccountID { get; set; }
        public string Product { get; set; }
        public int LoanCycle { get; set; }
        public bool UseDefaultLaonCycle { get; set; }
        public int LoanAmount { get; set; }
        public int DiscountAmount { get; set; }
        public DateTime InterestAccrualCommenceDate { get; set; }
        public string LoanFees { get; set; }
        public int Moratarium { get; set; }
        public int PrincipalRepaymentType { get; set; }
        public int PrincipalPaymentFrequency { get; set; }
        public int InterestRepaymentType { get; set; }
        public int InterestAccrualMode { get; set; }
        public int InterestPaymentFrequency { get; set; }
        public int InterestID { get; set; }
        public double InterestRate { get; set; }
        public bool UseDefaultInterests { get; set; }
        public int DaysAtRisk { get; set; }
        public string RealLoanStatus { get; set; }
        public int LendingModel { get; set; }
        public int SubLendingModel { get; set; }
        public string OtherLendingModel { get; set; }
        public string IPPIS { get; set; }
        public int EconomicSector { get; set; }
        public string OtherEconomicSector { get; set; }
        public bool MoveSecurityDepositOutOfCustomerAccount { get; set; }
        public double DefaultingLoanInterest { get; set; }
        public int LoanUpdateType { get; set; }
        public double RefinanceAmount { get; set; }
        public int RefinanceAmountInKobo { get; set; }
        public int NewTenure { get; set; }
        public bool RestructureFromLoanInception { get; set; }
        public string NewCommencementDate { get; set; }
        public string LoanRestructure { get; set; }
        public string OutstandingBalance { get; set; }
        public bool IsSecurityDepositTaken { get; set; }
        public string SecurityPledged { get; set; }
        public double CollateralValue { get; set; }
        public string CollateralValueToNaira { get; set; }
        public int RestructureLoanCount { get; set; }
        public int RefinanceLoanCount { get; set; }
        public double RefinancedLoanAmount { get; set; }
        public string RefinancedLoanAmountToNaira { get; set; }
        public int RefinancedTenure { get; set; }
        public string LastDateRefinanced { get; set; }
        public string RefinancedCommencementDate { get; set; }
        public string MDAId { get; set; }
        public string MDAFullName { get; set; }
        public string LendingGroupName { get; set; }
        public string LoanWriteOffDate { get; set; }
        public string LoanWriteBackDate { get; set; }
        public bool IsLoanWrittenOff { get; set; }
        public double LoanWriteOffAmount { get; set; }
        public double CummulativeWithdrawalAmount { get; set; }
        public double CummulativeDepositAmount { get; set; }
        public double CummulativeWithdrawalAmountWithExclusions { get; set; }
        public double CummulativeDepositAmountWithExclusions { get; set; }
        public int TotalNoOfDebitTrx { get; set; }
        public int TotalNoOfCreditTrx { get; set; }
        public object Number2 { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateCreatedFinancial { get; set; }
        public string Number { get; set; }
        public int ProductID { get; set; }
        public string ProductCode { get; set; }
        public string ReferenceNo { get; set; }
        public string AccountOfficer { get; set; }
        public string AccountOfficerCode { get; set; }
        public int AccountOfficerID { get; set; }
        public StatementPreference StatementPreference { get; set; }
        public int NotificationPreference { get; set; }
        public string AccountOpenningTrackingRef { get; set; }
        public string Name { get; set; }
        public int BranchID { get; set; }
        public double LedgerBalance { get; set; }
        public double AvailableBalance { get; set; }
        public bool IsRecovaLoan { get; set; }
        public bool IsAutoCheck { get; set; }
        public int AccessLevel { get; set; }
        public int AccountStatus { get; set; }
        public int CustomerID { get; set; }
        public string IntroducerName { get; set; }
        public string FunderID { get; set; }
        public string FunderStr { get; set; }
        public string FunderCode { get; set; }
        public string AccessRestrictionText { get; set; }
        public int UserAccessLevel { get; set; }
        public string WithdrawableAmountInNaira { get; set; }
        public string WithdrawableAmountWithAccessLevelInNaira { get; set; }
        public string BalanceInNaira { get; set; }
        public string LedgerBalanceWithAccessLevelInNaira { get; set; }
        public string AvailableBalanceInNaira { get; set; }
        public string AvailableBalanceWithAccessLevelInNaira { get; set; }
        public string NameAndNumber { get; set; }
        public string IDAndNumber { get; set; }
        public int ID { get; set; }
        public bool IsDeleted { get; set; }
        public string MFBCode { get; set; }
        public bool LogObject { get; set; }
        public bool UseAuditTrail { get; set; }
    }
}
