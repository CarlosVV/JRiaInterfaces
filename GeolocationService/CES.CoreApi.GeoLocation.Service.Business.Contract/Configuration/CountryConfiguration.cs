using System.Collections.Generic;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Configuration
{
    public class CountryConfiguration
    {
        public string CountryCode { get; set; }
        public List<DataProviderConfiguration> DataProviders { get; set; }
    }
}
