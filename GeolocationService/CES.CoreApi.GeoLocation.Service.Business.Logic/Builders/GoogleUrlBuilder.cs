using System.Collections.Generic;
using System.Globalization;
using System.Web;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Tools;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Attributes;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;


namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Builders
{
    public class GoogleUrlBuilder  : BaseUrlBuilder, IUrlBuilder
	{
        #region Core

        private readonly IAddressQueryBuilder _addressQueryBuilder;
        private readonly IGooglePushPinParameterProvider _pushPinParameterProvider;
        private readonly ICorrectImageSizeProvider _imageSizeProvider;      
        private const string KeyTemplate = "&key={0}";

        public GoogleUrlBuilder(IAddressQueryBuilder addressQueryBuilder,
            IGooglePushPinParameterProvider pushPinParameterProvider, ICorrectImageSizeProvider imageSizeProvider)
            :base()
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
				"{0}/geocode/xml?address={1}{2}",
				Configuration.Provider.GeoLocationConfigurationSection.Instance.Google.Url,
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
				"{0}/geocode/xml?address={1}{2}",
				Configuration.Provider.GeoLocationConfigurationSection.Instance.Google.Url,
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
				"{0}/geocode/xml?address={1}{2}",
				Configuration.Provider.GeoLocationConfigurationSection.Instance.Google.Url,
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
				"{0}/geocode/xml?address={1}{2}",
				Configuration.Provider.GeoLocationConfigurationSection.Instance.Google.Url,
                HttpUtility.UrlEncode(address),
                GetLicenseKeyParameter());
            return url;
        }

        public string BuildUrl(LocationModel center, MapSizeModel size, MapOutputParametersModel outputParameters, ICollection<PushPinModel> pushPins)
        {
            var url = string.Format(CultureInfo.InvariantCulture,
				"{0}/staticmap?center={1},{2}&zoom={3}&size={4}x{5}&format={6}&maptype={7}{8}{9}",
				Configuration.Provider.GeoLocationConfigurationSection.Instance.Google.Url,
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
			var key = Configuration.Provider.GeoLocationConfigurationSection.Instance.Google.Key;
            return string.IsNullOrEmpty(key)
                ? string.Empty
                : string.Format(CultureInfo.InvariantCulture, KeyTemplate, key);
        }

        #endregion
    }
}
