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
    public class GoogleResponseParserUnitTests
    {
        private Mock<IGoogleAddressParser> _addressParser;
        private Mock<IGoogleLevelOfConfidenceProvider> _levelOfConfidenceProvider;
        private const int MaxRecords = 15;
        private const string Country = "US";

        [TestInitialize]
        public void Setup()
        {
            _addressParser = new Mock<IGoogleAddressParser>();
            _levelOfConfidenceProvider = new Mock<IGoogleLevelOfConfidenceProvider>();
        }

        [TestMethod]
        public void Constructor_GoogleAddressParserIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(() => new GoogleResponseParser(null, _levelOfConfidenceProvider.Object),
                SubSystemError.GeneralRequiredParameterIsUndefined, "addressParser");
        }

        [TestMethod]
        public void Constructor_LevelOfConfidenceProviderIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(() => new GoogleResponseParser(_addressParser.Object, null),
                SubSystemError.GeneralRequiredParameterIsUndefined, "levelOfConfidenceProvider");
        }

        [TestMethod]
        public void Constructor_HappyPath_ExceptionRaised()
        {
            ExceptionHelper.CheckHappyPath(() => new GoogleResponseParser(_addressParser.Object, _levelOfConfidenceProvider.Object));
        }

        #region AutocompleteAddressResponseModel tests

        [TestMethod]
        public void Parse_AutocompleteAddressResponseModelReturned_HappyPath()
        {
            var dataResponse = GoogleResponseParserHelper.GetAutomcompleteDataResponse();

            var suggestionModel = GoogleResponseParserHelper.GetAutocompleteSuggestionModel();
            _addressParser.Setup(p => p.ParseAddress(It.IsAny<XElement>())).Returns(suggestionModel.Address);
            _levelOfConfidenceProvider.Setup(p => p.GetLevelOfConfidence(It.IsAny<string>()))
                .Returns(LevelOfConfidence.Medium);

            var result = new GoogleResponseParser(_addressParser.Object, _levelOfConfidenceProvider.Object).ParseAutocompleteAddressResponse(dataResponse, MaxRecords, LevelOfConfidence.Low, Country);

            Assert.IsNotNull(result);
            Assert.AreEqual(suggestionModel.Address, result.Suggestions[0].Address);
            Assert.AreEqual(suggestionModel.Location.Latitude, result.Suggestions[0].Location.Latitude);
            Assert.AreEqual(suggestionModel.Location.Longitude, result.Suggestions[0].Location.Longitude);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(DataProviderType.Google, result.DataProvider);
        }

        [TestMethod]
        public void Parse_DataResponseIsNull_InvalidAddressAutocompleteResponseReturned()
        {
            var result = new GoogleResponseParser(_addressParser.Object, _levelOfConfidenceProvider.Object).ParseAutocompleteAddressResponse(null, MaxRecords, LevelOfConfidence.High, Country);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(DataProviderType.Google, result.DataProvider);
        }

        [TestMethod]
        public void Parse_DataResponseIsInvalid_InvalidAddressAutocompleteResponseReturned()
        {
            var dataResponse = GoogleResponseParserHelper.GetAutomcompleteDataResponse(true);
            var result = new GoogleResponseParser(_addressParser.Object, _levelOfConfidenceProvider.Object).ParseAutocompleteAddressResponse(dataResponse, MaxRecords, LevelOfConfidence.High, Country);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(DataProviderType.Google, result.DataProvider);
        }

        [TestMethod]
        public void Parse_NoAddressInResponse_InvalidAddressAutocompleteResponseReturned()
        {
            var dataResponse = GoogleResponseParserHelper.GetAutomcompleteDataResponseNoAddress();
            var result = new GoogleResponseParser(_addressParser.Object, _levelOfConfidenceProvider.Object).ParseAutocompleteAddressResponse(dataResponse, MaxRecords, LevelOfConfidence.High, Country);

            Assert.IsNotNull(result);
            Assert.IsNull(result.Suggestions);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(DataProviderType.Google, result.DataProvider);
        }
        
        #endregion

        #region ValidateAddressResponseModel tests

        [TestMethod]
        public void Parse_ValidateAddressResponseModelReturned_HappyPath()
        {
            var dataResponse = GoogleResponseParserHelper.GetValidateAddressDataResponse();

            var addressModel = GoogleResponseParserHelper.GetAddressModel();
            _addressParser.Setup(p => p.ParseAddress(It.IsAny<XElement>())).Returns(addressModel);
            _levelOfConfidenceProvider.Setup(p => p.GetLevelOfConfidence(It.IsAny<string>())).Returns(LevelOfConfidence.High);

            var locationModel = GoogleResponseParserHelper.GetLocationModel();

            var result = new GoogleResponseParser(_addressParser.Object, _levelOfConfidenceProvider.Object).ParseGeocodeAddressResponse(dataResponse, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.AreEqual(addressModel, result.Address);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(DataProviderType.Google, result.DataProvider);
            Assert.AreEqual(LevelOfConfidence.High, result.Confidence);
            Assert.AreEqual(locationModel.Latitude, result.Location.Latitude);
            Assert.AreEqual(locationModel.Longitude, result.Location.Longitude);
        }

        [TestMethod]
        public void Parse_DataResponseIsNull_InvalidValidateAddressResponseReturned()
        {
            var result = new GoogleResponseParser(_addressParser.Object, _levelOfConfidenceProvider.Object).ParseGeocodeAddressResponse(null, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(DataProviderType.Google, result.DataProvider);
            Assert.IsNull(result.Location);
            Assert.IsNull(result.Address);
        }

        [TestMethod]
        public void Parse_DataResponseIsInvalid_InvalidValidateAddressResponseReturned()
        {
            var dataResponse = GoogleResponseParserHelper.GetValidateAddressDataResponse(true);
            var result = new GoogleResponseParser(_addressParser.Object, _levelOfConfidenceProvider.Object).ParseGeocodeAddressResponse(dataResponse, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(DataProviderType.Google, result.DataProvider);
            Assert.IsNull(result.Location);
            Assert.IsNull(result.Address);
        }

        [TestMethod]
        public void Parse_NoAddressInResponse_InvalidValidateAddressResponseReturned()
        {
            var dataResponse = GoogleResponseParserHelper.GetValidateAddressDataResponseNoAddress();
            var result = new GoogleResponseParser(_addressParser.Object, _levelOfConfidenceProvider.Object).ParseGeocodeAddressResponse(dataResponse, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(DataProviderType.Google, result.DataProvider);
            Assert.IsNull(result.Location);
            Assert.IsNull(result.Address);
        }

        #endregion

        #region GeocodeAddressResponseModel tests

        [TestMethod]
        public void Parse_GeocodeAddressResponseModelReturned_HappyPath()
        {
            var dataResponse = GoogleResponseParserHelper.GetGeocodeAddressDataResponse();

            var addressModel = GoogleResponseParserHelper.GetAddressModel();
            _addressParser.Setup(p => p.ParseAddress(It.IsAny<XElement>())).Returns(addressModel);
            _levelOfConfidenceProvider.Setup(p => p.GetLevelOfConfidence(It.IsAny<string>())).Returns(LevelOfConfidence.High);

            var locationModel = GoogleResponseParserHelper.GetLocationModel();

            var result = new GoogleResponseParser(_addressParser.Object, _levelOfConfidenceProvider.Object).ParseGeocodeAddressResponse(dataResponse, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.AreEqual(addressModel, result.Address);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(DataProviderType.Google, result.DataProvider);
            Assert.AreEqual(LevelOfConfidence.High, result.Confidence);
            Assert.AreEqual(locationModel.Latitude, result.Location.Latitude);
            Assert.AreEqual(locationModel.Longitude, result.Location.Longitude);
        }

        [TestMethod]
        public void Parse_DataResponseIsNull_InvalidGeocodeAddressResponseReturned()
        {
            var result = new GoogleResponseParser(_addressParser.Object, _levelOfConfidenceProvider.Object).ParseGeocodeAddressResponse(null, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(DataProviderType.Google, result.DataProvider);
            Assert.IsNull(result.Location);
            Assert.IsNull(result.Address);
        }

        [TestMethod]
        public void Parse_DataResponseIsInvalid_InvalidGeocodeAddressResponseReturned()
        {
            var dataResponse = GoogleResponseParserHelper.GetGeocodeAddressDataResponse(true);
            var result = new GoogleResponseParser(_addressParser.Object, _levelOfConfidenceProvider.Object).ParseGeocodeAddressResponse(dataResponse, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(DataProviderType.Google, result.DataProvider);
            Assert.IsNull(result.Location);
            Assert.IsNull(result.Address);
        }

        [TestMethod]
        public void Parse_NoAddressInResponse_InvalidGeocodeAddressResponseReturned()
        {
            var dataResponse = GoogleResponseParserHelper.GetGeocodeAddressDataResponseNoAddress();
            var result = new GoogleResponseParser(_addressParser.Object, _levelOfConfidenceProvider.Object).ParseGeocodeAddressResponse(dataResponse, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(DataProviderType.Google, result.DataProvider);
            Assert.IsNull(result.Location);
            Assert.IsNull(result.Address);
        }

        #endregion

        #region GetMapResponseModel tests

        [TestMethod]
        public void Parse_GetMapResponseModelReturned_HappyPath()
        {
            var dataResponse = GoogleResponseParserHelper.GetMapDataResponse();
            var result = new GoogleResponseParser(_addressParser.Object, _levelOfConfidenceProvider.Object).ParseMapResponse(dataResponse);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.MapData);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(DataProviderType.Google, result.DataProvider);
        }

        [TestMethod]
        public void Parse_DataResponseIsNull_InvalidGetMapResponseReturned()
        {
            BinaryDataResponse response = null;
            // ReSharper disable ExpressionIsAlwaysNull
            var result = new GoogleResponseParser(_addressParser.Object, _levelOfConfidenceProvider.Object).ParseMapResponse(response);
            // ReSharper restore ExpressionIsAlwaysNull

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(DataProviderType.Google, result.DataProvider);
        }

        [TestMethod]
        public void Parse_DataResponseIsInvalid_InvalidGetMapResponseReturned()
        {
            var dataResponse = GoogleResponseParserHelper.GetMapDataResponse(true);
            var result = new GoogleResponseParser(_addressParser.Object, _levelOfConfidenceProvider.Object).ParseMapResponse(dataResponse);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(DataProviderType.Google, result.DataProvider);
        }

        #endregion
    }
}
