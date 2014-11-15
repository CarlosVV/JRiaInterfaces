using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces
{
    public interface IResponseParser
    {
        /// <summary>
        /// Parses autocomplete data response
        /// </summary>
        /// <param name="dataResponse">Data response instance</param>
        /// <param name="maxRecords">Number of records to return</param>
        /// <param name="country">Country code</param>
        /// <returns></returns>
        AutocompleteAddressResponseModel Parse(DataResponse dataResponse, int maxRecords, string country = null);

        /// <summary>
        /// Parses Address Verification data response
        /// </summary>
        /// <param name="dataResponse">Data response instance</param>
        /// <param name="acceptableConfidence">Acceptable level of confidence</param>
        /// <param name="includeLocation">Defines whether location information should be populated</param>
        /// <returns></returns>
        ValidateAddressResponseModel Parse(DataResponse dataResponse, LevelOfConfidence acceptableConfidence,
            bool includeLocation);

        /// <summary>
        /// Parses geo coding data response
        /// </summary>
        /// <param name="dataResponse">Data response instance</param>
        /// <param name="acceptableConfidence">Acceptable level of confidence</param>
        /// <returns></returns>
        GeocodeAddressResponseModel Parse(DataResponse dataResponse, LevelOfConfidence acceptableConfidence);

        /// <summary>
        /// Parses map api response
        /// </summary>
        /// <param name="dataResponse">Data response containing map image</param>
        /// <returns></returns>
        GetMapResponseModel Parse(BinaryDataResponse dataResponse);
    }
}