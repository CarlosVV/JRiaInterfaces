using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Configuration
{
    public class DataProviderConfiguration
    {
        public DataProviderServiceType DataProviderServiceType { get; set; }
        public DataProviderType DataProviderType { get; set; }
        public int Priority { get; set; }
    }
}
