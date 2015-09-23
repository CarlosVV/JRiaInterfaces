using System.Collections.Generic;
using System.Globalization;
using System.Web;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Tools;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Attributes;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Constants;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Builders
{
    public class GoogleUrlBuilder : BaseUrlBuilder, IUrlBuilder
    {
        #region Core

        private readonly IAddressQueryBuilder _addressQueryBuilder;
        private readonly IGooglePushPinParameterProvider _pushPinParameterProvider;
        private readonly ICorrectImageSizeProvider _imageSizeProvider;
        private readonly string _addressAutocompleteUrlTemplate;
        private readonly string _addressGeocodeAndVerificationUrlTemplate;
        private readonly string _reverseGeocodePointUrlTemplate;
        private readonly string _mappingUrlTemplate;
        private const string KeyTemplate = "&key={0}";

        public GoogleUrlBuilder(IConfigurationProvider configurationProvider, IAddressQueryBuilder addressQueryBuilder,
            IGooglePushPinParameterProvider pushPinParameterProvider, ICorrectImageSizeProvider imageSizeProvider)
            : base(configurationProvider, ConfigurationConstants.GoogleLicenseKeyConfigurationName, DataProviderType.Google)
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

            _addressAutocompleteUrlTemplate = ConfigurationProvider.Read<string>(ConfigurationConstants.GoogleAddressAutocompleteUrlTemplate);
            _addressGeocodeAndVerificationUrlTemplate = ConfigurationProvider.Read<string>(ConfigurationConstants.GoogleAddressGeocodeAndVerificationUrlTemplate);
            _reverseGeocodePointUrlTemplate = ConfigurationProvider.Read<string>(ConfigurationConstants.GoogleReverseGeocodePointUrlTemplate);
            _mappingUrlTemplate = configurationProvider.Read<string>(ConfigurationConstants.GoogleMappingUrlTemplate);
            
            if (string.IsNullOrEmpty(_addressAutocompleteUrlTemplate))
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeolocationUrlTemplateNotFound,
                    DataProviderType.Google, ConfigurationConstants.GoogleAddressAutocompleteUrlTemplate);

            if (string.IsNullOrEmpty(_addressGeocodeAndVerificationUrlTemplate))
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeolocationUrlTemplateNotFound,
                    DataProviderType.Google, ConfigurationConstants.GoogleAddressGeocodeAndVerificationUrlTemplate);

            if (string.IsNullOrEmpty(_reverseGeocodePointUrlTemplate))
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeolocationUrlTemplateNotFound,
                    DataProviderType.Google, ConfigurationConstants.GoogleReverseGeocodePointUrlTemplate);

            if (string.IsNullOrEmpty(_mappingUrlTemplate))
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeolocationUrlTemplateNotFound,
                    DataProviderType.Google, ConfigurationConstants.GoogleMappingUrlTemplate);
        }

        #endregion

        #region Public methods

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
                GetLicenseKeyParameter());
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
            var query = _addressQueryBuilder.Build(address.Address1, address.AdministrativeArea, address.Country);

            var url = string.Format(CultureInfo.InvariantCulture,
                _addressAutocompleteUrlTemplate,
                query,
                GetLicenseKeyParameter());

            return url;
        }

        /// <summary>
        /// Builds URL for address geocoding and verification
        /// </summary>
        /// <param name="address">Address instance to geocode or verify</param>
        /// <returns></returns>
        public string BuildUrl(AddressModel address)
        {
            var addressFormatted = string.Join(",",
                address.Address1,
                address.Address2,
                address.City,
                address.AdministrativeArea,
                address.PostalCode,
                address.Country);

            var url = string.Format(CultureInfo.InvariantCulture,
                _addressGeocodeAndVerificationUrlTemplate,
                HttpUtility.UrlEncode(addressFormatted),
                GetLicenseKeyParameter());
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
                _addressGeocodeAndVerificationUrlTemplate,
                HttpUtility.UrlEncode(address),
                GetLicenseKeyParameter());
            return url;
        }

        public string BuildUrl(LocationModel center, MapSizeModel size, MapOutputParametersModel outputParameters, ICollection<PushPinModel> pushPins)
        {
            var url = string.Format(CultureInfo.InvariantCulture,
                _mappingUrlTemplate,
                center.Latitude,
                center.Longitude,
                outputParameters.ZoomLevel,
                _imageSizeProvider.GetCorrectImageSize(DataProviderType.Google, ImageDimension.Width, size.Width),
                _imageSizeProvider.GetCorrectImageSize(DataProviderType.Google, ImageDimension.Height, size.Height),
                outputParameters.ImageFormat.GetAttributeValue<GoogleImageFormatAttribute, string>(x => x.ImageFormat),
                outputParameters.MapStyle.GetAttributeValue<GoogleMapTypeAttribute, string>(x => x.MapType),
                _pushPinParameterProvider.GetPushPinParameter(pushPins),
                GetLicenseKeyParameter());
            return url;
        }

        #endregion

        #region private methods

        private string GetLicenseKeyParameter()
        {
            return string.IsNullOrEmpty(LicenseKey)
                ? string.Empty
                : string.Format(CultureInfo.InvariantCulture, KeyTemplate, LicenseKey);
        }

        #endregion
    }
}
