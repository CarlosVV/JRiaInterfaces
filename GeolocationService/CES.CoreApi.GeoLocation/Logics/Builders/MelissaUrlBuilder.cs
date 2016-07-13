using System.Collections.Generic;
using System.Globalization;
using System.Web;
using CES.CoreApi.GeoLocation.Interfaces;
using CES.CoreApi.GeoLocation.Models;
using System;
using CES.CoreApi.GeoLocation.Configuration;

namespace CES.CoreApi.GeoLocation.Logic.Builders
{
    public class MelissaUrlBuilder : BaseUrlBuilder, IUrlBuilder
    {
        #region Core
		
		private readonly string key = GeoLocationConfigurationSection.Instance.MelissaData.Key;   
      
        #endregion

        #region Public methods

   //     /// <summary>
   //     /// Builds URL for reverse geocoding - provides address by point
   //     /// </summary>
   //     /// <param name="location">Geographic point to reverse geocode</param>
   //     /// <returns></returns>
   //     public string BuildUrl(LocationModel location)
   //     {
   //         //throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
   //         //   SubSystemError.GeolocationReverseGeocodingIsNotSupported,
   //         //   DataProviderType.MelissaData);
			//throw new U
   //     }

        /// <summary>
        /// Builds URL for address autocomplete
        /// </summary>
        /// <param name="address">Address instance</param>
        /// <param name="maxRecords">Number of records to return</param>
        /// <returns></returns>
        public string BuildUrl(AutocompleteAddressModel address, int maxRecords)
        {
            var url = string.Format(CultureInfo.InvariantCulture,
				"{0}?id={1}&format=xml&address1={2}&administrativearea={3}&Country={4}&maxrecords={5}",
				GeoLocationConfigurationSection.Instance.MelissaData.AutoCompleteUrl,
                key,
                HttpUtility.UrlEncode(address.Address1),
                HttpUtility.UrlEncode(address.AdministrativeArea),
                HttpUtility.UrlEncode(address.Country),
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
				"{0}?format=xml&id={1}&a1={2}&a2={3}&loc={4}&admarea{5}&postal={6}&ctry={7}",
				GeoLocationConfigurationSection.Instance.MelissaData.Url,
                key,
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
			   "{0}?format=xml&id={1}&a1={2}&maxrecords=1",
			   GeoLocationConfigurationSection.Instance.MelissaData.Url,
               key,
               HttpUtility.UrlEncode(address));
            return url;
        }

		public string BuildUrl(LocationModel location)
		{
			throw new NotImplementedException();
		}

		public string BuildUrl(LocationModel center, MapSizeModel size, MapOutputParametersModel outputParameters, ICollection<PushPinModel> pushPins)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Builds URL for getting map
		/// </summary>
		/// <param name="center">Map center point</param>
		/// <param name="size">Map size</param>
		/// <param name="outputParameters">Parameters defining how to display map</param>
		/// <param name="pushPins">Collection of pins to display on map</param>
		/// <returns></returns>
		//public string BuildUrl(LocationModel center, MapSizeModel size, MapOutputParametersModel outputParameters, ICollection<PushPinModel> pushPins)
		//{
		//    throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
		//       SubSystemError.GeolocationMappingIsNotSupported,
		//       DataProviderType.MelissaData);
		//}

		#endregion
	}
}
