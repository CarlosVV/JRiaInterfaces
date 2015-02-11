using System.Xml.Linq;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Parsers;
using CES.CoreApi.GeoLocation.Service.Business.Logic.UnitTest.Helpers;
using CES.CoreApi.GeoLocation.Service.UnitTestTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.UnitTest
{
    [TestClass]
    public class MelissaResponseParserUnitTests
    {
        private Mock<IMelissaAddressParser> _addressParser;
        private Mock<IMelissaLevelOfConfidenceProvider> _levelOfConfidenceProvider;

        //Bing does not use these parameters
        private const int MaxRecords = 15;
        private const string Country = "US";

        [TestInitialize]
        public void Setup()
        {
            _addressParser = new Mock<IMelissaAddressParser>();
            _levelOfConfidenceProvider = new Mock<IMelissaLevelOfConfidenceProvider>();
        }

        [TestMethod]
        public void Constructor_addressParserIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(() => new MelissaResponseParser(null, _levelOfConfidenceProvider.Object),
                SubSystemError.GeneralRequiredParameterIsUndefined, "addressParser");
        }

        [TestMethod]
        public void Constructor_LevelOfConfidenceProviderIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(() => new MelissaResponseParser(_addressParser.Object, null),
                SubSystemError.GeneralRequiredParameterIsUndefined, "levelOfConfidenceProvider");
        }

        [TestMethod]
        public void Constructor_HappyPath_NoExceptionRaised()
        {
            ExceptionHelper.CheckHappyPath(
                () => new MelissaResponseParser(_addressParser.Object, _levelOfConfidenceProvider.Object));
        }

        #region AutocompleteAddressResponse tests

        [TestMethod]
        public void Parse_AutocompleteAddressResponseModelReturned_HappyPath()
        {
            var dataResponse = MelissaResponseParserHelper.GetAutomcompleteDataResponse();

            var suggestionModel = MelissaResponseParserHelper.GetAutocompleteSuggestionModel();
            _addressParser.Setup(p => p.ParseAddress(It.IsAny<XElement>(), It.IsAny<XNamespace>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(suggestionModel.Address);

            var result = new MelissaResponseParser(_addressParser.Object, _levelOfConfidenceProvider.Object).Parse(dataResponse, MaxRecords, Country);

            Assert.IsNotNull(result);
            Assert.AreEqual(suggestionModel.Address, result.Suggestions[0].Address);
            Assert.IsNull(result.Suggestions[0].Location);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(DataProviderType.MelissaData, result.DataProvider);
        }

        [TestMethod]
        public void Parse_DataResponseIsNull_InvalidAddressAutocompleteResponseReturned()
        {
            var result = new MelissaResponseParser(_addressParser.Object, _levelOfConfidenceProvider.Object).Parse(null, MaxRecords, Country);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(DataProviderType.MelissaData, result.DataProvider);
        }

        [TestMethod]
        public void Parse_DataResponseIsInvalid_InvalidAddressAutocompleteResponseReturned()
        {
            var dataResponse = MelissaResponseParserHelper.GetAutomcompleteDataResponse(true);
            var result = new MelissaResponseParser(_addressParser.Object, _levelOfConfidenceProvider.Object).Parse(dataResponse, MaxRecords, Country);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(DataProviderType.MelissaData, result.DataProvider);
        }

        [TestMethod]
        public void Parse_NoAddressInResponse_InvalidAddressAutocompleteResponseReturned()
        {
            var dataResponse = MelissaResponseParserHelper.GetAutomcompleteDataResponseNoAddress();
            var result = new MelissaResponseParser(_addressParser.Object, _levelOfConfidenceProvider.Object).Parse(dataResponse, MaxRecords, Country);

            Assert.IsNotNull(result);
            Assert.IsNull(result.Suggestions);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(DataProviderType.MelissaData, result.DataProvider);
        }

        [TestMethod]
        public void Parse_NoResultsInResponse_InvalidAddressAutocompleteResponseReturned()
        {
            var dataResponse = MelissaResponseParserHelper.GetAutomcompleteDataResponseNoResults();
            var result = new MelissaResponseParser(_addressParser.Object, _levelOfConfidenceProvider.Object).Parse(dataResponse, MaxRecords, Country);

            Assert.IsNotNull(result);
            Assert.IsNull(result.Suggestions);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(DataProviderType.MelissaData, result.DataProvider);
        }

        #endregion

        #region ValidateAddressResponseModel tests

        [TestMethod]
        public void Parse_ValidateAddressResponseModelReturned_HappyPath()
        {
            var dataResponse = MelissaResponseParserHelper.GetValidateAddressDataResponse();

            var addressModel = MelissaResponseParserHelper.GetAddressModel();
            _addressParser.Setup(p => p.ParseAddress(It.IsAny<XElement>(), It.IsAny<XNamespace>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(addressModel);
            _levelOfConfidenceProvider.Setup(p => p.GetLevelOfConfidence(It.IsAny<string>())).Returns(LevelOfConfidence.High);

            var locationModel = MelissaResponseParserHelper.GetLocationModel();

            var result = new MelissaResponseParser(_addressParser.Object, _levelOfConfidenceProvider.Object).Parse(dataResponse, LevelOfConfidence.High, true);

            Assert.IsNotNull(result);
            Assert.AreEqual(addressModel, result.Address);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(DataProviderType.MelissaData, result.AddressValidationProvider);
            Assert.AreEqual(DataProviderType.MelissaData, result.GeocodingProvider);
            Assert.AreEqual(LevelOfConfidence.High, result.Confidence);
            Assert.AreEqual(locationModel.Latitude, result.Location.Latitude);
            Assert.AreEqual(locationModel.Longitude, result.Location.Longitude);
        }

        [TestMethod]
        public void Parse_ValidateAddressResponseModelReturned_HappyPathWithoutLocation()
        {
            var dataResponse = MelissaResponseParserHelper.GetValidateAddressDataResponse();

            var addressModel = MelissaResponseParserHelper.GetAddressModel();
            _addressParser.Setup(p => p.ParseAddress(It.IsAny<XElement>(), It.IsAny<XNamespace>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(addressModel);
            _levelOfConfidenceProvider.Setup(p => p.GetLevelOfConfidence(It.IsAny<string>())).Returns(LevelOfConfidence.High);

            var result = new MelissaResponseParser(_addressParser.Object, _levelOfConfidenceProvider.Object).Parse(dataResponse, LevelOfConfidence.High, false);

            Assert.IsNotNull(result);
            Assert.AreEqual(addressModel, result.Address);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(DataProviderType.MelissaData, result.AddressValidationProvider);
            Assert.AreEqual(DataProviderType.Undefined, result.GeocodingProvider);
            Assert.AreEqual(LevelOfConfidence.High, result.Confidence);
            Assert.IsNull(result.Location);
        }

        [TestMethod]
        public void Parse_DataResponseIsNull_InvalidValidateAddressResponseReturned()
        {
            var result = new MelissaResponseParser(_addressParser.Object, _levelOfConfidenceProvider.Object).Parse(null, LevelOfConfidence.High, true);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(DataProviderType.MelissaData, result.AddressValidationProvider);
            Assert.AreEqual(DataProviderType.MelissaData, result.GeocodingProvider);
            Assert.IsNull(result.Location);
            Assert.IsNull(result.Address);
        }

        [TestMethod]
        public void Parse_DataResponseIsInvalid_InvalidValidateAddressResponseReturned()
        {
            var dataResponse = MelissaResponseParserHelper.GetValidateAddressDataResponse(true);
            var result = new MelissaResponseParser(_addressParser.Object, _levelOfConfidenceProvider.Object).Parse(dataResponse, LevelOfConfidence.High, true);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(DataProviderType.MelissaData, result.AddressValidationProvider);
            Assert.AreEqual(DataProviderType.MelissaData, result.GeocodingProvider);
            Assert.IsNull(result.Location);
            Assert.IsNull(result.Address);
        }

        [TestMethod]
        public void Parse_NoAddressInResponse_InvalidValidateAddressResponseReturned()
        {
            var dataResponse = MelissaResponseParserHelper.GetValidateAddressDataResponseNoAddress();
            var result = new MelissaResponseParser(_addressParser.Object, _levelOfConfidenceProvider.Object).Parse(dataResponse, LevelOfConfidence.High, true);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(DataProviderType.MelissaData, result.AddressValidationProvider);
            Assert.AreEqual(DataProviderType.MelissaData, result.GeocodingProvider);
            Assert.IsNull(result.Location);
            Assert.IsNull(result.Address);
        }

        [TestMethod]
        public void Parse_NoMatchRecordInResponse_ExceptionRaised()
        {
            var dataResponse = MelissaResponseParserHelper.GetValidateAddressDataResponse();

            var addressModel = MelissaResponseParserHelper.GetAddressModel();
            _addressParser.Setup(p => p.ParseAddress(It.IsAny<XElement>(), It.IsAny<XNamespace>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(addressModel);
            _levelOfConfidenceProvider.Setup(p => p.GetLevelOfConfidence(It.IsAny<string>())).Returns(LevelOfConfidence.Low);


            var result = new MelissaResponseParser(_addressParser.Object, _levelOfConfidenceProvider.Object).Parse(dataResponse, LevelOfConfidence.High, true);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(DataProviderType.MelissaData, result.AddressValidationProvider);
            Assert.AreEqual(DataProviderType.MelissaData, result.GeocodingProvider);
            Assert.IsNull(result.Location);
            Assert.IsNull(result.Address);
        }

        #endregion

        #region GeocodeAddressResponseModel tests

        [TestMethod]
        public void Parse_GeocodeAddressResponseModelReturned_HappyPath()
        {
            var dataResponse = MelissaResponseParserHelper.GetGeocodeAddressDataResponse();

            var addressModel = MelissaResponseParserHelper.GetAddressModel();
            _addressParser.Setup(p => p.ParseAddress(It.IsAny<XElement>(), It.IsAny<XNamespace>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(addressModel);
            _levelOfConfidenceProvider.Setup(p => p.GetLevelOfConfidence(It.IsAny<string>())).Returns(LevelOfConfidence.High);

            var locationModel = MelissaResponseParserHelper.GetLocationModel();

            var result = new MelissaResponseParser(_addressParser.Object, _levelOfConfidenceProvider.Object).Parse(dataResponse, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.AreEqual(addressModel, result.Address);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(DataProviderType.MelissaData, result.DataProvider);
            Assert.AreEqual(LevelOfConfidence.High, result.Confidence);
            Assert.AreEqual(locationModel.Latitude, result.Location.Latitude);
            Assert.AreEqual(locationModel.Longitude, result.Location.Longitude);
        }

        [TestMethod]
        public void Parse_DataResponseIsNull_InvalidGeocodeAddressResponseReturned()
        {
            var result = new MelissaResponseParser(_addressParser.Object, _levelOfConfidenceProvider.Object).Parse(null, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(DataProviderType.MelissaData, result.DataProvider);
            Assert.IsNull(result.Location);
            Assert.IsNull(result.Address);
        }

        [TestMethod]
        public void Parse_DataResponseIsInvalid_InvalidGeocodeAddressResponseReturned()
        {
            var dataResponse = MelissaResponseParserHelper.GetGeocodeAddressDataResponse(true);
            var result = new MelissaResponseParser(_addressParser.Object, _levelOfConfidenceProvider.Object).Parse(dataResponse, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(DataProviderType.MelissaData, result.DataProvider);
            Assert.IsNull(result.Location);
            Assert.IsNull(result.Address);
        }

        [TestMethod]
        public void Parse_NoAddressInResponse_InvalidGeocodeAddressResponseReturned()
        {
            var dataResponse = MelissaResponseParserHelper.GetGeocodeAddressDataResponseNoAddress();
            var result = new MelissaResponseParser(_addressParser.Object, _levelOfConfidenceProvider.Object).Parse(dataResponse, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(DataProviderType.MelissaData, result.DataProvider);
            Assert.IsNull(result.Location);
            Assert.IsNull(result.Address);
        }

        [TestMethod]
        public void Parse_NoMatchRecord_InvalidGeocodeAddressResponseReturned()
        {
            var dataResponse = MelissaResponseParserHelper.GetGeocodeAddressDataResponse();
            _levelOfConfidenceProvider.Setup(p => p.GetLevelOfConfidence(It.IsAny<string>())).Returns(LevelOfConfidence.Low);
            var result = new MelissaResponseParser(_addressParser.Object, _levelOfConfidenceProvider.Object).Parse(dataResponse, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(DataProviderType.MelissaData, result.DataProvider);
            Assert.IsNull(result.Location);
            Assert.IsNull(result.Address);
        }

        #endregion

        #region GetMapResponseModel tests

        [TestMethod]
        public void Parse_GetMapResponseModelReturned_HappyPath()
        {
            ExceptionHelper.CheckException(() => new MelissaResponseParser(_addressParser.Object, _levelOfConfidenceProvider.Object).Parse(It.IsAny<BinaryDataResponse>()),
                SubSystemError.GeolocationMappingIsNotSupported, DataProviderType.MelissaData);
        }
        
        #endregion
    }
}
