
using CES.CoreApi.GeoLocation.Enumerations;

namespace CES.CoreApi.GeoLocation.Configuration
{
    public class DataProviderConfiguration
    {
        public DataProviderServiceType DataProviderServiceType { get; set; }
        public DataProviderType DataProviderType { get; set; }
        public int Priority { get; set; }
    }
}
