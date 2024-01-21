using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BANKSOFRI_LOAN.DATALAYER.Models.DTO.ProvenirObjects
{
   public class ProvenirDecisionResponse
    {
		// using System.Xml.Serialization;
		// XmlSerializer serializer = new XmlSerializer(typeof(Data));
		// using (StringReader reader = new StringReader(xml))
		// {
		//    var test = (Data)serializer.Deserialize(reader);
		// }

		[XmlRoot(ElementName = "Bureau")]
		public class Bureau
		{

			[XmlAttribute(AttributeName = "balanceTotal")]
			public int BalanceTotal { get; set; }

			[XmlAttribute(AttributeName = "BVN")]
			public string BVN { get; set; }

			[XmlAttribute(AttributeName = "countAccountStatusClosed")]
			public int CountAccountStatusClosed { get; set; }

			[XmlAttribute(AttributeName = "countAccountStatusDelinquent30over60days")]
			public int CountAccountStatusDelinquent30over60days { get; set; }

			[XmlAttribute(AttributeName = "countAccountStatusPerforming")]
			public int CountAccountStatusPerforming { get; set; }

			[XmlAttribute(AttributeName = "countAccountStatusWrittenOff")]
			public int CountAccountStatusWrittenOff { get; set; }

			[XmlAttribute(AttributeName = "score")]
			public int Score { get; set; }
		}

		[XmlRoot(ElementName = "Contact")]
		public class Contact
		{

			[XmlAttribute(AttributeName = "email")]
			public string Email { get; set; }

			[XmlAttribute(AttributeName = "phone")]
			public int Phone { get; set; }
		}

		[XmlRoot(ElementName = "Employment")]
		public class Employment
		{

			[XmlAttribute(AttributeName = "duration")]
			public int Duration { get; set; }

			[XmlAttribute(AttributeName = "employerName")]
			public string EmployerName { get; set; }

			[XmlAttribute(AttributeName = "income")]
			public int Income { get; set; }

			[XmlAttribute(AttributeName = "incomeVerified")]
			public bool IncomeVerified { get; set; }

			[XmlAttribute(AttributeName = "industry")]
			public string Industry { get; set; }

			[XmlAttribute(AttributeName = "type")]
			public string Type { get; set; }
		}

		[XmlRoot(ElementName = "Location")]
		public class Location
		{

			[XmlAttribute(AttributeName = "city")]
			public string City { get; set; }

			[XmlAttribute(AttributeName = "country")]
			public string Country { get; set; }

			[XmlAttribute(AttributeName = "postalCode")]
			public string PostalCode { get; set; }

			[XmlAttribute(AttributeName = "residenceStatus")]
			public string ResidenceStatus { get; set; }

			[XmlAttribute(AttributeName = "state")]
			public string State { get; set; }

			[XmlAttribute(AttributeName = "street")]
			public string Street { get; set; }
		}

		[XmlRoot(ElementName = "Applicant")]
		public class Applicant
		{

			[XmlElement(ElementName = "Bureau")]
			public Bureau Bureau { get; set; }

			[XmlElement(ElementName = "Contact")]
			public Contact Contact { get; set; }

			[XmlElement(ElementName = "Employment")]
			public Employment Employment { get; set; }

			[XmlElement(ElementName = "Location")]
			public Location Location { get; set; }

			[XmlAttribute(AttributeName = "dateOfBirth")]
			public DateTime DateOfBirth { get; set; }

			[XmlAttribute(AttributeName = "firstname")]
			public string Firstname { get; set; }

			[XmlAttribute(AttributeName = "lastname")]
			public string Lastname { get; set; }

			[XmlAttribute(AttributeName = "levelOfEducation")]
			public string LevelOfEducation { get; set; }

			[XmlAttribute(AttributeName = "gender")]
			public string Gender { get; set; }

			[XmlAttribute(AttributeName = "maritalStatus")]
			public string MaritalStatus { get; set; }

			[XmlAttribute(AttributeName = "kycScore")]
			public string KycScore { get; set; }

			[XmlAttribute(AttributeName = "ontimeRepaymentRate")]
			public int OntimeRepaymentRate { get; set; }

			[XmlAttribute(AttributeName = "type")]
			public string Type { get; set; }
		}

		[XmlRoot(ElementName = "Applicants")]
		public class Applicants
		{

			[XmlElement(ElementName = "Applicant")]
			public Applicant Applicant { get; set; }
		}

		[XmlRoot(ElementName = "offer")]
		public class Offer
		{

			[XmlAttribute(AttributeName = "tenor")]
			public int Tenor { get; set; }

			[XmlAttribute(AttributeName = "rate")]
			public int Rate { get; set; }

			[XmlAttribute(AttributeName = "maxAmount")]
			public int MaxAmount { get; set; }
		}

		[XmlRoot(ElementName = "offers")]
		public class Offers
		{

			[XmlElement(ElementName = "offer")]
			public Offer Offer { get; set; }
		}

		[XmlRoot(ElementName = "Decision")]
		public class Decision
		{

			[XmlElement(ElementName = "offers")]
			public Offers Offers { get; set; }

			[XmlAttribute(AttributeName = "overallDecision")]
			public string OverallDecision { get; set; }

			[XmlAttribute(AttributeName = "guid")]
			public string Guid { get; set; }

			[XmlAttribute(AttributeName = "riskLevel")]
			public string RiskLevel { get; set; }
		}

		[XmlRoot(ElementName = "Application")]
		public class Application
		{

			[XmlElement(ElementName = "Applicants")]
			public Applicants Applicants { get; set; }

			[XmlElement(ElementName = "Decision")]
			public Decision Decision { get; set; }

			[XmlAttribute(AttributeName = "desiredAmount")]
			public int DesiredAmount { get; set; }

			[XmlAttribute(AttributeName = "loanNumber")]
			public int LoanNumber { get; set; }

			[XmlAttribute(AttributeName = "reasonForLoan")]
			public string ReasonForLoan { get; set; }

			[XmlAttribute(AttributeName = "requestChannel")]
			public string RequestChannel { get; set; }

			[XmlAttribute(AttributeName = "applicationID")]
			public int ApplicationID { get; set; }

			[XmlAttribute(AttributeName = "clientID")]
			public int ClientID { get; set; }
		}

		[XmlRoot(ElementName = "LinksMF")]
		public class LinksMF
		{

			[XmlElement(ElementName = "Application")]
			public Application Application { get; set; }
		}

		[XmlRoot(ElementName = "Temporary")]
		public class Temporary
		{

			[XmlAttribute(AttributeName = "rawXML")]
			public string RawXML { get; set; }
		}

		[XmlRoot(ElementName = "Data")]
		public class Data
		{

			[XmlElement(ElementName = "LinksMF")]
			public LinksMF LinksMF { get; set; }

			[XmlElement(ElementName = "Temporary")]
			public Temporary Temporary { get; set; }
		}


	}
}
