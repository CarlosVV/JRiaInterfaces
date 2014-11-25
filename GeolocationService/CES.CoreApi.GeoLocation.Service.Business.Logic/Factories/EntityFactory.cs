using System;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Factories
{
    public class EntityFactory : IEntityFactory
    {
         #region Core

        private readonly IIocContainer _container;
        private readonly IRegistrationNameProvider _registrationNameProvider;

        public EntityFactory(IIocContainer container)
        {
            if (container == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                  SubSystemError.GeneralRequiredParameterIsUndefined, "container");

            _container = container;
            _registrationNameProvider = _container.Resolve<IRegistrationNameProvider>();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Builds Url Builder instance according provider and entity type
        /// </summary>
        /// <param name="providerType">Data provider type</param>
        /// <param name="entity">Factory entity</param>
        /// <returns></returns>
        public T GetInstance<T>(DataProviderType providerType, FactoryEntity entity) where T : class
        {
            var registrationName = _registrationNameProvider.GetRegistrationName(providerType, entity);

            T foundEntity;

            try
            {
                foundEntity = _container.Resolve<T>(registrationName);
            }
            catch (Exception)
            {
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeolocationFactoryEntityNotRegisteredInContainer,
                    providerType, entity);
            }

            return foundEntity;
        }

        #endregion
    }
}
