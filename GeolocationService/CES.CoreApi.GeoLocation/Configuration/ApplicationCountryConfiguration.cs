using System.Collections.Generic;

namespace CES.CoreApi.GeoLocation.Configuration
{
    public class ApplicationCountryConfiguration
    {
        public List<CountryConfiguration> CountryConfigurations { get; set; }
        public int ApplicationId { get; set; }
    }
}
