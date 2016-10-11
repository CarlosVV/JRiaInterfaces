using System;
using System.Linq;
using CES.CoreApi.GeoLocation.Interfaces;
using CES.CoreApi.GeoLocation.Models;
using CES.CoreApi.GeoLocation.Enumerations;
using CES.CoreApi.GeoLocation.Configuration;
using CES.CoreApi.GeoLocation.ClientSettings;

namespace CES.CoreApi.GeoLocation.Logic.Processors
{
    public class GeocodeServiceRequestProcessor : BaseServiceRequestProcessor, IGeocodeServiceRequestProcessor
    {
        #region Core

        private readonly IGeocodeAddressDataProvider _geocodeAddressDataProvider;

        public GeocodeServiceRequestProcessor(
            IGeocodeAddressDataProvider geocodeAddressDataProvider)
           
        {
         
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

        private GeocodeAddressResponseModel Process(string country, Func<DataProvider, GeocodeAddressResponseModel> geocodeByProvider)
        {
            var numberOfProviders = GeoLocationConfigurationSection.Instance.NumberOfProvidersToProcessResult.Value;
			var providers = GetProviderConfigurationByCountry(country, DataProviderServiceType.Geocoding, numberOfProviders).ToList();

			//if (!providers.Any())
			//    throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
			//        SubSystemError.GeolocationDataProviderNotFound, DataProviderServiceType.Geocoding);

			if (!providers.Any())
				throw new Exception("Invalid app configaration or app is not authorized");

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
