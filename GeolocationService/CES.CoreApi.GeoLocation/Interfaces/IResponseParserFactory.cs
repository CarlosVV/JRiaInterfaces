using CES.CoreApi.GeoLocation.Enumerations;

namespace CES.CoreApi.GeoLocation.Interfaces
{
    public interface IResponseParserFactory
    {
        T GetInstance<T>(DataProviderType providerType) where T : class;
    }
}