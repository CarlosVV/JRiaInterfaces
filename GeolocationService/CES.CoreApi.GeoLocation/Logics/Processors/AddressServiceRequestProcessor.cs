using System;
using System.Linq;
using CES.CoreApi.GeoLocation.Interfaces;
using CES.CoreApi.GeoLocation.Models;
using CES.CoreApi.GeoLocation.Enumerations;
using CES.CoreApi.GeoLocation.Configuration;
using CES.CoreApi.GeoLocation.ClientSettings;

namespace CES.CoreApi.GeoLocation.Logic.Processors
{
    public class AddressServiceRequestProcessor : BaseServiceRequestProcessor, IAddressServiceRequestProcessor
    {
        #region Core

        private readonly IAddressVerificationDataProvider _addressVerificationDataProvider;
        private readonly IAddressAutocompleteDataProvider _addressAutocompleteDataProvider;

        public AddressServiceRequestProcessor(
            IAddressVerificationDataProvider addressVerificationDataProvider, IAddressAutocompleteDataProvider addressAutocompleteDataProvider)
            
        {
         
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
            var defaultNumberOfHints = GeoLocationConfigurationSection.Instance.AutompleteDefaultNumberOfHints.Value;
			int maximumNumberOfHints = GeoLocationConfigurationSection.Instance.AutompleteMaximumNumberOfHints.Value;

			if (maxRecords <= 0)
                maxRecords = defaultNumberOfHints;
			else if(maxRecords > maximumNumberOfHints)
			{
				maxRecords = maximumNumberOfHints;
			}

            var providerConfiguration = GetProviderConfigurationByCountry(address.Country, DataProviderServiceType.AddressAutoComplete, maxRecords).FirstOrDefault();
	

			 var result = _addressAutocompleteDataProvider
					.GetAddressHintList(address, maxRecords, providerConfiguration.DataProviderType, confidence);


		
			return result;
		}

        #endregion 

        #region Private methods

        private ValidateAddressResponseModel VerifyAddress(string country, Func<DataProvider, ValidateAddressResponseModel> verifyByProvider)
        {
			var numberOfProviders = GeoLocationConfigurationSection.Instance.NumberOfProvidersToProcessResult.Value;


				//CountryConfigurationProvider.ConfigurationProvider.Read<int>(ConfigurationConstants.NumberOfProvidersToProcessResult);
            var providers = GetProviderConfigurationByCountry(country, DataProviderServiceType.AddressVerification, numberOfProviders).ToList();

            if (!providers.Any())
                throw new Exception("application is not setup or not authorized");

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
