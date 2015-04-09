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
    public class BingResponseParserUnitTests
    {
        private Mock<IBingAddressParser> _bingAddressParser;
        
        //Bing does not use these parameters
        private const int MaxRecords = 15;
        private const string Country = "US";

        [TestInitialize]
        public void Setup()
        {
            _bingAddressParser = new Mock<IBingAddressParser>();
        }

        [TestMethod]
        public void Constructor_BingAddressParserIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(() => new BingResponseParser(null),
                SubSystemError.GeneralRequiredParameterIsUndefined, "addressParser");
        }

        #region AutocompleteAddressResponse tests

        [TestMethod]
        public void Parse_AutocompleteAddressResponseModelReturned_HappyPath()
        {
            var dataResponse = BingResponseParserHelper.GetAutomcompleteDataResponse();

            var suggestionModel = BingResponseParserHelper.GetAutocompleteSuggestionModel();
            _bingAddressParser.Setup(p => p.ParseAddress(It.IsAny<XElement>(), It.IsAny<XNamespace>())).Returns(suggestionModel.Address);

            var result = new BingResponseParser(_bingAddressParser.Object).ParseAutocompleteAddressResponse(dataResponse, MaxRecords, LevelOfConfidence.Low, Country);

            Assert.IsNotNull(result);
            Assert.AreEqual(suggestionModel.Address, result.Suggestions[0].Address);
            Assert.AreEqual(suggestionModel.Location.Latitude, result.Suggestions[0].Location.Latitude);
            Assert.AreEqual(suggestionModel.Location.Longitude, result.Suggestions[0].Location.Longitude);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(DataProviderType.Bing, result.DataProvider);
        }

        [TestMethod]
        public void Parse_DataResponseIsNull_InvalidAddressAutocompleteResponseReturned()
        {
            var result = new BingResponseParser(_bingAddressParser.Object).ParseAutocompleteAddressResponse(null, MaxRecords, LevelOfConfidence.High, Country);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(DataProviderType.Bing, result.DataProvider);
        }

        [TestMethod]
        public void Parse_DataResponseIsInvalid_InvalidAddressAutocompleteResponseReturned()
        {
            var dataResponse = BingResponseParserHelper.GetAutomcompleteDataResponse(true);
            var result = new BingResponseParser(_bingAddressParser.Object).ParseAutocompleteAddressResponse(dataResponse, MaxRecords, LevelOfConfidence.High, Country);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(DataProviderType.Bing, result.DataProvider);
        }

        [TestMethod]
        public void Parse_NoAddressInResponse_InvalidAddressAutocompleteResponseReturned()
        {
            var dataResponse = BingResponseParserHelper.GetAutomcompleteDataResponseNoAddress();
            var result = new BingResponseParser(_bingAddressParser.Object).ParseAutocompleteAddressResponse(dataResponse, MaxRecords, LevelOfConfidence.High, Country);
            
            Assert.IsNotNull(result);
            Assert.IsNull(result.Suggestions);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(DataProviderType.Bing, result.DataProvider);
        }

        #endregion

        #region ValidateAddressResponseModel tests

        [TestMethod]
        public void Parse_ValidateAddressResponseModelReturned_HappyPath()
        {
            var dataResponse = BingResponseParserHelper.GetValidateAddressDataResponse();

            var addressModel = BingResponseParserHelper.GetAddressModel();
            _bingAddressParser.Setup(p => p.ParseAddress(It.IsAny<XElement>(), It.IsAny<XNamespace>())).Returns(addressModel);

            var locationModel = BingResponseParserHelper.GetLocationModel();

            var result = new BingResponseParser(_bingAddressParser.Object).ParseGeocodeAddressResponse(dataResponse, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.AreEqual(addressModel, result.Address);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(DataProviderType.Bing, result.DataProvider);
            Assert.AreEqual(LevelOfConfidence.High, result.Confidence);
            Assert.AreEqual(locationModel.Latitude, result.Location.Latitude);
            Assert.AreEqual(locationModel.Longitude, result.Location.Longitude);
        }
        
        [TestMethod]
        public void Parse_DataResponseIsNull_InvalidValidateAddressResponseReturned()
        {
            var result = new BingResponseParser(_bingAddressParser.Object).ParseGeocodeAddressResponse(null, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(DataProviderType.Bing, result.DataProvider);
            Assert.IsNull(result.Location);
            Assert.IsNull(result.Address);
        }

        [TestMethod]
        public void Parse_DataResponseIsInvalid_InvalidValidateAddressResponseReturned()
        {
            var dataResponse = BingResponseParserHelper.GetValidateAddressDataResponse(true);
            var result = new BingResponseParser(_bingAddressParser.Object).ParseGeocodeAddressResponse(dataResponse, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(DataProviderType.Bing, result.DataProvider);
            Assert.IsNull(result.Location);
            Assert.IsNull(result.Address);
        }

        [TestMethod]
        public void Parse_NoAddressInResponse_InvalidValidateAddressResponseReturned()
        {
            var dataResponse = BingResponseParserHelper.GetValidateAddressDataResponseNoAddress();
            var result = new BingResponseParser(_bingAddressParser.Object).ParseGeocodeAddressResponse(dataResponse, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(DataProviderType.Bing, result.DataProvider);
            Assert.IsNull(result.Location);
            Assert.IsNull(result.Address);
        }

        [TestMethod]
        public void Parse_NoLocationInResponse_ExceptionRaised()
        {
            var dataResponse = BingResponseParserHelper.GetValidateAddressDataResponseNoLocation();

            ExceptionHelper.CheckException(() => new BingResponseParser(_bingAddressParser.Object).ParseGeocodeAddressResponse(dataResponse, LevelOfConfidence.High),
                SubSystemError.GeolocationLocationIsNotFoundInResponse, DataProviderType.Bing);
        }

        #endregion

        #region GeocodeAddressResponseModel tests

        [TestMethod]
        public void Parse_GeocodeAddressResponseModelReturned_HappyPath()
        {
            var dataResponse = BingResponseParserHelper.GetGeocodeAddressDataResponse();

            var addressModel = BingResponseParserHelper.GetAddressModel();
            _bingAddressParser.Setup(p => p.ParseAddress(It.IsAny<XElement>(), It.IsAny<XNamespace>())).Returns(addressModel);

            var locationModel = BingResponseParserHelper.GetLocationModel();

            var result = new BingResponseParser(_bingAddressParser.Object).ParseGeocodeAddressResponse(dataResponse, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.AreEqual(addressModel, result.Address);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(DataProviderType.Bing, result.DataProvider);
            Assert.AreEqual(LevelOfConfidence.High, result.Confidence);
            Assert.AreEqual(locationModel.Latitude, result.Location.Latitude);
            Assert.AreEqual(locationModel.Longitude, result.Location.Longitude);
        }

        [TestMethod]
        public void Parse_DataResponseIsNull_InvalidGeocodeAddressResponseReturned()
        {
            var result = new BingResponseParser(_bingAddressParser.Object).ParseGeocodeAddressResponse(null, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(DataProviderType.Bing, result.DataProvider);
            Assert.IsNull(result.Location);
            Assert.IsNull(result.Address);
        }

        [TestMethod]
        public void Parse_DataResponseIsInvalid_InvalidGeocodeAddressResponseReturned()
        {
            var dataResponse = BingResponseParserHelper.GetGeocodeAddressDataResponse(true);
            var result = new BingResponseParser(_bingAddressParser.Object).ParseGeocodeAddressResponse(dataResponse, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(DataProviderType.Bing, result.DataProvider);
            Assert.IsNull(result.Location);
            Assert.IsNull(result.Address);
        }

        [TestMethod]
        public void Parse_NoAddressInResponse_InvalidGeocodeAddressResponseReturned()
        {
            var dataResponse = BingResponseParserHelper.GetGeocodeAddressDataResponseNoAddress();
            var result = new BingResponseParser(_bingAddressParser.Object).ParseGeocodeAddressResponse(dataResponse, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(DataProviderType.Bing, result.DataProvider);
            Assert.IsNull(result.Location);
            Assert.IsNull(result.Address);
        }

        [TestMethod]
        public void Parse_NoLocationInResponse_InvalidGeocodeAddressResponseReturned()
        {
            var dataResponse = BingResponseParserHelper.GetGeocodeAddressDataResponseNoLocation();
            var result = new BingResponseParser(_bingAddressParser.Object).ParseGeocodeAddressResponse(dataResponse, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(DataProviderType.Bing, result.DataProvider);
            Assert.IsNull(result.Location);
            Assert.IsNull(result.Address);
        }

        #endregion

        #region GetMapResponseModel tests

        [TestMethod]
        public void Parse_GetMapResponseModelReturned_HappyPath()
        {
            var dataResponse = BingResponseParserHelper.GetMapDataResponse();
            var result = new BingResponseParser(_bingAddressParser.Object).ParseMapResponse(dataResponse);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.MapData);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(DataProviderType.Bing, result.DataProvider);
        }

        [TestMethod]
        public void Parse_DataResponseIsNull_InvalidGetMapResponseReturned()
        {
            BinaryDataResponse response = null;
// ReSharper disable ExpressionIsAlwaysNull
            var result = new BingResponseParser(_bingAddressParser.Object).ParseMapResponse(response);
// ReSharper restore ExpressionIsAlwaysNull

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(DataProviderType.Bing, result.DataProvider);
        }

        [TestMethod]
        public void Parse_DataResponseIsInvalid_InvalidGetMapResponseReturned()
        {
            var dataResponse = BingResponseParserHelper.GetMapDataResponse(true);
            var result = new BingResponseParser(_bingAddressParser.Object).ParseMapResponse(dataResponse);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(DataProviderType.Bing, result.DataProvider);
        }
        
        #endregion
    }
}
