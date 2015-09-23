using System.Collections.Generic;
using System.Globalization;
using System.Web;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Constants;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Builders
{
    public class BingUrlBuilder: BaseUrlBuilder, IUrlBuilder
    {
        #region Core

        private readonly IAddressQueryBuilder _addressQueryBuilder;
        private readonly IBingPushPinParameterProvider _pushPinParameterProvider;
        private readonly ICorrectImageSizeProvider _imageSizeProvider;
        private readonly string _addressAutocompleteUrlTemplate;
        private readonly string _formattedAddressGeocodeAndVerificationUrlTemplate;
        private readonly string _addressGeocodeAndVerificationUrlTemplate;
        private readonly string _reverseGeocodePointUrlTemplate;
        private readonly string _mappingUrlTemplate;

        public BingUrlBuilder(IConfigurationProvider configurationProvider, IAddressQueryBuilder addressQueryBuilder,
            IBingPushPinParameterProvider pushPinParameterProvider, ICorrectImageSizeProvider imageSizeProvider)
            : base(configurationProvider, ConfigurationConstants.BingLicenseKeyConfigurationName, DataProviderType.Bing)
        {
            if (addressQueryBuilder == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "addressQueryBuilder");
            if (pushPinParameterProvider == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "pushPinParameterProvider");
            if (imageSizeProvider == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "imageSizeProvider");

            _addressQueryBuilder = addressQueryBuilder;
            _pushPinParameterProvider = pushPinParameterProvider;
            _imageSizeProvider = imageSizeProvider;
            _addressAutocompleteUrlTemplate = configurationProvider.Read<string>(ConfigurationConstants.BingAddressAutocompleteUrlTemplate);
            _addressGeocodeAndVerificationUrlTemplate = ConfigurationProvider.Read<string>(ConfigurationConstants.BingAddressGeocodeAndVerificationUrlTemplate);
            _formattedAddressGeocodeAndVerificationUrlTemplate = ConfigurationProvider.Read<string>(ConfigurationConstants.BingFormattedAddressGeocodeAndVerificationUrlTemplate);
            _reverseGeocodePointUrlTemplate = configurationProvider.Read<string>(ConfigurationConstants.BingReverseGeocodePointUrlTemplate);
            _mappingUrlTemplate = configurationProvider.Read<string>(ConfigurationConstants.BingMappingUrlTemplate);

            if (string.IsNullOrEmpty(_addressAutocompleteUrlTemplate))
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeolocationUrlTemplateNotFound,
                    DataProviderType.Bing, ConfigurationConstants.BingAddressAutocompleteUrlTemplate);

            if (string.IsNullOrEmpty(_formattedAddressGeocodeAndVerificationUrlTemplate))
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeolocationUrlTemplateNotFound,
                    DataProviderType.Bing, ConfigurationConstants.BingFormattedAddressGeocodeAndVerificationUrlTemplate);

            if (string.IsNullOrEmpty(_addressGeocodeAndVerificationUrlTemplate))
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeolocationUrlTemplateNotFound,
                    DataProviderType.Bing, ConfigurationConstants.BingAddressGeocodeAndVerificationUrlTemplate);

            if (string.IsNullOrEmpty(_reverseGeocodePointUrlTemplate))
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeolocationUrlTemplateNotFound,
                    DataProviderType.Bing, ConfigurationConstants.BingReverseGeocodePointUrlTemplate);

            if (string.IsNullOrEmpty(_mappingUrlTemplate))
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeolocationUrlTemplateNotFound,
                    DataProviderType.Bing, ConfigurationConstants.BingMappingUrlTemplate);
        }

        #endregion

        /// <summary>
        /// Builds URL for reverse geocdoing - provides address by point
        /// </summary>
        /// <param name="location">Geographic point to reverse geocode</param>
        /// <returns></returns>
        public string BuildUrl(LocationModel location)
        {
            var url = string.Format(CultureInfo.InvariantCulture,
                _reverseGeocodePointUrlTemplate,
                location.Latitude,
                location.Longitude,
                LicenseKey);
            return url;
        }

        /// <summary>
        /// Builds URL for address autocomplete
        /// </summary>
        /// <param name="address">Address instance</param>
        /// <param name="maxRecords">Number of records to return</param>
        /// <returns></returns>
        public string BuildUrl(AutocompleteAddressModel address, int maxRecords)
        {
            var url = string.Format(CultureInfo.InvariantCulture,
                _addressAutocompleteUrlTemplate,
                HttpUtility.UrlEncode(address.Country),
                HttpUtility.UrlEncode(address.AdministrativeArea),
                HttpUtility.UrlEncode(address.City),
                HttpUtility.UrlEncode(address.PostalCode),
                HttpUtility.UrlEncode(address.Address1),
                maxRecords,
                LicenseKey);
            return url;
        }

        /// <summary>
        /// Builds URL for address geocoding and verification
        /// </summary>
        /// <param name="address">Address instance to geocode or verify</param>
        /// <returns></returns>
        public string BuildUrl(AddressModel address)
        {
            var url = string.Format(CultureInfo.InvariantCulture,
                _addressGeocodeAndVerificationUrlTemplate,
                HttpUtility.UrlEncode(address.Country),
                HttpUtility.UrlEncode(address.AdministrativeArea),
                HttpUtility.UrlEncode(address.City),
                HttpUtility.UrlEncode(address.PostalCode),
                HttpUtility.UrlEncode(_addressQueryBuilder.Build(address.Address1, address.Address2)),
                LicenseKey);
            return url;
        }

        /// <summary>
        /// Builds URL for free formatted address geocoding or verification
        /// </summary>
        /// <param name="address">Free formatted address string</param>
        /// <returns></returns>
        public string BuildUrl(string address)
        {
            var url = string.Format(CultureInfo.InvariantCulture,
               _formattedAddressGeocodeAndVerificationUrlTemplate,
               HttpUtility.UrlEncode(address),
                LicenseKey);
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
            var url = string.Format(CultureInfo.InvariantCulture,
                _mappingUrlTemplate,
                outputParameters.MapStyle,
                center.Latitude,
                center.Longitude,
                outputParameters.ZoomLevel,
                _imageSizeProvider.GetCorrectImageSize(DataProviderType.Bing, ImageDimension.Width, size.Width),
                _imageSizeProvider.GetCorrectImageSize(DataProviderType.Bing, ImageDimension.Height, size.Height),
                outputParameters.ImageFormat,
                _pushPinParameterProvider.GetPushPinParameter(pushPins),
                LicenseKey);
            return url;
        }
    }
}
