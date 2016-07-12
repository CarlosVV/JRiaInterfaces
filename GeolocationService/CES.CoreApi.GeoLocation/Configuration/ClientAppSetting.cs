using System.Collections.Generic;

namespace CES.CoreApi.GeoLocation.Configuration
{
    public class ClientAppSetting
	{
		public int ApplicationId { get; set; }
		public string CountryCode { get; set; }
		public string DefaultGeoLocationProvider { get; set; }
        public List<DataProviderConfiguration> DataProviders { get; set; }
    }
}
