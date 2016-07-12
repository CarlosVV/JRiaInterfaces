using System;
using System.Collections.Generic;
using System.Globalization;

using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Builders;
using CES.CoreApi.GeoLocation.Enumerations;

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
                throw new  Exception("Invalid provider or provider is not setup properly");

			if (entity == FactoryEntity.Undefined)
                throw new Exception("invalid entity");

            return string.Format(
                       CultureInfo.InvariantCulture,
                       UrlBuilderRegistrationNameTemplate,
                       providerType);
        }


        #endregion
    }
}
