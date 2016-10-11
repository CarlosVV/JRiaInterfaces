using CES.CoreApi.GeoLocation.Enumerations;
namespace CES.CoreApi.GeoLocation.ClientSettings
{
	public class DataProvider
    {
        public DataProviderServiceType DataProviderServiceType { get; set; }
        public DataProviderType DataProviderType { get; set; }
        public int Priority { get; set; }
    }
}
