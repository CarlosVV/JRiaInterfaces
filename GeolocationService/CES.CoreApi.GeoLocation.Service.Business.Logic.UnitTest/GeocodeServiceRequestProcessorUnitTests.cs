using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Constants;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Processors;
using CES.CoreApi.GeoLocation.Service.UnitTestTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.UnitTest
{
    [TestClass]
    public class GeocodeServiceRequestProcessorUnitTests
    {
        private Mock<ICountryConfigurationProvider> _configurationProvider;
        private Mock<IGeocodeAddressDataProvider> _geocodeAddressDataProvider;
        
        private const string Country = "US";
        private const string FormattedAddress = "6565 Knott Ave., Buena Park, CA 90620";

        [TestInitialize]
        public void Setup()
        {
            _configurationProvider = new Mock<ICountryConfigurationProvider>();
            _geocodeAddressDataProvider = new Mock<IGeocodeAddressDataProvider>();
        }

        #region Constructor tests

        [TestMethod]
        public void Constructor_ConfigurationProviderIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(
                () => new GeocodeServiceRequestProcessor(null, _geocodeAddressDataProvider.Object),
                 SubSystemError.GeneralRequiredParameterIsUndefined, "configurationProvider");
        }

        [TestMethod]
        public void Constructor_GeocodeAddressDataProviderIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(
                () => new GeocodeServiceRequestProcessor(_configurationProvider.Object, null),
                SubSystemError.GeneralRequiredParameterIsUndefined, "geocodeAddressDataProvider");
        }

        #endregion

        [TestMethod]
        public void GeocodeAddress_AddressModelPassed_HappyPath()
        {
            var addressModel = TestModelsProvider.GetAddressModel();
            var responseModel = GetGeocodeAddressResponseModel();
            var countryConfiguration = TestModelsProvider.GetUsCountryConfiguration();

            _geocodeAddressDataProvider.Setup(p => p.Geocode(It.IsAny<AddressModel>(), It.IsAny<DataProviderType>(), It.IsAny<LevelOfConfidence>()))
                .Returns(responseModel);
            _configurationProvider.Setup(p => p.ConfigurationProvider.Read<int>(ConfigurationConstants.NumberOfProvidersToProcessResult)).Returns(2);
            _configurationProvider.Setup(p => p.GetProviderConfigurationByCountry(Country)).Returns(countryConfiguration);
            

            var result = new GeocodeServiceRequestProcessor(_configurationProvider.Object, _geocodeAddressDataProvider.Object)
                    .GeocodeAddress(addressModel, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Address);
            Assert.IsNotNull(result.Location);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(DataProviderType.Google, result.DataProvider);
        }

        [TestMethod]
        public void GeocodeAddress_FormattedAddressPassed_HappyPath()
        {
            var responseModel = GetGeocodeAddressResponseModel();
            var countryConfiguration = TestModelsProvider.GetUsCountryConfiguration();

            _geocodeAddressDataProvider.Setup(p => p.Geocode(It.IsAny<string>(), It.IsAny<DataProviderType>(), It.IsAny<LevelOfConfidence>())).Returns(responseModel);
            _configurationProvider.Setup(p => p.ConfigurationProvider.Read<int>(ConfigurationConstants.NumberOfProvidersToProcessResult)).Returns(2);
            _configurationProvider.Setup(p => p.GetProviderConfigurationByCountry(Country)).Returns(countryConfiguration);
            
            var result = new GeocodeServiceRequestProcessor(_configurationProvider.Object, _geocodeAddressDataProvider.Object)
                    .GeocodeAddress(FormattedAddress, Country, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Address);
            Assert.IsNotNull(result.Location);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(DataProviderType.Google, result.DataProvider);
        }

        [TestMethod]
        public void ReverseGeocodePoint_LocationModelPassed_HappyPath()
        {
            var locationModel = GetLocationModel();
            var responseModel = GetGeocodeAddressResponseModel();
            var countryConfiguration = TestModelsProvider.GetUsCountryConfiguration();

            _geocodeAddressDataProvider.Setup(p => p.ReverseGeocode(It.IsAny<LocationModel>(), It.IsAny<DataProviderType>(), It.IsAny<LevelOfConfidence>())).Returns(responseModel);
            _configurationProvider.Setup(p => p.ConfigurationProvider.Read<int>(ConfigurationConstants.NumberOfProvidersToProcessResult)).Returns(2);
            _configurationProvider.Setup(p => p.GetProviderConfigurationByCountry(Country)).Returns(countryConfiguration);
            
            var result = new GeocodeServiceRequestProcessor(_configurationProvider.Object, _geocodeAddressDataProvider.Object)
                    .ReverseGeocodePoint(locationModel, Country, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Address);
            Assert.IsNotNull(result.Location);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(DataProviderType.Google, result.DataProvider);
        }
        
        [TestMethod]
        public void GeocodeAddress_NoProviderFound_ExceptionRaised()
        {
            var addressModel = TestModelsProvider.GetAddressModel();
            var responseModel = GetGeocodeAddressResponseModel();
            var countryConfiguration = TestModelsProvider.GetCountryConfigurationWithoutProviders();

            _geocodeAddressDataProvider.Setup(p => p.Geocode(It.IsAny<AddressModel>(), It.IsAny<DataProviderType>(), It.IsAny<LevelOfConfidence>()))
                .Returns(responseModel);
            _configurationProvider.Setup(p => p.ConfigurationProvider.Read<int>(ConfigurationConstants.NumberOfProvidersToProcessResult)).Returns(2);
            _configurationProvider.Setup(p => p.GetProviderConfigurationByCountry(Country)).Returns(countryConfiguration);

            ExceptionHelper.CheckException(
                () => new GeocodeServiceRequestProcessor(_configurationProvider.Object, _geocodeAddressDataProvider.Object)
                    .GeocodeAddress(addressModel, LevelOfConfidence.High),
                SubSystemError.GeolocationDataProviderNotFound, DataProviderServiceType.Geocoding);
        }

        [TestMethod]
        public void GeocodeAddress_FirstProviderResponseIsInvalid_SecondProviderUsed()
        {
            var responseModel1 = GetGeocodeAddressResponseModel(true);
            var responseModel2 = GetGeocodeAddressResponseModel();
            var countryConfiguration = TestModelsProvider.GetUsCountryConfiguration();

            _geocodeAddressDataProvider.Setup(p => p.Geocode(It.IsAny<string>(), DataProviderType.Bing, It.IsAny<LevelOfConfidence>())).Returns(responseModel1);
            _geocodeAddressDataProvider.Setup(p => p.Geocode(It.IsAny<string>(), DataProviderType.Google, It.IsAny<LevelOfConfidence>())).Returns(responseModel2);
            _configurationProvider.Setup(p => p.ConfigurationProvider.Read<int>(ConfigurationConstants.NumberOfProvidersToProcessResult)).Returns(2);
            _configurationProvider.Setup(p => p.GetProviderConfigurationByCountry(Country)).Returns(countryConfiguration);

            var result = new GeocodeServiceRequestProcessor(_configurationProvider.Object, _geocodeAddressDataProvider.Object)
                    .GeocodeAddress(FormattedAddress, Country, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Address);
            Assert.IsNotNull(result.Location);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(DataProviderType.Google, result.DataProvider);
        }

        [TestMethod]
        public void GeocodeAddress_BothProviderResponseIsInvalid_NoLocationPopulated()
        {
            var responseModel1 = GetGeocodeAddressResponseModel(true);
            var responseModel2 = GetGeocodeAddressResponseModel(true);
            var countryConfiguration = TestModelsProvider.GetUsCountryConfiguration();

            _geocodeAddressDataProvider.Setup(p => p.Geocode(It.IsAny<string>(), DataProviderType.Bing, It.IsAny<LevelOfConfidence>())).Returns(responseModel1);
            _geocodeAddressDataProvider.Setup(p => p.Geocode(It.IsAny<string>(), DataProviderType.Google, It.IsAny<LevelOfConfidence>())).Returns(responseModel2);
            _configurationProvider.Setup(p => p.ConfigurationProvider.Read<int>(ConfigurationConstants.NumberOfProvidersToProcessResult)).Returns(2);
            _configurationProvider.Setup(p => p.GetProviderConfigurationByCountry(Country)).Returns(countryConfiguration);
            
            var result = new GeocodeServiceRequestProcessor(_configurationProvider.Object, _geocodeAddressDataProvider.Object)
                    .GeocodeAddress(FormattedAddress, Country, LevelOfConfidence.High);

            Assert.IsNotNull(result);
            Assert.IsNull(result.Address);
            Assert.IsNull(result.Location);
            Assert.IsTrue(!result.IsValid);
            Assert.AreEqual(DataProviderType.Google, result.DataProvider);
        }

        #region private methods

        private static LocationModel GetLocationModel()
        {
            return new LocationModel
            {
                Latitude = 33.755802,
                Longitude = -118.308556
            };
        }

        private static GeocodeAddressResponseModel GetGeocodeAddressResponseModel(bool makeInvalid = false)
        {
            return new GeocodeAddressResponseModel
            {
                Address = makeInvalid ? null : TestModelsProvider.GetAddressModel(),
                Confidence = LevelOfConfidence.High,
                DataProvider = DataProviderType.Google,
                IsValid = !makeInvalid,
                Location = makeInvalid ? null : GetLocationModel()
            };
        }

        #endregion
    }
}
