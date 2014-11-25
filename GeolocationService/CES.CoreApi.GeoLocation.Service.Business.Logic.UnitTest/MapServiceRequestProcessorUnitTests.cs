using System.Collections.Generic;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Processors;
using CES.CoreApi.GeoLocation.Service.UnitTestTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.UnitTest
{
    [TestClass]
    public class MapServiceRequestProcessorUnitTests
    {
        private Mock<ICountryConfigurationProvider> _configurationProvider;
        private Mock<IMappingDataProvider> _mappingDataProvider;
        private Mock<MapOutputParametersModel> _mapOutputParametersModel;

        private const string Country = "US";

        [TestInitialize]
        public void Setup()
        {
            _configurationProvider = new Mock<ICountryConfigurationProvider>();
            _mappingDataProvider = new Mock<IMappingDataProvider>();
            _mapOutputParametersModel = new Mock<MapOutputParametersModel>();
        }

        #region Constructor tests

        [TestMethod]
        public void Constructor_ConfigurationProviderIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(
                () => new MapServiceRequestProcessor(null, _mappingDataProvider.Object),
                 SubSystemError.GeneralRequiredParameterIsUndefined, "configurationProvider");
        }

        [TestMethod]
        public void Constructor_MappingDataProviderIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(
                () => new MapServiceRequestProcessor(_configurationProvider.Object, null),
                SubSystemError.GeneralRequiredParameterIsUndefined, "mappingDataProvider");
        }

        #endregion

        [TestMethod]
        public void GeocodeAddress_AddressModelPassed_HappyPath()
        {
            var responseModel = GetMapResponseModel();
            var countryConfiguration = TestModelsProvider.GetUsCountryConfiguration();

            _configurationProvider.Setup(p => p.GetProviderConfigurationByCountry(Country)).Returns(countryConfiguration);
            _mappingDataProvider.Setup(p => p.GetMap(It.IsAny<LocationModel>(), It.IsAny<MapSizeModel>(), It.IsAny<MapOutputParametersModel>(), It.IsAny<ICollection<PushPinModel>>(), It.IsAny<DataProviderType>())).Returns(responseModel);
            _mapOutputParametersModel.Object.ImageFormat = ImageFormat.Jpeg;
            _mapOutputParametersModel.Object.MapStyle = MapStyle.Road;

            var result = new MapServiceRequestProcessor(_configurationProvider.Object, _mappingDataProvider.Object)
                .GetMap(Country, It.IsAny<LocationModel>(), It.IsAny<MapSizeModel>(), _mapOutputParametersModel.Object, It.IsAny<ICollection<PushPinModel>>());

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.MapData);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(DataProviderType.Google, result.DataProvider);
        }

        [TestMethod]
        public void GeocodeAddress_NoProviderFound_ExceptionRaised()
        {
            var countryConfiguration = TestModelsProvider.GetCountryConfigurationWithoutProviders();

            _configurationProvider.Setup(p => p.GetProviderConfigurationByCountry(Country)).Returns(countryConfiguration);
            _mappingDataProvider.Setup(
                p => p.GetMap(It.IsAny<LocationModel>(), It.IsAny<MapSizeModel>(), It.IsAny<MapOutputParametersModel>(),
                        It.IsAny<ICollection<PushPinModel>>(), It.IsAny<DataProviderType>())).Returns(It.IsAny<GetMapResponseModel>);

            ExceptionHelper.CheckException(
                () => new MapServiceRequestProcessor(_configurationProvider.Object, _mappingDataProvider.Object)
                    .GetMap(Country, It.IsAny<LocationModel>(), It.IsAny<MapSizeModel>(),
                        It.IsAny<MapOutputParametersModel>(), It.IsAny<ICollection<PushPinModel>>()),
                SubSystemError.GeolocationDataProviderNotFound, DataProviderServiceType.Mapping);
        }

        private static GetMapResponseModel GetMapResponseModel()
        {
            return new GetMapResponseModel
            {
                DataProvider = DataProviderType.Google,
                IsValid = true,
                MapData = new byte[1024]
            };
        }

        [TestMethod]
        public void GeocodeAddress_ImageFormatUndefined_DefaultImageFormatUsed()
        {
            var responseModel = GetMapResponseModel();
            var countryConfiguration = TestModelsProvider.GetUsCountryConfiguration();

            _configurationProvider.Setup(p => p.GetProviderConfigurationByCountry(Country)).Returns(countryConfiguration);
            _mappingDataProvider.Setup(p => p.GetMap(It.IsAny<LocationModel>(), It.IsAny<MapSizeModel>(), It.IsAny<MapOutputParametersModel>(), It.IsAny<ICollection<PushPinModel>>(), It.IsAny<DataProviderType>())).Returns(responseModel);
            _mapOutputParametersModel.Object.ImageFormat = ImageFormat.Undefined;
            _mapOutputParametersModel.Object.MapStyle = MapStyle.Road;

            var result = new MapServiceRequestProcessor(_configurationProvider.Object, _mappingDataProvider.Object)
                .GetMap(Country, It.IsAny<LocationModel>(), It.IsAny<MapSizeModel>(), _mapOutputParametersModel.Object, It.IsAny<ICollection<PushPinModel>>());

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.MapData);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(DataProviderType.Google, result.DataProvider);
        }

        [TestMethod]
        public void GeocodeAddress_MapStyleUndefined_DefaultMapStyleUsed()
        {
            var responseModel = GetMapResponseModel();
            var countryConfiguration = TestModelsProvider.GetUsCountryConfiguration();

            _configurationProvider.Setup(p => p.GetProviderConfigurationByCountry(Country)).Returns(countryConfiguration);
            _mappingDataProvider.Setup(p => p.GetMap(It.IsAny<LocationModel>(), It.IsAny<MapSizeModel>(), It.IsAny<MapOutputParametersModel>(), It.IsAny<ICollection<PushPinModel>>(), It.IsAny<DataProviderType>())).Returns(responseModel);
            _mapOutputParametersModel.Object.ImageFormat = ImageFormat.Jpeg;
            _mapOutputParametersModel.Object.MapStyle = MapStyle.Undefined;

            var result = new MapServiceRequestProcessor(_configurationProvider.Object, _mappingDataProvider.Object)
                .GetMap(Country, It.IsAny<LocationModel>(), It.IsAny<MapSizeModel>(), _mapOutputParametersModel.Object, It.IsAny<ICollection<PushPinModel>>());

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.MapData);
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(DataProviderType.Google, result.DataProvider);
        }
    }
}
