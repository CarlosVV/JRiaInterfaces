using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
//using CES.CoreApi.Common.Enumerations;
//using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Tools;
using CES.CoreApi.GeoLocation.Interfaces;
using CES.CoreApi.GeoLocation.Logic.Constants;
using CES.CoreApi.GeoLocation.Enumerations;
using CES.CoreApi.GeoLocation.Models;

namespace CES.CoreApi.GeoLocation.Logic.Parsers
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
                ? GetInvalidAddressAutocompleteResponse("")
                : GetAddressAutocompleteResponse(rootElement, country);
        }

		class Result
		{
			public string Results { get; set; }
			public string FormattedAddress { get; set; }
			public string AddressLine1 { get; set; }
			public string AddressLine2 { get; set; }
			public string Locality { get; set; }
			public string AdministrativeArea { get; set; }
			public string PostalCode { get; set; }
			public string CountryName { get; set; }
			public string CountryISO3166_1_Alpha2 { get; set; }

			public string SubPremisesNumber { get; set; }
			public string Latitude { get; set; }
			public string Longitude { get; set; }

		}

		class M
		{
			public string TransmissionResults { get; set; }
			public List<Result> Records { get; set; }
		}


		/// <summary>
		/// Parses Address Verification data response
		/// </summary>
		/// <param name="dataResponse">Data response instance</param>
		/// <param name="acceptableConfidence">Acceptable level of confidence</param>
		/// <returns></returns>
		public ValidateAddressResponseModel ParseValidateAddressResponse(DataResponse dataResponse, LevelOfConfidence acceptableConfidence)
        {
			var x = Newtonsoft.Json.JsonConvert.DeserializeObject<M>(dataResponse.RawResponse);
			if (x != null && x.Records != null && x.Records.Count > 0)
			{
				var add = x.Records[0];
				var r = new ValidateAddressResponseModel
				{
					Address = new AddressModel
					{
						Address1 = add.AddressLine1,
						Address2 = add.AddressLine2,
						AdministrativeArea = add.AdministrativeArea,
						City = add.Locality,
						Country = add.CountryISO3166_1_Alpha2,
						FormattedAddress = add.FormattedAddress,
						PostalCode = add.PostalCode,
						UnitOrApartment = add.SubPremisesNumber

					},
					
					Location = new LocationModel
					{

						Latitude = GetSafeDouble(add.Latitude),
						Longitude = GetSafeDouble(add.Longitude)
					}

				};

				return r;
			}
			return GetInvalidAddressVerificationResponse("");
			//var rootElement = GetResponseDocument(dataResponse);

			//         return rootElement == null
			//             ? GetInvalidAddressVerificationResponse("")
			//             : GetAddressVerificationResponse(rootElement, acceptableConfidence);
		}

		private double GetSafeDouble(string value)
		{
			double id;
			double.TryParse(value, out id);
			return id;
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
            var results = rootElement.Descendants(MelissaConstants.Results);
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
	

            var records = rootElement.Descendants(_xNamespace + MelissaConstants.TotalRecords).ToList();
            if (records.Count <=0 || records[0].Value =="0")
                return GetInvalidAddressVerificationResponse();

            var matchRecord = GetMatchRecord(rootElement.Descendants(_xNamespace + MelissaConstants.Records), acceptableConfidence);

            if (matchRecord == null)
                return GetInvalidAddressVerificationResponse();

            var responseModel = GetValidAddressVerificationResponse(matchRecord.Item1);
            responseModel.Address = _addressParser.ParseAddress(matchRecord.Item2, _xNamespace);



			responseModel.ResultCodes = GetResultMessage(matchRecord.Item2);
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
          //  var acceptableConfidenceLevels = GetAcceptableConfidenceLevels(acceptableConfidence);

            var matchRecord =
                (from responseRecord in records.Elements(_xNamespace + MelissaConstants.ResponseRecord)
                    let confidence = _levelOfConfidenceProvider.GetLevelOfConfidence(
                        responseRecord.GetValue<string>(MelissaConstants.Results, _xNamespace))
                   // where acceptableConfidenceLevels.Contains((int) confidence)
                    select new Tuple<LevelOfConfidence, XElement>(confidence, responseRecord))
                    .FirstOrDefault();

            return matchRecord;
        }

		/// <summary>
		/// Populates Location element by values
		/// </summary>
		/// <param name="matchRecord">Match record instance</param>
		private string GetResultMessage(XElement matchRecord)
		{
			return matchRecord.GetValue<string>(MelissaConstants.Results, _xNamespace);
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

		public GetMapResponseModel ParseMapResponse(BinaryDataResponse dataResponse)
		{
			throw new NotImplementedException();
		}


		private List<MelissaDataReturnCode> GetMelissaDataReturnCodes()
		{
			var items = new List<MelissaDataReturnCode>();

			items.Add(new MelissaDataReturnCode { Code = "AV11", Description = "The address has been partially verified to the Administrative Area (State) Level, which is NOT the highest level possible with the reference data." });
			items.Add(new MelissaDataReturnCode { Code = "AV12", Description = "he address has been partially verified to the Locality (City) Level, which is NOT the highest level possible with the reference data." });
			items.Add(new MelissaDataReturnCode { Code = "AV13", Description = "The address has been partially verified to the Thoroughfare (Street) Level, which is NOT the highest level possible with the reference data." });
			items.Add(new MelissaDataReturnCode { Code = "AV14", Description = "The address has been partially verified to the Premise (House or Building) Level, which is NOT the highest level possible with the reference data." });
			items.Add(new MelissaDataReturnCode { Code = "AV15", Description = "The address has been partially verified to the SubPremises (Suite) or PO Box Level, which is NOT the highest level possible with the reference data." });


			items.Add(new MelissaDataReturnCode { Code = "AV21", Description = "The address has been verified to the Administrative Area (State) Level, which is the highest level possible with the reference da" });
			items.Add(new MelissaDataReturnCode { Code = "AV22", Description = "The address has been verified to the Locality (City) Level, which is the highest level possible with the reference data." });
			items.Add(new MelissaDataReturnCode { Code = "AV23", Description = "The address has been verified to the Thoroughfare (Street) Level, which is the highest level possible with the reference data." });
			items.Add(new MelissaDataReturnCode { Code = "AV24", Description = "The address has been verified to the Premise (House or Building) Level, which is the highest level possible with the reference data." });
			items.Add(new MelissaDataReturnCode { Code = "AV25", Description = "The address has been verified to the SubPremise (Suite) or PO Box Level, which is the highest level possible with the reference data." });

			

			return items;
		}
		#endregion
	}
}
