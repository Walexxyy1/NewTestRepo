using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.BUSINESSLOGIC.DTO
{
    public class CustomerDetailsDTO
    {
        public string address { get; set; }
        public string age { get; set; }
        public string BVN { get; set; }
        public string branchCode { get; set; }
        public string customerID { get; set; }
        public string dateOfBirth { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public object localGovernmentArea { get; set; }
        public string name { get; set; }
        public string phoneNumber { get; set; }
        public string state { get; set; }
        public List<Account> Accounts { get; set; }
    }
    public class CustomerDetails
    {
        public string Address { get; set; }
        public string Age { get; set; }
        public string BVN { get; set; }
        public string CustomerID { get; set; }
        public string DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string LocalGovernmentArea { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string State { get; set; }
    }
    public class Account
    {
        public string accessLevel { get; set; }
        public string accountNumber { get; set; }
        public string accountStatus { get; set; }
        public string accountType { get; set; }
        public string availableBalance { get; set; }
        public string branch { get; set; }
        public string customerID { get; set; }
        public string accountName { get; set; }
        public string productCode { get; set; }
        public string dateCreated { get; set; }
        public string lastActivityDate { get; set; }
        public string ledgerBalance { get; set; }
        public string NUBAN { get; set; }
        public object referenceNo { get; set; }
        public string withdrawableAmount { get; set; }
        public string kycLevel { get; set; }
    }
    public class CustomerDetailsResponse
    {
        public CustomerDetails CustomerDetails { get; set; }
        public List<Account> Accounts { get; set; }
    }

    public class CustomerAccounts
    {
        public string accessLevel { get; set; }
        public string accountNumber { get; set; }
        public string accountStatus { get; set; }
        public string accountType { get; set; }
        public string availableBalance { get; set; }
        public string branch { get; set; }
        public string customerID { get; set; }
        public string accountName { get; set; }
        public string productCode { get; set; }
        public string dateCreated { get; set; }
        public string lastActivityDate { get; set; }
        public string ledgerBalance { get; set; }
        public string NUBAN { get; set; }
        public string referenceNo { get; set; }
        public string withdrawableAmount { get; set; }
        public string kycLevel { get; set; }
    }

    public class GetCustomerDetails
    {
        public string address { get; set; }
        public string age { get; set; }
        public string BVN { get; set; }
        public string branchCode { get; set; }
        public string customerID { get; set; }
        public string dateOfBirth { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public object localGovernmentArea { get; set; }
        public string name { get; set; }
        public string phoneNumber { get; set; }
        public string state { get; set; }
        public List<Account> Accounts { get; set; }
    }
}
