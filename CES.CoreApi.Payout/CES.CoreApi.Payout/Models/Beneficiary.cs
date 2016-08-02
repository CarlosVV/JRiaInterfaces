using System.Runtime.Serialization;

namespace CES.CoreApi.Payout.Models
{
	[DataContract]
	public class Beneficiary
	{
		//public string Name { get; set; }
	
		[DataMember(Name = "FirstName")]
		public string BeneficiaryNameFirst { get; set; }

		[DataMember(Name = "LastName1")]
		public string BeneficiaryNameLast1 { get; set; }
		[DataMember(Name = "IDNumber")]
		public int BenID { get; set; }
		[DataMember(Name = "PhoneNumber")]
		public string BenTelNo { get; set; }

		[DataMember(Name = "Address")]
		public string BenAddress { get; set; }

		[DataMember(Name = "City")]
		public string BenCity { get; set; }

		[DataMember(Name = "State")]
		public string BenState { get; set; }

		[DataMember(Name = "Country")]
		public string BenCountry { get; set; }	
	
	}
}