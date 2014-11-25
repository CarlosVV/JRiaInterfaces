using System.Globalization;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Providers
{
    public class RegistrationNameProvider : IRegistrationNameProvider
    {
        #region Core

        private const string ResponseParserRegistrationNameTemplate = "{0}_ResponseParser";
        private const string UrlBuilderRegistrationNameTemplate = "{0}_UrlBuilder";

        #endregion

        #region Public methods

        public string GetRegistrationName(DataProviderType providerType, FactoryEntity entity)
        {
            if (providerType == DataProviderType.Undefined)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                        SubSystemError.GeneralInvalidParameterValue,
                        "providerType", providerType);

            if (entity == FactoryEntity.Undefined)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                        SubSystemError.GeneralInvalidParameterValue,
                        "entity", entity);

            switch (entity)
            {
                case FactoryEntity.Parser:
                    return string.Format(
                        CultureInfo.InvariantCulture,
                        ResponseParserRegistrationNameTemplate,
                        providerType);

                case FactoryEntity.UrlBuilder:
                    return string.Format(
                        CultureInfo.InvariantCulture,
                        UrlBuilderRegistrationNameTemplate,
                        providerType);

                default:
                    throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                        SubSystemError.GeolocationUnsupportedFactoryEntity,
                        entity);
            }
        }

        #endregion
    }
}
