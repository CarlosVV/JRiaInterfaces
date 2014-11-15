using System.Collections.Generic;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces
{
    public interface IUrlBuilder
    {
        /// <summary>
        /// Builds URL for reverse geocdoing - provides address by point
        /// </summary>
        /// <param name="location">Geographic point to reverse geocode</param>
        /// <returns></returns>
        string BuildUrl(LocationModel location);

        /// <summary>
        /// Builds URL for address autocomplete
        /// </summary>
        /// <param name="address">Address string</param>
        /// <param name="administrativeArea">Administrative area</param>
        /// <param name="country">Country code</param>
        /// <param name="maxRecords">Number of records to return</param>
        /// <returns></returns>
        string BuildUrl(string address, string administrativeArea, string country, int maxRecords);

        /// <summary>
        /// Builds URL for address geocoding and verification
        /// </summary>
        /// <param name="address">Address instance to geocode or verify</param>
        /// <returns></returns>
        string BuildUrl(AddressModel address);

        /// <summary>
        /// Builds URL for free formatted address geocoding or verification
        /// </summary>
        /// <param name="address">Free formatted address string</param>
        /// <returns></returns>
        string BuildUrl(string address);

        /// <summary>
        /// Builds URL for getting map
        /// </summary>
        /// <param name="center">Map center point</param>
        /// <param name="size">Map size</param>
        /// <param name="outputParameters">Parameters defining how to display map</param>
        /// <param name="pushPins">Collection of pins to display on map</param>
        /// <returns></returns>
        string BuildUrl(LocationModel center, MapSizeModel size, MapOutputParametersModel outputParameters, 
            ICollection<PushPinModel> pushPins);
    }
}