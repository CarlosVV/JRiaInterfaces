using System;
using System.Collections.Generic;
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
    public class MelissaResponseParser : BaseDataResponseParser, IResponseParser
    {
        #region Core

        private readonly XNamespace _xNamespace;
        private readonly IMelissaAddressParser _addressParser;
        private readonly IMelissaLevelOfConfidenceProvider _levelOfConfidenceProvider;

        public MelissaResponseParser(IMelissaAddressParser addressParser,
            IMelissaLevelOfConfidenceProvider levelOfConfidenceProvider)
            : base(DataProviderType.MelissaData)
        {
            if (addressParser == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "addressParser");
            if (levelOfConfidenceProvider == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "levelOfConfidenceProvider");

            _addressParser = addressParser;
            _levelOfConfidenceProvider = levelOfConfidenceProvider;
            _xNamespace = "urn:mdGlobalAddress";
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
                ? GetInvalidAddressAutocompleteResponse()
                : GetAddressAutocompleteResponse(rootElement, country);
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
                ? GetInvalidAddressVerificationResponse()
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

        public GetMapResponseModel ParseMapResponse(BinaryDataResponse dataResponse)
        {
            throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                SubSystemError.GeolocationMappingIsNotSupported,
                DataProviderType.MelissaData);
        }

        #endregion

        #region Private methods

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
        /// <param name="country">Country code</param>
        /// <returns></returns>
        private AutocompleteAddressResponseModel GetAddressAutocompleteResponse(XContainer rootElement, string country)
        {
            var results = rootElement.Element(MelissaConstants.Results);
            if (results == null)
                return GetInvalidAddressAutocompleteResponse();

            var addressHints = (from hint in results.Elements(MelissaConstants.ResultGlobal)
                let address = hint.Element(MelissaConstants.Address)
                where address != null
                select address)
                .ToList();

            //No hints returned
            if (!addressHints.Any())
                return GetInvalidAddressAutocompleteResponse();

            //Populate valid response model
            var responseModel = GetValidAddressAutocompleteResponse();
            responseModel.Suggestions = (from hint in addressHints
                select new AutocompleteSuggestionModel
                {
                    Confidence = LevelOfConfidence.NotFound,
                    Address = _addressParser.ParseAddress(hint, country, isAutocompleteService: true)
                })
                .OrderByDescending(p => p.Confidence)
                .ToList();

            return responseModel;
        }

        /// <summary>
        /// Gets address verification model
        /// </summary>
        /// <param name="rootElement">Response data root XML element</param>
        /// <param name="acceptableConfidence">Acceptable level of confidence</param>
        /// <returns></returns>
        private ValidateAddressResponseModel GetAddressVerificationResponse(XContainer rootElement, LevelOfConfidence acceptableConfidence)
        {
            var records = rootElement.Elements(_xNamespace + MelissaConstants.Records).ToList();
            if (!records.Any())
                return GetInvalidAddressVerificationResponse();

            var matchRecord = GetMatchRecord(records, acceptableConfidence);

            if (matchRecord == null)
                return GetInvalidAddressVerificationResponse();

            var responseModel = GetValidAddressVerificationResponse(matchRecord.Item1);
            responseModel.Address = _addressParser.ParseAddress(matchRecord.Item2, _xNamespace);

            //Populates location of verified address
            responseModel.Location = GetLocation(matchRecord.Item2);

            return responseModel;
        }

        /// <summary>
        /// Gets match record according request level of confidence or higher
        /// </summary>
        /// <param name="records">Response data records</param>
        /// <param name="acceptableConfidence">Acceptable level of confidence</param>
        /// <returns></returns>
        private Tuple<LevelOfConfidence, XElement> GetMatchRecord(IEnumerable<XElement> records, LevelOfConfidence acceptableConfidence)
        {
            var acceptableConfidenceLevels = GetAcceptableConfidenceLevels(acceptableConfidence);

            var matchRecord =
                (from responseRecord in records.Elements(_xNamespace + MelissaConstants.ResponseRecord)
                    let confidence = _levelOfConfidenceProvider.GetLevelOfConfidence(
                        responseRecord.GetValue<string>(MelissaConstants.Results, _xNamespace))
                    where acceptableConfidenceLevels.Contains((int) confidence)
                    select new Tuple<LevelOfConfidence, XElement>(confidence, responseRecord))
                    .FirstOrDefault();

            return matchRecord;
        }

        /// <summary>
        /// Populates Location element by values
        /// </summary>
        /// <param name="matchRecord">Match record instance</param>
        private LocationModel GetLocation(XElement matchRecord)
        {
            return new LocationModel
            {
                Latitude = matchRecord.GetValue<double>(MelissaConstants.Latitude, _xNamespace),
                Longitude = matchRecord.GetValue<double>(MelissaConstants.Longitude, _xNamespace)
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
            var records = rootElement.Elements(_xNamespace + MelissaConstants.Records).ToList();
            if (!records.Any())
                return GetInvalidGeocodeAddressResponse();

            var matchRecord = GetMatchRecord(records, acceptableConfidence);

            if (matchRecord == null || matchRecord.Item2 == null)
                return GetInvalidGeocodeAddressResponse();

            var responseModel = GetValidGeocodeAddressResponse(matchRecord.Item1);
            responseModel.Address = _addressParser.ParseAddress(matchRecord.Item2, _xNamespace);
            responseModel.Location = GetLocation(matchRecord.Item2);

            return responseModel;
        }

        #endregion
    }
}
