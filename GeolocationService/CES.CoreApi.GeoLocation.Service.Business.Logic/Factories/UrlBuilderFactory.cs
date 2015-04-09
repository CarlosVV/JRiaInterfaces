using System;
using System.Collections.Generic;
using System.Globalization;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Builders;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Factories
{
    public class UrlBuilderFactory : Dictionary<string, Func<BaseUrlBuilder>>, IUrlBuilderFactory
    {
        #region Core

        private const string UrlBuilderRegistrationNameTemplate = "I{0}UrlBuilder";
        
        #endregion

        #region Public methods

        public T GetInstance<T>(DataProviderType providerType, FactoryEntity entity) where T : class
        {
            var name = GetRegistrationName(providerType, entity);
            return this[name]() as T;
        }

        #endregion

        #region Private methods

        private static string GetRegistrationName(DataProviderType providerType, FactoryEntity entity)
        {
            if (providerType == DataProviderType.Undefined)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                        SubSystemError.GeneralInvalidParameterValue,
                        "providerType", providerType);

            if (entity == FactoryEntity.Undefined)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                        SubSystemError.GeneralInvalidParameterValue,
                        "entity", entity);

            return string.Format(
                       CultureInfo.InvariantCulture,
                       UrlBuilderRegistrationNameTemplate,
                       providerType);
        }


        #endregion
    }
}
