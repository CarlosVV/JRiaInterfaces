using System;
using System.Linq;
using System.Xml.Linq;
//using CES.CoreApi.Common.Enumerations;
//using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Tools;
using CES.CoreApi.GeoLocation.Interfaces;
using CES.CoreApi.GeoLocation.Logic.Constants;
using CES.CoreApi.GeoLocation.Enumerations;
using CES.CoreApi.GeoLocation.Models;
using System.Collections.Generic;

namespace CES.CoreApi.GeoLocation.Logic.Parsers
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
			//var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Logics.Parsers.ProviderResponses.GoogleGeocode>(dataResponse.RawResponse);
			//ValidateAddressResponseModel model = new ValidateAddressResponseModel();
			//var addressModel = new AddressModel();
			//var addressComponent = new AddressComponent();
			//var location = new LocationModel();
			//model.ProviderMessage = result.status;
			//foreach (var item in result.results)
			//{
			//	if (item.geometry != null && item.geometry.location != null)
			//	{
			//		location.Latitude = item.geometry.location.lat;
			//		location.Longitude = item.geometry.location.lng;
			//	}

			//	addressComponent.FormattedAddress = item.formatted_address;
			//	model.ResultCodes = string.Join(",", item.types);		
			//	foreach (var address in item.address_components)
			//	{
			//		if (HasAddressComponent(address.types, GoogleConstants.StreetNumber))
			//		{
			//			addressComponent.StreetNumber = address.short_name;
			//		}
			//		if (HasAddressComponent(address.types, GoogleConstants.Street))
			//		{
			//			addressComponent.Street = address.short_name;
			//			addressComponent.StreetLongName = address.long_name;
			//		}
			//		if (HasAddressComponent(address.types, GoogleConstants.City))
			//		{
			//			addressComponent.Locality = address.short_name;
			//			addressComponent.LocalityLongName = address.long_name;
			//		}
			//		if (HasAddressComponent(address.types, GoogleConstants.AdministrativeArea))
			//		{
			//			addressComponent.AdministrativeArea = address.short_name;
			//			addressComponent.AdministrativeAreaLongName = address.long_name;
			//		}
			//		if (HasAddressComponent(address.types, GoogleConstants.Country))
			//		{
			//			addressComponent.Country = address.short_name;
			//			addressComponent.CountryName = address.long_name;
			//		}
			//		if (HasAddressComponent(address.types, GoogleConstants.PostalCode))
			//		{
			//			addressComponent.PostalCode = address.short_name;						
			//		}
			//	}
			//}
			//model.Location = location;
			//model.Address = addressModel;
			//model.AddressComponent = addressComponent;
			//model.DataProvider = DataProviderType.Google;
			//return model;

			return rootElement == null
				? GetInvalidAddressVerificationResponse("")
				: GetAddressVerificationResponse(rootElement, acceptableConfidence);


			return GetInvalidAddressVerificationResponse("");

		}

		private bool HasAddressComponent(List<string> items, string key)
		{
			var result = from q in items where q.Equals(key, StringComparison.OrdinalIgnoreCase) select q;
			return result.Count() > 0;
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
