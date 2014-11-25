using System;
using System.Linq;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Foundation.Contract.Enumerations;
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
        private readonly IGeocodeServiceRequestProcessor _geocodeServiceRequestProcessor;

        public AddressServiceRequestProcessor(ICountryConfigurationProvider configurationProvider,
            IAddressVerificationDataProvider addressVerificationDataProvider, IAddressAutocompleteDataProvider addressAutocompleteDataProvider,
            IGeocodeServiceRequestProcessor geocodeServiceRequestProcessor)
            : base(configurationProvider)
        {
            if (addressVerificationDataProvider == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "addressVerificationDataProvider");
            if (addressAutocompleteDataProvider == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "addressAutocompleteDataProvider");
            if (geocodeServiceRequestProcessor == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "geocodeServiceRequestProcessor");

            _addressVerificationDataProvider = addressVerificationDataProvider;
            _addressAutocompleteDataProvider = addressAutocompleteDataProvider;
            _geocodeServiceRequestProcessor = geocodeServiceRequestProcessor;
        }

        #endregion

        #region Public methods

        public ValidateAddressResponseModel ValidateAddress(AddressModel address, LevelOfConfidence confidence)
        {
            return VerifyAddress(address.Country, providerConfiguration =>
            {
                //Detect if geo coordinates needs to be populated using current provider
                var includeLocation = IsGeocodingServiceSame(CountryConfiguration(address.Country), providerConfiguration);

                var response = _addressVerificationDataProvider.Verify(
                    address,
                    providerConfiguration.DataProviderType,
                    confidence,
                    includeLocation);

                if (includeLocation)
                    return response;

                //Geocode address by another provider
                GeocodeAddress(response, address, confidence);

                return response;
            });
        }

        public ValidateAddressResponseModel ValidateAddress(string formattedAddress, string country, LevelOfConfidence confidence)
        {
            return VerifyAddress(country, providerConfiguration =>
            {
                //Detect if geo coordinates needs to be populated using current provider
                var includeLocation = IsGeocodingServiceSame(CountryConfiguration(country), providerConfiguration);

                var response = _addressVerificationDataProvider.Verify(
                    formattedAddress,
                    providerConfiguration.DataProviderType,
                    confidence,
                    includeLocation);

                if (includeLocation)
                    return response;

                //Geocode address by another provider
                GeocodeAddress(response, formattedAddress, country, confidence);

                return response;
            });
        }

        public AutocompleteAddressResponseModel GetAutocompleteList(string country, string administrativeArea, string address, int maxRecords)
        {
            var defaultNumberOfHints = CountryConfigurationProvider.ConfigurationProvider.Read<int>(ConfigurationConstants.AddressAutompleteDefaultNumberOfHints);
            var maximumNumberOfHints = CountryConfigurationProvider.ConfigurationProvider.Read<int>(ConfigurationConstants.AddressAutompleteMaximumNumberOfHints);

            if (maxRecords <= 0 || maxRecords > maximumNumberOfHints)
                maxRecords = defaultNumberOfHints;

            var providerConfiguration = GetProviderConfigurationByCountry(country, DataProviderServiceType.AddressAutoComplete).FirstOrDefault();

            if (providerConfiguration == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeolocationDataProviderNotFound,
                    DataProviderServiceType.AddressAutoComplete);

            return _addressAutocompleteDataProvider.GetAddressHintList(country, administrativeArea, address, maxRecords, providerConfiguration.DataProviderType);
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

        /// <summary>
        /// Detectes if location should be populated using different data provider
        /// </summary>
        /// <param name="configuration">Country configuration instance</param>
        /// <param name="providerConfiguration">Address verification provider instance</param>
        /// <returns></returns>
        private static bool IsGeocodingServiceSame(CountryConfiguration configuration, DataProviderConfiguration providerConfiguration)
        {
            var geocodingProviders = configuration.DataProviders
                .Where(p => p.DataProviderServiceType == DataProviderServiceType.Geocoding)
                .OrderByDescending(p => p.Priority);

            var geocodingProvider = geocodingProviders.FirstOrDefault(p => p.Priority <= providerConfiguration.Priority &&
                                                                           p.DataProviderType == providerConfiguration.DataProviderType);

            return geocodingProvider != null;
        }

        /// <summary>
        /// Uses another data provider to geocode address
        /// </summary>
        /// <param name="response">Address verification response</param>
        /// <param name="address">Address requested to validate</param>
        /// <param name="confidence">Acceptable level of confidence</param>
        private void GeocodeAddress(ValidateAddressResponseModel response, AddressModel address, LevelOfConfidence confidence)
        {
            var geoCodeResponseModel = _geocodeServiceRequestProcessor.GeocodeAddress(address, confidence);
            HandleGeocodingResponse(response, geoCodeResponseModel);
        }

        /// <summary>
        /// Uses another data provider to geocode address
        /// </summary>
        /// <param name="response">Address verification response</param>
        /// <param name="formattedAddress">Formatted address requested to validate</param>
        /// <param name="country">Country of address location</param>
        /// <param name="confidence">Acceptable level of confidence</param>
        private void GeocodeAddress(ValidateAddressResponseModel response, string formattedAddress, string country, LevelOfConfidence confidence)
        {
            var geoCodeResponseModel = _geocodeServiceRequestProcessor.GeocodeAddress(formattedAddress, country, confidence);
            HandleGeocodingResponse(response, geoCodeResponseModel);
        }

        /// <summary>
        /// Handles geocoding service response and populate location geo coordinates in address verification response
        /// </summary>
        /// <param name="response">Address verification response</param>
        /// <param name="geoCodeResponseModel">Geocoding response</param>
        private static void HandleGeocodingResponse(ValidateAddressResponseModel response,
            GeocodeAddressResponseModel geoCodeResponseModel)
        {
            if (geoCodeResponseModel.IsValid)
            {
                response.Location = geoCodeResponseModel.Location;
                response.GeocodingProvider = geoCodeResponseModel.DataProvider;
            }
            else
            {
                response.GeocodingProvider = DataProviderType.Undefined;
            }
        }

        #endregion
    }
}
