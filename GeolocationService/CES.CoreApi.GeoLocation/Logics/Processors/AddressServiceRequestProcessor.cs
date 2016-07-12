using System;
using System.Linq;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;


using CES.CoreApi.GeoLocation.Models;
using CES.CoreApi.GeoLocation.Enumerations;
using CES.CoreApi.Configuration.Provider;
using CES.CoreApi.GeoLocation.Configuration;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Processors
{
    public class AddressServiceRequestProcessor : BaseServiceRequestProcessor, IAddressServiceRequestProcessor
    {
        #region Core

        private readonly IAddressVerificationDataProvider _addressVerificationDataProvider;
        private readonly IAddressAutocompleteDataProvider _addressAutocompleteDataProvider;

        public AddressServiceRequestProcessor(
            IAddressVerificationDataProvider addressVerificationDataProvider, IAddressAutocompleteDataProvider addressAutocompleteDataProvider)
            
        {
            //if (addressVerificationDataProvider == null)
            //    throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
            //       SubSystemError.GeneralRequiredParameterIsUndefined, "addressVerificationDataProvider");
            //if (addressAutocompleteDataProvider == null)
            //    throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
            //       SubSystemError.GeneralRequiredParameterIsUndefined, "addressAutocompleteDataProvider");

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

			if (maxRecords <= 0 || maxRecords > maximumNumberOfHints)
                maxRecords = defaultNumberOfHints;

            var providerConfiguration = GetProviderConfigurationByCountry(address.Country, DataProviderServiceType.AddressAutoComplete).FirstOrDefault();
			var id = Guid.NewGuid();
			//AuditRepository.SetAudit(new Foundation.Contract.Models.AuditLog
			//{
			//	AppId = 8000,
			//	AppInstanceId = 1,
			//	AppName = "GeoLocation",
			//	Context = string.Format("{0},{1}" ,address.Address1, address.Country),
			//	DumpType = 1,
			//	JsonContent = JsonConvert.SerializeObject(address),
			//	Queue = 1,
			//	ServiceId = 1,
			//	SoapContent = "",
			//	TransactionId = 1,
			//	Id = id
			//});

			//if (providerConfiguration == null)
   //             throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
   //                 SubSystemError.GeolocationDataProviderNotFound,
   //                 DataProviderServiceType.AddressAutoComplete);

         var r = _addressAutocompleteDataProvider
                .GetAddressHintList(address, maxRecords, providerConfiguration.DataProviderType, confidence);


			//AuditRepository.SetAudit(new Foundation.Contract.Models.AuditLog
			//{
			//	AppId = 8000,
			//	AppInstanceId = 1,
			//	AppName = "GeoLocation",
			//	Context = "Api Response",
			//	DumpType = 2,
			//	JsonContent = JsonConvert.SerializeObject(r),
			//	Queue = 1,
			//	ServiceId = 1,
			//	SoapContent = "",
			//	TransactionId = 1,
			//	Id = id
			//});
			return r;
		}

        #endregion 

        #region Private methods

        private ValidateAddressResponseModel VerifyAddress(string country, Func<DataProviderConfiguration, ValidateAddressResponseModel> verifyByProvider)
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
