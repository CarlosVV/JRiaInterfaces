using System;
using System.Linq;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Configuration;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Constants;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Providers
{
    public class CountryConfigurationProvider : ICountryConfigurationProvider
    {
        #region Core

        private readonly DataProviderServiceConfiguration _configurationProvider;
        private const string DefaultCountry = "default";

        public CountryConfigurationProvider(IConfigurationProvider configurationProvider)
        {
            if (configurationProvider == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "configurationProvider");
            ConfigurationProvider = configurationProvider;
            _configurationProvider = ConfigurationProvider.ReadFromJson<DataProviderServiceConfiguration>(ConfigurationConstants.DataProviderServiceConfiguration);
        }

        #endregion

        #region Public methods

        public CountryConfiguration GetProviderConfigurationByCountry(string countryCode)
        {
            return GetCountryConfiguration(countryCode) ??
                   GetCountryConfiguration(DefaultCountry);
        }

        /// <summary>
        /// Global application configuration provider instance
        /// </summary>
        public IConfigurationProvider ConfigurationProvider { get; private set; }

        #endregion

        #region Private methods

        private CountryConfiguration GetCountryConfiguration(string countryCode)
        {
            return (from country in _configurationProvider.CountryConfigurations
                    where country.CountryCode.Equals(countryCode, StringComparison.OrdinalIgnoreCase)
                    select country).FirstOrDefault();
        }

        #endregion
    }
}
