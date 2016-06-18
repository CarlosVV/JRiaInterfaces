using System.Collections.Generic;

namespace CES.CoreApi.GeoLocation.Configuration
{
    public class CountryConfiguration
    {
        public string CountryCode { get; set; }
        public List<DataProviderConfiguration> DataProviders { get; set; }
    }
}
