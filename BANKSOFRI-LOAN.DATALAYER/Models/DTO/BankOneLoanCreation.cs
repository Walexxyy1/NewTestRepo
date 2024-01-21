using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.DATALAYER.Models.DTO
{
   public class BankOneLoanCreation
    {
    }

    public class BankOneLoanCreationRequest
    {
        public string TransactionTrackingRef { get; set; }
        public string LoanProductCode { get; set; }
        public string CustomerID { get; set; }
        public string LinkedAccountNumber { get; set; }
        public string CollateralDetails { get; set; }
        public string CollateralType { get; set; }
        public int ComputationMode { get; set; }
        public int Tenure { get; set; }
        public int Moratorium { get; set; }
        public DateTime InterestAccrualCommencementDate { get; set; }
        public int Amount { get; set; }
        public int InterestRate { get; set; }
        public int PrincipalPaymentFrequency { get; set; }
        public int InterestPaymentFrequency { get; set; }

    }
    public class BankOneLoanCreationResponse
    {
        public bool IsSuccessful { get; set; }
        public object CustomerIDInString { get; set; }
        public string Message { get; set; }
        public object TransactionTrackingRef { get; set; }
        public object Page { get; set; }
    }

    public class AccountlienResponse
    {
        public string RequestStatus { get; set; }
        public string ResponseDescription { get; set; }
        public string ResponseStatus { get; set; }
    }

    public class AccountlienRequestObject
    {
        public string AccountNo { get; set; }
        public string AuthenticationCode { get; set; }
        public string ReferenceID { get; set; }
        public string Amount { get; set; }
        public string Reason { get; set; }
    }

    public class RemovelientOnAccountRequestObject
    {
        public string AccountNo { get; set; }
        public string AuthenticationCode { get; set; }
        public string ReferenceID { get; set; }
        public string Amount { get; set; }
        public string Reason { get; set; }
    }
}
