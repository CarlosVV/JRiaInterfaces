using System.Collections.Generic;
using System.Globalization;
using System.Web;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.Foundation.Contract.Exceptions;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Constants;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Builders
{
    public class MelissaUrlBuilder : BaseUrlBuilder, IUrlBuilder
    {
        #region Core

        private readonly string _addressAutocompleteUrlTemplate;
        private readonly string _addressGeocodeAndVerificationUrlTemplate;
        private readonly string _formattedAddressGeocodeAndVerificationUrlTemplate;
            
        public MelissaUrlBuilder(IConfigurationProvider configurationProvider)
            : base(configurationProvider, ConfigurationConstants.MelissaDataLicenseKeyConfigurationName, DataProviderType.MelissaData)
        {
            _addressAutocompleteUrlTemplate = configurationProvider.Read<string>(ConfigurationConstants.MelissaDataAddressAutocompleteUrlTemplate);
            _addressGeocodeAndVerificationUrlTemplate = ConfigurationProvider.Read<string>(ConfigurationConstants.MelissaDataAddressGeocodeAndVerificationUrlTemplate);
            _formattedAddressGeocodeAndVerificationUrlTemplate = ConfigurationProvider.Read<string>(ConfigurationConstants.MelissaDataFormattedAddressGeocodeAndVerificationUrlTemplate);
            
            if (string.IsNullOrEmpty(_addressAutocompleteUrlTemplate))
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeolocationUrlTemplateNotFound,
                    DataProviderType.MelissaData, ConfigurationConstants.MelissaDataAddressAutocompleteUrlTemplate);

            if (string.IsNullOrEmpty(_addressGeocodeAndVerificationUrlTemplate))
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeolocationUrlTemplateNotFound,
                    DataProviderType.MelissaData, ConfigurationConstants.MelissaDataAddressGeocodeAndVerificationUrlTemplate);

            if (string.IsNullOrEmpty(_formattedAddressGeocodeAndVerificationUrlTemplate))
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeolocationUrlTemplateNotFound,
                    DataProviderType.MelissaData, ConfigurationConstants.MelissaDataFormattedAddressGeocodeAndVerificationUrlTemplate);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Builds URL for reverse geocoding - provides address by point
        /// </summary>
        /// <param name="location">Geographic point to reverse geocode</param>
        /// <returns></returns>
        public string BuildUrl(LocationModel location)
        {
            throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
               SubSystemError.GeolocationReverseGeocodingIsNotSupported,
               DataProviderType.MelissaData);
        }

        /// <summary>
        /// Builds URL for address autocomplete
        /// </summary>
        /// <param name="address">Address string</param>
        /// <param name="administrativeArea">Administrative area</param>
        /// <param name="country">Country code</param>
        /// <param name="maxRecords">Number of records to return</param>
        /// <returns></returns>
        public string BuildUrl(string address, string administrativeArea, string country, int maxRecords)
        {
            var url = string.Format(CultureInfo.InvariantCulture,
                _addressAutocompleteUrlTemplate,
                LicenseKey,
                HttpUtility.UrlEncode(address),
                HttpUtility.UrlEncode(administrativeArea),
                HttpUtility.UrlEncode(country),
                maxRecords);
            return url;
        }

        /// <summary>
        /// Builds Melissa Data address verification URL
        /// </summary>
        /// <param name="address">Address to verify</param>
        /// <returns></returns>
        public string BuildUrl(AddressModel address)
        {
            var url = string.Format(
                _addressGeocodeAndVerificationUrlTemplate,
                LicenseKey,
                HttpUtility.UrlEncode(address.Address1),
                HttpUtility.UrlEncode(address.Address2),
                HttpUtility.UrlEncode(address.City),
                HttpUtility.UrlEncode(address.AdministrativeArea),
                HttpUtility.UrlEncode(address.PostalCode),
                HttpUtility.UrlEncode(address.Country));

            return url;
        }

        /// <summary>
        /// Builds Melissa Data formatted address verification URL
        /// Raises exception iuf country is not US or Canada since MelissaData cannot handle free form of address for other countries
        /// </summary>
        /// <param name="address">Formatted address</param>
        /// <returns></returns>
        public string BuildUrl(string address)
        {
            var url = string.Format(CultureInfo.InvariantCulture,
               _formattedAddressGeocodeAndVerificationUrlTemplate,
               LicenseKey,
               HttpUtility.UrlEncode(address));
            return url;
        }

        /// <summary>
        /// Builds URL for getting map
        /// </summary>
        /// <param name="center">Map center point</param>
        /// <param name="size">Map size</param>
        /// <param name="outputParameters">Parameters defining how to display map</param>
        /// <param name="pushPins">Collection of pins to display on map</param>
        /// <returns></returns>
        public string BuildUrl(LocationModel center, MapSizeModel size, MapOutputParametersModel outputParameters, ICollection<PushPinModel> pushPins)
        {
            throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
               SubSystemError.GeolocationMappingIsNotSupported,
               DataProviderType.MelissaData);
        }

        #endregion
    }
}
