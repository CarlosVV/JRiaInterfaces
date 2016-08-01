using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace CES.CoreApi.Payout.ViewModels
{
	[DataContract]
	public class Sender
	{
		//public string Name { get; set; }
		
		[DataMember(Name = "FirstName")]
		public string CustomerNameFirst { get; set; }
		public string CustomerNameMid { get; set; }
		[DataMember(Name = "LastName1")]
		public string CustomerNameLast1 { get; set; }
	
		public string CustomerNameLast2 { get; set; }
		[DataMember(Name = "Address")]
		public string CustAddress { get; set; }
		[DataMember(Name = "City")]
		public string CustCity { get; set; }
		[DataMember(Name = "State")]
		public string CustState { get; set; }
		[DataMember(Name = "Country")]
		public string CustCountry { get; set; }
		[DataMember(Name = "PhoneNumber")]
		public string CustomerTelNo { get; set; }
		public string CustPostalCode { get; set; }
	}
}