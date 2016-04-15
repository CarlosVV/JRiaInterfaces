using System.Collections.Generic;
using System.Globalization;
using System.Web;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;


namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Builders
{
    public class BingUrlBuilder: BaseUrlBuilder, IUrlBuilder
    {
        #region Core

        private readonly IAddressQueryBuilder _addressQueryBuilder;
        private readonly IBingPushPinParameterProvider _pushPinParameterProvider;
        private readonly ICorrectImageSizeProvider _imageSizeProvider;   
		private readonly  string key = Configuration.Provider.GeoLocationConfigurationSection.Instance.Bing.Key;

        public BingUrlBuilder(IAddressQueryBuilder addressQueryBuilder,
            IBingPushPinParameterProvider pushPinParameterProvider, ICorrectImageSizeProvider imageSizeProvider)
         
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

        /// <summary>
        /// Builds URL for reverse geocdoing - provides address by point
        /// </summary>
        /// <param name="location">Geographic point to reverse geocode</param>
        /// <returns></returns>
        public string BuildUrl(LocationModel location)
        {
            var url = string.Format(CultureInfo.InvariantCulture,
				"{0}/Locations/{1},{2}?o=xml&include=ciso2&userIp=127.0.0.1&maxResults=1&key={3}",
				Configuration.Provider.GeoLocationConfigurationSection.Instance.Bing.Url,
                location.Latitude,
                location.Longitude,
				key);
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
				"{0}/Locations?countryRegion={1}&adminDistrict={2}&locality={3}&postalCode={4}&addressLine={5}&o=xml&include=ciso2&userIp=127.0.0.1&maxResults={6}&key={7}",
				Configuration.Provider.GeoLocationConfigurationSection.Instance.Bing.Url,
                HttpUtility.UrlEncode(address.Country),
                HttpUtility.UrlEncode(address.AdministrativeArea),
                HttpUtility.UrlEncode(address.City),
                HttpUtility.UrlEncode(address.PostalCode),
                HttpUtility.UrlEncode(address.Address1),
                maxRecords,
				key);
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
				"{0}/Locations?CountryRegion={1}&adminDistrict={2}&locality={3}&postalCode={4}&addressLine={5}&o=xml&include=ciso2&userIp=127.0.0.1&maxResults=1&key={6}",
				Configuration.Provider.GeoLocationConfigurationSection.Instance.Bing.Url,
                HttpUtility.UrlEncode(address.Country),
                HttpUtility.UrlEncode(address.AdministrativeArea),
                HttpUtility.UrlEncode(address.City),
                HttpUtility.UrlEncode(address.PostalCode),
                HttpUtility.UrlEncode(_addressQueryBuilder.Build(address.Address1, address.Address2)),
				key);
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
			   "{0}/Locations?q={1}&o=xml&include=ciso2&userIp=127.0.0.1&maxResults=1&key={2}",
			   Configuration.Provider.GeoLocationConfigurationSection.Instance.Bing.Url,
               HttpUtility.UrlEncode(address),
				key);
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
				"/{0}/Imagery/Map/{1}/{2},{3}/{4}?mapSize={5},{6}&format={7}{8}&key={9}",
				Configuration.Provider.GeoLocationConfigurationSection.Instance.Bing.Url,
                outputParameters.MapStyle,
                center.Latitude,
                center.Longitude,
                outputParameters.ZoomLevel,
                _imageSizeProvider.GetCorrectImageSize(DataProviderType.Bing, ImageDimension.Width, size.Width),
                _imageSizeProvider.GetCorrectImageSize(DataProviderType.Bing, ImageDimension.Height, size.Height),
                outputParameters.ImageFormat,
                _pushPinParameterProvider.GetPushPinParameter(pushPins),
				key);
            return url;
        }
    }
}
