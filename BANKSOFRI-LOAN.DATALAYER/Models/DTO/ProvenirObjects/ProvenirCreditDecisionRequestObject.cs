using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BANKSOFRI_LOAN.DATALAYER.Models.DTO.ProvenirObjects
{
    public class InternalProc
    {
        public string ontime_repayment_rate { get; set; }
        public string KYC_score { get; set; }
    }

    public class Employment
    {
        public string duration { get; set; }
        public string income { get; set; }
        public bool incomeVerified { get; set; }
        public string employer_name { get; set; }
        public string industry { get; set; }
        public string type { get; set; }
    }

    public class Identity
    {
        public bool idVerified { get; set; }
    }

    public class Contact
    {
        public string phone { get; set; }
        public string email { get; set; }
    }

    public class Details
    {
        public string levelOfEducation { get; set; }
        public string firstname { get; set; }
        public string gender { get; set; }
        public string dateOfBirth { get; set; }
        public string maritalStatus { get; set; }
        public string lastname { get; set; }
    }

    public class Location
    {
        public string country { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public string postalCode { get; set; }
        public string residenceStatus { get; set; }
        public string state { get; set; }
    }

    public class Bureau
    {
        public string Count_AccountStatus_Performing { get; set; }
        public string Score { get; set; }
        public string Count_AccountStatus_Delinquent_30_over_60_days { get; set; }
        public string Count_AccountStatus_Closed { get; set; }
        public string Balance_Total { get; set; }
        public string BVN { get; set; }
        public string Count_AccountStatus_Written_Off { get; set; }
    }

    public class ProvenirCreditDecisionRequestObject
    {
        public InternalProc @internal { get; set; }
        public Employment employment { get; set; }
        public Identity identity { get; set; }
        public Contact contact { get; set; }
        public Details details { get; set; }
        public Location location { get; set; }
        public Bureau bureau { get; set; }
        public string clientId { get; set; }
        public string desiredAmount { get; set; }
        public string clientType { get; set; }
        public string loanPurpose { get; set; }
        public string applicationId { get; set; }
        public string loanNumber { get; set; }
        public string productCode { get; set; }
    }

}
