using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces
{
    public interface IResponseParserFactory
    {
        T GetInstance<T>(DataProviderType providerType) where T : class;
    }
}