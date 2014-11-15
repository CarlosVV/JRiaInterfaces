﻿using System;
using System.Linq;
using System.Xml.Linq;
using CES.CoreApi.Common.Tools;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.Foundation.Contract.Exceptions;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Constants;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Parsers
{
    public class BingResponseParser: BaseDataResponseParser, IResponseParser
    {
        #region Core

        private readonly IBingAddressParser _addressParser;
        private readonly XNamespace _responseNamespace;

        public BingResponseParser(IBingAddressParser addressParser)
            : base(DataProviderType.Bing)
        {
            if (addressParser == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "addressParser");
            _addressParser = addressParser;
            _responseNamespace = "http://schemas.microsoft.com/search/local/ws/rest/v1";
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Parses autocomplete data response
        /// </summary>
        /// <param name="dataResponse">Data response instance</param>
        /// <param name="maxRecords">Number of records to return</param>
        /// <param name="country">Country code</param>
        /// <returns></returns>
        public AutocompleteAddressResponseModel Parse(DataResponse dataResponse, int maxRecords, string country = null)
        {
            var resourceSets = GetResourceSets(dataResponse);

            return resourceSets == null
                ? GetInvalidAddressAutocompleteResponse()
                : GetAddressAutocompleteResponse(resourceSets);
        }

        /// <summary>
        /// Parses Address Verification data response
        /// </summary>
        /// <param name="dataResponse">Data response instance</param>
        /// <param name="acceptableConfidence">Acceptable level of confidence</param>
        /// <param name="includeLocation">Defines whether location information should be populated</param>
        /// <returns></returns>
        public ValidateAddressResponseModel Parse(DataResponse dataResponse, LevelOfConfidence acceptableConfidence,
            bool includeLocation)
        {
            var resourceSets = GetResourceSets(dataResponse);

            return resourceSets == null
                ? GetInvalidAddressVerificationResponse()
                : GetAddressVerificationResponse(resourceSets, acceptableConfidence, includeLocation);
        }

        /// <summary>
        /// Parses geo coding data response
        /// </summary>
        /// <param name="dataResponse">Data response instance</param>
        /// <param name="acceptableConfidence">Acceptable level of confidence</param>
        /// <returns></returns>
        public GeocodeAddressResponseModel Parse(DataResponse dataResponse, LevelOfConfidence acceptableConfidence)
        {
            var resourceSets = GetResourceSets(dataResponse);

            return resourceSets == null
                ? GetInvalidGeocodeAddressResponse()
                : GetGeocodeAddressResponse(resourceSets, acceptableConfidence);
        }

        /// <summary>
        /// Parses map api response
        /// </summary>
        /// <param name="dataResponse">Data response containing map image</param>
        /// <returns></returns>
        public GetMapResponseModel Parse(BinaryDataResponse dataResponse)
        {
            return dataResponse == null || !dataResponse.IsSuccessStatusCode
                ? GetInvalidMapResponse()
                : GetValidMapResponse(dataResponse.BinaryResponse);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Gets ResourceSets element from response XML
        /// </summary>
        /// <param name="dataResponse">Service data response</param>
        /// <returns></returns>
        private XElement GetResourceSets(DataResponse dataResponse)
        {
            if (dataResponse == null || !dataResponse.IsSuccessStatusCode)
                return null;

            var responseDocument = XDocument.Parse(dataResponse.RawResponse);

            return responseDocument.Root != null
                ? responseDocument.Root.Element(_responseNamespace + BingConstants.ResourceSets)
                : null;
        }

        /// <summary>
        /// Gets address autcomplete response model
        /// </summary>
        /// <param name="resourceSets">ResourceSets element from response XML</param>
        /// <returns></returns>
        private AutocompleteAddressResponseModel GetAddressAutocompleteResponse(XContainer resourceSets)
        {
            var addressHints = (from resourceSet in resourceSets.Elements(_responseNamespace + BingConstants.ResourceSet)
                                let estimatedTotal = resourceSet.GetValue<int>(BingConstants.EstimatedTotal, _responseNamespace)
                                where estimatedTotal > 0
                                from resource in resourceSet.Elements(_responseNamespace + BingConstants.Resources)
                                from location in resource.Elements(_responseNamespace + BingConstants.Location)
                                let address = location.Element(_responseNamespace + BingConstants.Address)
                                select address)
                .ToList();

            //No hints returned
            if (!addressHints.Any())
                return GetInvalidAddressAutocompleteResponse();

            //Populate valid response model
            var responseModel = GetValidAddressAutocompleteResponse();
            responseModel.Addresses = (from hint in addressHints
                                       select _addressParser.ParseAddress(hint, _responseNamespace))
                .ToList();

            return responseModel;
        }

        /// <summary>
        /// Gets address verification model
        /// </summary>
        /// <param name="resourceSets">ResourceSets element from response XML</param>
        /// <param name="acceptableConfidence">Acceptable level of confidence</param>
        /// <param name="includeLocation">Defines whether location information should be populated</param>
        /// <returns></returns>
        private ValidateAddressResponseModel GetAddressVerificationResponse(XContainer resourceSets, LevelOfConfidence acceptableConfidence, bool includeLocation)
        {
            var matchRecord = GetMatchRecord(resourceSets, acceptableConfidence);

            if (matchRecord == null)
                return GetInvalidAddressVerificationResponse();

            var responseModel = GetValidAddressVerificationResponse(matchRecord.Item1);
            responseModel.Address = _addressParser.ParseAddress(matchRecord.Item3, _responseNamespace);
         
            if (!includeLocation) 
                return responseModel;

            //Populates location of verified address
            responseModel.Location = GetLocation(matchRecord.Item2);
            responseModel.GeocodingProvider = ProviderType;

            return responseModel;
        }
        
        /// <summary>
        /// Gets match record according request level of confidence or higher
        /// </summary>
        /// <param name="resourceSets">ResourceSets element from response XML</param>
        /// <param name="acceptableConfidence">Acceptable level of confidence</param>
        /// <returns></returns>
        private Tuple<LevelOfConfidence, XContainer, XElement> GetMatchRecord(XContainer resourceSets, LevelOfConfidence acceptableConfidence)
        {
            var acceptableConfidenceLevels = GetAcceptableConfidenceLevels(acceptableConfidence);

            var matchRecord = (from resourceSet in resourceSets.Elements(_responseNamespace + BingConstants.ResourceSet)
                               let estimatedTotal = resourceSet.GetValue<int>(BingConstants.EstimatedTotal, _responseNamespace)
                               where estimatedTotal > 0
                               from resource in resourceSet.Elements(_responseNamespace + BingConstants.Resources)
                               from location in resource.Elements(_responseNamespace + BingConstants.Location)
                               let confidence = location.GetValue<string>(BingConstants.Confidence, _responseNamespace).ConvertValue<LevelOfConfidence>()
                               where acceptableConfidenceLevels.Contains((int)confidence)
                               let address = location.Element(_responseNamespace + BingConstants.Address)
                               select new Tuple<LevelOfConfidence, XContainer, XElement>(confidence, location, address))
                .FirstOrDefault();
            return matchRecord;
        }

        /// <summary>
        /// Get Location instance
        /// </summary>
        /// <param name="locationRecord">Match record instance</param>
        private LocationModel GetLocation(XContainer locationRecord)
        {
            var pointElement = locationRecord.Element(_responseNamespace + BingConstants.Point);
            if (pointElement == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                   SubSystemError.GeolocationLocationIsNotFoundInResponse, DataProviderType.Bing);

            return new LocationModel
            {
                Latitude = pointElement.GetValue<decimal>(BingConstants.Latitude, _responseNamespace),
                Longitude = pointElement.GetValue<decimal>(BingConstants.Longitude, _responseNamespace)
            };
        }

        /// <summary>
        /// Parses XML response document and populates response model by the data if match record found
        /// </summary>
        /// <param name="resourceSets"></param>
        /// <param name="acceptableConfidence">Acceptable level of confidence</param>
        /// <returns></returns>
        private GeocodeAddressResponseModel GetGeocodeAddressResponse(XContainer resourceSets, LevelOfConfidence acceptableConfidence)
        {
            var matchRecord = GetMatchRecord(resourceSets, acceptableConfidence);

            if (matchRecord == null || matchRecord.Item2 == null)
                return GetInvalidGeocodeAddressResponse();

            var responseModel = GetValidGeocodeAddressResponse(matchRecord.Item1);
            responseModel.Address = _addressParser.ParseAddress(matchRecord.Item3, _responseNamespace);
            responseModel.Location = GetLocation(matchRecord.Item2);

            return responseModel;
        }

        #endregion
    }
}
