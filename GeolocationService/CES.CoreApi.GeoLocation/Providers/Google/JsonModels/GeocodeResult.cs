using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.GeoLocation.Providers.Google.JsonModels
{
	internal class GeocodeResult
	{
		public List<AddressComponent> address_components { get; set; }
		public string formatted_address { get; set; }
		public Geometry geometry { get; set; }
		public bool partial_match { get; set; }
		public List<string> types { get; set; }
	}

	internal class GeocodeResults
	{
		public List<GeocodeResult> results { get; set; }
		public string status { get; set; }
	}
}
