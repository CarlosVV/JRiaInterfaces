using System;
using System.Collections.Generic;


namespace CES.CoreApi.GeoLocation.Logics.Parsers.ProviderResponses
{
	internal class result
	{
		public List<address_component> address_components { get; set; }
		public string formatted_address { get; set; }
		public Geometry geometry { get; set; }
		public bool partial_match { get; set; }
		public List<string> types { get; set; }
	}
	internal class address_component
	{
		public string long_name { get; set; }
		public string short_name { get; set; }
		public List<string> types { get; set; }
	}
	internal class Geocode
	{
		public double lat { get; set; }
		public double lng { get; set; }
	}
	internal class Geometry
	{
		public Geocode location { get; set; }
		public string location_type { get; set; }


	}

	internal class GoogleGeocode
	{
		public List<result> results { get; set; }
		public string status { get; set; }
	}
}
