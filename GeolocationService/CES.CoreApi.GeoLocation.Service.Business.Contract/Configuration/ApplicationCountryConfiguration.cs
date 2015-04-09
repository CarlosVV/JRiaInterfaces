using System.Collections.Generic;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Configuration
{
    public class ApplicationCountryConfiguration
    {
        public List<CountryConfiguration> CountryConfigurations { get; set; }
        public int ApplicationId { get; set; }
    }
}
