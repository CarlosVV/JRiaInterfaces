using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces
{
    public interface IResponseParserFactory
    {
        T GetInstance<T>(DataProviderType providerType, FactoryEntity entity) where T : class;
    }
}