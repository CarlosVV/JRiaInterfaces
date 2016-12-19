using System.Collections.Generic;

namespace CES.CoreApi.GeoLocation.Providers.Bing.JsonModels
{
	public class BingResult
	{
		public string authenticationResultCode { get; set; }
		public int statusCode { get; set; }
		public List<BingResourceSet> resourceSets { get; set; }
		public string statusDescription { get; set; }
	}

	public class BingResourceSet	{
		public List<BingResource> resources { get; set; }
	}
}
