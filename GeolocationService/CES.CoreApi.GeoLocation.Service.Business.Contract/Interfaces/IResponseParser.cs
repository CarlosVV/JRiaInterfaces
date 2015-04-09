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
        /// <param name="confidence"></param>
        /// <param name="country">Country code</param>
        /// <returns></returns>
        AutocompleteAddressResponseModel ParseAutocompleteAddressResponse(DataResponse dataResponse, int maxRecords, LevelOfConfidence confidence, string country = null);

        /// <summary>
        /// Parses Address Verification data response
        /// </summary>
        /// <param name="dataResponse">Data response instance</param>
        /// <param name="acceptableConfidence">Acceptable level of confidence</param>
        /// <returns></returns>
        ValidateAddressResponseModel ParseValidateAddressResponse(DataResponse dataResponse, LevelOfConfidence acceptableConfidence);

        /// <summary>
        /// Parses geo coding data response
        /// </summary>
        /// <param name="dataResponse">Data response instance</param>
        /// <param name="acceptableConfidence">Acceptable level of confidence</param>
        /// <returns></returns>
        GeocodeAddressResponseModel ParseGeocodeAddressResponse(DataResponse dataResponse, LevelOfConfidence acceptableConfidence);

        /// <summary>
        /// Parses map api response
        /// </summary>
        /// <param name="dataResponse">Data response containing map image</param>
        /// <returns></returns>
        GetMapResponseModel ParseMapResponse(BinaryDataResponse dataResponse);
    }
}