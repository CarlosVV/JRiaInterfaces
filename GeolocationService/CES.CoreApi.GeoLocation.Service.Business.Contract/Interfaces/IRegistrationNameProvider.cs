using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces
{
    public interface IRegistrationNameProvider
    {
        string GetRegistrationName(DataProviderType providerType, FactoryEntity entity);
    }
}