using System.Collections.Generic;

namespace CES.CoreApi.GeoLocation.ClientSettings
{
    public class ClientAppSetting
	{
		public int ApplicationId { get; set; }
		public string CountryCode { get; set; }
		public string DefaultGeoLocationProvider { get; set; }
        public List<DataProvider> DataProviders { get; set; }
    }
}
