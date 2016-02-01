using CES.CoreApi.Compliance.Service.Business.Contract.Enumerations;

namespace CES.CoreApi.Compliance.Service.Business.Contract.Configuration
{
    public class DataProviderConfiguration
    {
        public DataProviderServiceType DataProviderServiceType { get; set; }
        public CheckPayoutProviderType DataProviderType { get; set; }
        public int Priority { get; set; }
    }
}
