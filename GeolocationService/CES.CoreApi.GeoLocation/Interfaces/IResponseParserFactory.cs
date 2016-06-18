using CES.CoreApi.GeoLocation.Enumerations;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces
{
    public interface IResponseParserFactory
    {
        T GetInstance<T>(DataProviderType providerType) where T : class;
    }
}