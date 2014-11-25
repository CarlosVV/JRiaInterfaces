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
    public class GeocodeServiceRequestProcessor : BaseServiceRequestProcessor, IGeocodeServiceRequestProcessor
    {
        #region Core

        private readonly IGeocodeAddressDataProvider _geocodeAddressDataProvider;

        public GeocodeServiceRequestProcessor(ICountryConfigurationProvider configurationProvider,
            IGeocodeAddressDataProvider geocodeAddressDataProvider)
            : base(configurationProvider)
        {
            if (geocodeAddressDataProvider == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "geocodeAddressDataProvider");

            _geocodeAddressDataProvider = geocodeAddressDataProvider;
        }

        #endregion

        #region Public methods

        public GeocodeAddressResponseModel GeocodeAddress(AddressModel address, LevelOfConfidence confidence)
        {
            return Process(address.Country, 
                providerConfiguration => _geocodeAddressDataProvider
                    .Geocode(address, providerConfiguration.DataProviderType, confidence));
        }

        public GeocodeAddressResponseModel GeocodeAddress(string formattedAddress, string country, LevelOfConfidence confidence)
        {
            return Process(country, 
                providerConfiguration => _geocodeAddressDataProvider
                    .Geocode(formattedAddress, providerConfiguration.DataProviderType, confidence));
        }

        public GeocodeAddressResponseModel ReverseGeocodePoint(LocationModel location, string country, LevelOfConfidence confidence)
        {
            return Process(country, 
                providerConfiguration => _geocodeAddressDataProvider
                    .ReverseGeocode(location, providerConfiguration.DataProviderType, confidence));
        }

        #endregion

        #region Private methods

        private GeocodeAddressResponseModel Process(string country, Func<DataProviderConfiguration, GeocodeAddressResponseModel> geocodeByProvider)
        {
            var numberOfProviders = CountryConfigurationProvider.ConfigurationProvider.Read<int>(ConfigurationConstants.NumberOfProvidersToProcessResult);
            var providers = GetProviderConfigurationByCountry(country, DataProviderServiceType.Geocoding, numberOfProviders).ToList();

            if (!providers.Any())
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeolocationDataProviderNotFound, DataProviderServiceType.Geocoding);

            GeocodeAddressResponseModel responseModel = null;

            foreach (var providerConfiguration in providers)
            {
                responseModel = geocodeByProvider(providerConfiguration);

                if (responseModel.IsValid)
                    return responseModel;
            }

            return responseModel;
        }

        #endregion
    }
}
