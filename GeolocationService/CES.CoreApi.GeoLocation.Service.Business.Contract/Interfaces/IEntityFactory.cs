using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces
{
    public interface IEntityFactory
    {
        /// <summary>
        /// Builds Url Builder instance according provider and service type
        /// </summary>
        /// <param name="providerType">Data provider type</param>
        /// <param name="entity">Factory entity</param>
        /// <returns></returns>
        T GetInstance<T>(DataProviderType providerType, FactoryEntity entity) where T : class;
    }
}