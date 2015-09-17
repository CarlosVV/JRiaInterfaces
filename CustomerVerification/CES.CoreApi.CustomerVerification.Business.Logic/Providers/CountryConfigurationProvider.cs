using System;
using System.Linq;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.CustomerVerification.Business.Contract.Configuration;
using CES.CoreApi.CustomerVerification.Business.Contract.Interfaces;
using CES.CoreApi.CustomerVerification.Business.Logic.Constants;
using CES.CoreApi.Foundation.Contract.Interfaces;

namespace CES.CoreApi.CustomerVerification.Business.Logic.Providers
{
    public class CountryConfigurationProvider : ICountryConfigurationProvider
    {
        #region Core

        private readonly IdVerificationProviderServiceConfiguration _countryConfigurations;

        public CountryConfigurationProvider(IConfigurationProvider configurationProvider)
        {
            if (configurationProvider == null)
                throw new CoreApiException(TechnicalSubSystem.CustomerVerificationService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "configurationProvider");

            _countryConfigurations = configurationProvider
                .ReadFromJson<IdVerificationProviderServiceConfiguration>(ConfigurationConstants.IdVerificationProviderServiceConfiguration);
        }

        #endregion

        #region ICountryConfigurationProvider implementation

        public CountryConfiguration GetConfiguration(string countryCode)
        {
            var configuration = _countryConfigurations
                .CountryConfigurations
                .FirstOrDefault(p => p.Country.Equals(countryCode, StringComparison.OrdinalIgnoreCase));

            if (configuration == null)
                throw new CoreApiException(TechnicalSubSystem.CustomerVerificationService,
                    SubSystemError.CustomerIdVerificationProviderCountryConfigurationIsNotFound, countryCode);

            return configuration;
        } 

        #endregion
    }
}
