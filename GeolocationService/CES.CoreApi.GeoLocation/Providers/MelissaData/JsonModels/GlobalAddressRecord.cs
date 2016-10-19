using System.Collections.Generic;

namespace CES.CoreApi.GeoLocation.Providers.MelissaData.JsonModels
{
	public class GlobalAddressModel
	{
		public string TransmissionResults { get; set; }
		public List<GlobalAddressRecord> Records { get; set; }
	}
	public class GlobalAddressRecord
	{	
		public string Results { get; set; }
		public string FormattedAddress { get; set; }
		public string AddressLine1 { get; set; }
		public string AddressLine2 { get; set; }
		public string Locality { get; set; }
		public string AdministrativeArea { get; set; }
		public string PostalCode { get; set; }
		public string CountryName { get; set; }
		public string CountryISO3166_1_Alpha2 { get; set; }

		public string PremisesNumber { get; set; }
		public string Latitude { get; set; }
		public string Longitude { get; set; }

		public string Thoroughfare { get; set; }
	}
}
