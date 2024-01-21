using System;
using System.Collections.Generic;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects
{
    public partial class CustomerAccount
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string CustomerId { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OtherNames { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime? Dob { get; set; }
        public string AccountNumber { get; set; }
        public string Bvn { get; set; }
        public string HomeAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Lga { get; set; }
        public string Image { get; set; }
        public string Signature { get; set; }
        public string ImageType { get; set; }
        public string SignatureType { get; set; }
        public string IdentificationImage { get; set; }
        public string IdentificationType { get; set; }
        public string DocumentType { get; set; }
        public string DocumentFrontPath { get; set; }
        public string DocumentBackPath { get; set; }
        public string LivelinessVideoPath { get; set; }
        public string MattiVerificationId { get; set; }
        public string IdentityVerificationStatus { get; set; }
        public string AccountUpgradeStatus { get; set; }
        public string GeneralStatus { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
