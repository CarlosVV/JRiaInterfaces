using Newtonsoft.Json;

namespace CES.CoreApi.GeoLocation.Models.Responses.Validate
{
	public class AddressComponent
	{
		public string FormattedAddress { get; set; }
		public string StreetNumber { get; set; }
		public string Street { get; set; }
		public string StreetLongName { get; set; }
		public string Locality { get; set; }
		public string LocalityLongName { get; set; }
		public string AdministrativeArea { get; set; }
		public string AdministrativeAreaLongName { get; set; }
		public string PostalCode { get; set; }
		public string PostalCodeLongName { get; set; }
		public string Country { get; set; }
		public string CountryName { get; set; }

		[JsonIgnore]
		public double AddressDistance { get; set; }
		[JsonIgnore]
		public double CityDistance { get; set; }
		[JsonIgnore]
		public double StateDistance { get; set; }
		[JsonIgnore]
		public double PostalCodeDistance { get; set; }
		[JsonIgnore]
		public double TotalAddressAndZipDistance { get { return AddressDistance + PostalCodeDistance; } }
	}
}
