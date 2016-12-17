using System.Collections.Generic;

namespace CES.CoreApi.GeoLocation.Providers.Bing.JsonModels
{
	public class BingResource
	{
		public string name { get; set; }

		public Point point { get; set; }
		public BingAddress address { get; set; }
		public string confidence {get;set;}
		public string entityType { get; set; }
		public List<GeocodePoint> geocodePoints { get; set; }
		public string[] matchCodes { get; set; }


	}
}
