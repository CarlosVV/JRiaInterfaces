using System;
using System.Linq;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Configuration;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Constants;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Processors
{
    public class AddressServiceRequestProcessor : BaseServiceRequestProcessor, IAddressServiceRequestProcessor
    {
        #region Core

        private readonly IAddressVerificationDataProvider _addressVerificationDataProvider;
        private readonly IAddressAutocompleteDataProvider _addressAutocompleteDataProvider;

        public AddressServiceRequestProcessor(ICountryConfigurationProvider configurationProvider,
            IAddressVerificationDataProvider addressVerificationDataProvider, IAddressAutocompleteDataProvider addressAutocompleteDataProvider)
            : base(configurationProvider)
        {
            if (addressVerificationDataProvider == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "addressVerificationDataProvider");
            if (addressAutocompleteDataProvider == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "addressAutocompleteDataProvider");

            _addressVerificationDataProvider = addressVerificationDataProvider;
            _addressAutocompleteDataProvider = addressAutocompleteDataProvider;
        }

        #endregion

        #region Public methods

        public ValidateAddressResponseModel ValidateAddress(AddressModel address, LevelOfConfidence confidence)
        {
            return VerifyAddress(address.Country, providerConfiguration =>
            {
                var response = _addressVerificationDataProvider.Verify(
                    address,
                    providerConfiguration.DataProviderType,
                    confidence);

                return response;
            });
        }

        public ValidateAddressResponseModel ValidateAddress(string formattedAddress, string country, LevelOfConfidence confidence)
        {
            return VerifyAddress(country, providerConfiguration =>
            {
                var response = _addressVerificationDataProvider.Verify(
                    formattedAddress,
                    providerConfiguration.DataProviderType,
                    confidence);

                return response;
            });
        }

        public AutocompleteAddressResponseModel GetAutocompleteList(AutocompleteAddressModel address, int maxRecords, LevelOfConfidence confidence)
        {
            var defaultNumberOfHints = CountryConfigurationProvider.ConfigurationProvider.Read<int>(ConfigurationConstants.AddressAutompleteDefaultNumberOfHints);
            var maximumNumberOfHints = CountryConfigurationProvider.ConfigurationProvider.Read<int>(ConfigurationConstants.AddressAutompleteMaximumNumberOfHints);

            if (maxRecords <= 0 || maxRecords > maximumNumberOfHints)
                maxRecords = defaultNumberOfHints;

            var providerConfiguration = GetProviderConfigurationByCountry(address.Country, DataProviderServiceType.AddressAutoComplete).FirstOrDefault();

            if (providerConfiguration == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeolocationDataProviderNotFound,
                    DataProviderServiceType.AddressAutoComplete);

            return _addressAutocompleteDataProvider
                .GetAddressHintList(address, maxRecords, providerConfiguration.DataProviderType, confidence);
        }

        #endregion 

        #region Private methods

        private ValidateAddressResponseModel VerifyAddress(string country, Func<DataProviderConfiguration, ValidateAddressResponseModel> verifyByProvider)
        {
            var numberOfProviders = CountryConfigurationProvider.ConfigurationProvider.Read<int>(ConfigurationConstants.NumberOfProvidersToProcessResult);
            var providers = GetProviderConfigurationByCountry(country, DataProviderServiceType.AddressVerification, numberOfProviders).ToList();

            if (!providers.Any())
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeolocationDataProviderNotFound,
                    DataProviderServiceType.AddressVerification);

            ValidateAddressResponseModel responseModel = null;

            foreach (var providerConfiguration in providers)
            {
                responseModel = verifyByProvider(providerConfiguration);

                if (responseModel.IsValid)
                    return responseModel;
            }

            return responseModel;
        }

        #endregion
    }
}
