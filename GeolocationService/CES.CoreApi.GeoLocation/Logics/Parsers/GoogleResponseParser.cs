using System;
using System.Linq;
using System.Xml.Linq;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Tools;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Constants;
using CES.CoreApi.GeoLocation.Enumerations;
using CES.CoreApi.GeoLocation.Models;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Parsers
{
    public class GoogleResponseParser: BaseDataResponseParser, IResponseParser
    {
        #region Core

        private readonly IGoogleAddressParser _addressParser;
        private readonly IGoogleLevelOfConfidenceProvider _levelOfConfidenceProvider;

        public GoogleResponseParser(IGoogleAddressParser addressParser,
            IGoogleLevelOfConfidenceProvider levelOfConfidenceProvider)
            : base(DataProviderType.Google)
        {
            if (addressParser == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "addressParser");
            if (levelOfConfidenceProvider == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "levelOfConfidenceProvider");
            _addressParser = addressParser;
            _levelOfConfidenceProvider = levelOfConfidenceProvider;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Parses autocomplete data response
        /// </summary>
        /// <param name="dataResponse">Data response instance</param>
        /// <param name="maxRecords">Number of records to return</param>
        /// <param name="confidence"></param>
        /// <param name="country">Country code</param>
        /// <returns></returns>
        public AutocompleteAddressResponseModel ParseAutocompleteAddressResponse(DataResponse dataResponse, int maxRecords, LevelOfConfidence confidence, string country = null)
        {
            var rootElement = GetResponseDocument(dataResponse);
            
            return rootElement == null 
                ? GetInvalidAddressAutocompleteResponse("")
                : GetAddressAutocompleteResponse(rootElement, maxRecords, confidence);
        }

        /// <summary>
        /// Parses Address Verification data response
        /// </summary>
        /// <param name="dataResponse">Data response instance</param>
        /// <param name="acceptableConfidence">Acceptable level of confidence</param>
        /// <returns></returns>
        public ValidateAddressResponseModel ParseValidateAddressResponse(DataResponse dataResponse, LevelOfConfidence acceptableConfidence)
        {
            var rootElement = GetResponseDocument(dataResponse);

            return rootElement == null
                ? GetInvalidAddressVerificationResponse("")
                : GetAddressVerificationResponse(rootElement, acceptableConfidence);
        }

        /// <summary>
        /// Parses geo coding data response
        /// </summary>
        /// <param name="dataResponse">Data response instance</param>
        /// <param name="acceptableConfidence">Acceptable level of confidence</param>
        /// <returns></returns>
        public GeocodeAddressResponseModel ParseGeocodeAddressResponse(DataResponse dataResponse, LevelOfConfidence acceptableConfidence)
        {
            var rootElement = GetResponseDocument(dataResponse);

            return rootElement == null
                ? GetInvalidGeocodeAddressResponse()
                : GetGeocodeAddressResponse(rootElement, acceptableConfidence);
        }

        /// <summary>
        /// Parses map api response
        /// </summary>
        /// <param name="dataResponse">Data response containing map image</param>
        /// <returns></returns>
        public GetMapResponseModel ParseMapResponse(BinaryDataResponse dataResponse)
        {
            return dataResponse == null || !dataResponse.IsSuccessStatusCode
               ? GetInvalidMapResponse()
               : GetValidMapResponse(dataResponse.BinaryResponse);
        }

        #endregion

        #region private methods

        /// <summary>
        /// Gets response data root XML element
        /// </summary>
        /// <param name="dataResponse">Service data response</param>
        /// <returns></returns>
        private static XElement GetResponseDocument(DataResponse dataResponse)
        {
            if (dataResponse == null || !dataResponse.IsSuccessStatusCode)
                return null;

            var responseDocument = XDocument.Parse(dataResponse.RawResponse);
            return responseDocument.Root;
        }

        /// <summary>
        /// Gets address autcomplete response model
        /// </summary>
        /// <param name="rootElement">Response data root XML element</param>
        /// <param name="maxRecords">Number of records to return</param>
        /// <param name="acceptableConfidence">Acceptable level of confidence</param>
        /// <returns></returns>
        private AutocompleteAddressResponseModel GetAddressAutocompleteResponse(XContainer rootElement, int maxRecords, LevelOfConfidence acceptableConfidence)
        {
            var acceptableConfidenceLevels = GetAcceptableConfidenceLevels(acceptableConfidence);

            var addressHints = (from result in rootElement.Elements(GoogleConstants.Result)
                                where result.GetValue<string>(GoogleConstants.FormattedAddress).Length > 0
                                let geometry = result.Element(GoogleConstants.Geometry)
                                let location = geometry.Element(GoogleConstants.Location)
                                let confidence = _levelOfConfidenceProvider.GetLevelOfConfidence(geometry.GetValue<string>(GoogleConstants.LocationType))
                                where acceptableConfidenceLevels.Contains((int)confidence)
                                orderby (int)confidence descending 
                                select new Tuple<LevelOfConfidence, XElement, XElement>(confidence, result, location))
                .ToList();

            //No hints returned
            if (!addressHints.Any())
                return GetInvalidAddressAutocompleteResponse();

            //Populate valid response model
            var responseModel = GetValidAddressAutocompleteResponse();
            responseModel.Suggestions = (from hint in addressHints.Take(maxRecords)
                select new AutocompleteSuggestionModel
                {
                    Confidence = hint.Item1,
                    Address = _addressParser.ParseAddress(hint.Item2),
                    Location = GetLocation(hint.Item3)
                })
                .ToList();

            return responseModel;
        }

        /// <summary>n
        /// Gets address verification model
        /// </summary>
        /// <param name="rootElement">Response data root XML element</param>
        /// <param name="acceptableConfidence">Acceptable level of confidence</param>
        /// <returns></returns>
        private ValidateAddressResponseModel GetAddressVerificationResponse(XContainer rootElement, LevelOfConfidence acceptableConfidence)
        {
            var matchRecord = GetMatchRecord(rootElement, acceptableConfidence);

            if (matchRecord == null)
                return GetInvalidAddressVerificationResponse();

            var responseModel = GetValidAddressVerificationResponse(matchRecord.Item1);
            responseModel.Address = _addressParser.ParseAddress(matchRecord.Item3);

            //Populates location of verified address
            responseModel.Location = GetLocation(matchRecord.Item2);

            return responseModel;
        }

        /// <summary>
        /// Gets match record according request level of confidence or higher
        /// </summary>
        /// <param name="rootElement">Response data root XML element</param>
        /// <param name="acceptableConfidence">Acceptable level of confidence</param>
        /// <returns></returns>
        private Tuple<LevelOfConfidence, XElement, XElement> GetMatchRecord(XContainer rootElement, LevelOfConfidence acceptableConfidence)
        {
            var acceptableConfidenceLevels = GetAcceptableConfidenceLevels(acceptableConfidence);

            var matchRecord = (from result in rootElement.Elements(GoogleConstants.Result)
                               let formattedAddress = result.GetValue<string>(GoogleConstants.FormattedAddress)
                               where formattedAddress.Length > 0
                               let geometry = result.Element(GoogleConstants.Geometry)
                               where geometry != null
                               let confidence = _levelOfConfidenceProvider.GetLevelOfConfidence(geometry.GetValue<string>(GoogleConstants.LocationType))
                               let location = geometry.Element(GoogleConstants.Location)
                               where acceptableConfidenceLevels.Contains((int)confidence)
                               select new Tuple<LevelOfConfidence, XElement, XElement>(confidence, location, result))
                .FirstOrDefault();
            return matchRecord;
        }

        /// <summary>
        /// Populates Location element by values
        /// </summary>
        /// <param name="locationRecord"></param>
        private static LocationModel GetLocation(XElement locationRecord)
        {
            return new LocationModel
            {
                Latitude = locationRecord.GetValue<double>(GoogleConstants.Latitude),
                Longitude = locationRecord.GetValue<double>(GoogleConstants.Longitude)
            };
        }

        /// <summary>
        /// Parses XML response document and populates geocode address response model by the data if match record found
        /// </summary>
        /// <param name="rootElement">Response data root XML element</param>
        /// <param name="acceptableConfidence">Acceptable level of confidence</param>
        /// <returns></returns>
        private GeocodeAddressResponseModel GetGeocodeAddressResponse(XContainer rootElement, LevelOfConfidence acceptableConfidence)
        {
            var matchRecord = GetMatchRecord(rootElement, acceptableConfidence);

            if (matchRecord == null || matchRecord.Item2 == null)
                return GetInvalidGeocodeAddressResponse();

            var responseModel = GetValidGeocodeAddressResponse(matchRecord.Item1);
            responseModel.Address = _addressParser.ParseAddress(matchRecord.Item3);
            responseModel.Location = GetLocation(matchRecord.Item2);

            return responseModel;
        }

        #endregion
    }
}
