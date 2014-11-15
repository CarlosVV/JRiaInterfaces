using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Providers;
using CES.CoreApi.GeoLocation.Service.UnitTestTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.UnitTest
{
    [TestClass]
    public class GeocodeAddressDataProviderUnitTests
    {
        private Mock<IDataResponseProvider> _responseProvider;
        private Mock<IEntityFactory> _entityFactory;
        private Mock<IUrlBuilder> _urlBuilder;
        private Mock<IResponseParser> _responseParser;

        private const string Country = "US";

        [TestInitialize]
        public void Setup()
        {
            _responseProvider = new Mock<IDataResponseProvider>();
            _entityFactory = new Mock<IEntityFactory>();
            _urlBuilder = new Mock<IUrlBuilder>();
            _responseParser = new Mock<IResponseParser>();
        }

        #region Constructor tests

        [TestMethod]
        public void Constructor_ResponseProviderIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(
                () => new GeocodeAddressDataProvider(null, _entityFactory.Object),
                 SubSystemError.GeneralRequiredParameterIsUndefined, "responseProvider");
        }

        [TestMethod]
        public void Constructor_EntityFactoryIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(
                () => new GeocodeAddressDataProvider(_responseProvider.Object, null),
                SubSystemError.GeneralRequiredParameterIsUndefined, "entityFactory");
        }

        [TestMethod]
        public void Constructor_HappyPath()
        {
            ExceptionHelper.CheckHappyPath(() => new GeocodeAddressDataProvider(_responseProvider.Object, _entityFactory.Object));
        }

        #endregion

        [TestMethod]
        public void GeocodeAddressModel()
        {
            _entityFactory.Setup(p => p.GetInstance<IUrlBuilder>(It.IsAny<DataProviderType>(), FactoryEntity.UrlBuilder))
                .Returns(_urlBuilder.Object)
                .Verifiable();
            _urlBuilder.Setup(p => p.BuildUrl(It.IsAny<AddressModel>()))
                .Returns(It.IsAny<string>())
                .Verifiable();
            _responseProvider.Setup(p => p.GetResponse(It.IsAny<string>(), It.IsAny<DataProviderType>()))
                .Returns(It.IsAny<DataResponse>())
                .Verifiable();
            _entityFactory.Setup(p => p.GetInstance<IResponseParser>(It.IsAny<DataProviderType>(), FactoryEntity.Parser))
                .Returns(_responseParser.Object)
                .Verifiable();
            _responseParser.Setup(p => p.Parse(It.IsAny<DataResponse>(), It.IsAny<LevelOfConfidence>()))
                .Returns(new GeocodeAddressResponseModel())
                .Verifiable();

            var result = new GeocodeAddressDataProvider(_responseProvider.Object, _entityFactory.Object)
                .Geocode(It.IsAny<AddressModel>(), It.IsAny<DataProviderType>(), It.IsAny<LevelOfConfidence>());

            _entityFactory.Verify(p => p.GetInstance<IUrlBuilder>(It.IsAny<DataProviderType>(), FactoryEntity.UrlBuilder), Times.Once);
            _urlBuilder.Verify(p => p.BuildUrl(It.IsAny<AddressModel>()), Times.Once);
            _responseProvider.Verify(p => p.GetResponse(It.IsAny<string>(), It.IsAny<DataProviderType>()), Times.Once);
            _entityFactory.Verify(p => p.GetInstance<IResponseParser>(It.IsAny<DataProviderType>(), FactoryEntity.Parser), Times.Once);
            _responseParser.Verify(p => p.Parse(It.IsAny<DataResponse>(), It.IsAny<LevelOfConfidence>()), Times.Once());
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GeocodeFormattedAddress()
        {
            _entityFactory.Setup(p => p.GetInstance<IUrlBuilder>(It.IsAny<DataProviderType>(), FactoryEntity.UrlBuilder))
                .Returns(_urlBuilder.Object)
                .Verifiable();
            _urlBuilder.Setup(p => p.BuildUrl(It.IsAny<string>()))
                .Returns(It.IsAny<string>())
                .Verifiable();
            _responseProvider.Setup(p => p.GetResponse(It.IsAny<string>(), It.IsAny<DataProviderType>()))
                .Returns(It.IsAny<DataResponse>())
                .Verifiable();
            _entityFactory.Setup(p => p.GetInstance<IResponseParser>(It.IsAny<DataProviderType>(), FactoryEntity.Parser))
                .Returns(_responseParser.Object)
                .Verifiable();
            _responseParser.Setup(p => p.Parse(It.IsAny<DataResponse>(), It.IsAny<LevelOfConfidence>()))
                .Returns(new GeocodeAddressResponseModel())
                .Verifiable();

            var result = new GeocodeAddressDataProvider(_responseProvider.Object, _entityFactory.Object)
                .Geocode(It.IsAny<string>(), It.IsAny<DataProviderType>(), It.IsAny<LevelOfConfidence>());

            _entityFactory.Verify(p => p.GetInstance<IUrlBuilder>(It.IsAny<DataProviderType>(), FactoryEntity.UrlBuilder), Times.Once);
            _urlBuilder.Verify(p => p.BuildUrl(It.IsAny<string>()), Times.Once);
            _responseProvider.Verify(p => p.GetResponse(It.IsAny<string>(), It.IsAny<DataProviderType>()), Times.Once);
            _entityFactory.Verify(p => p.GetInstance<IResponseParser>(It.IsAny<DataProviderType>(), FactoryEntity.Parser), Times.Once);
            _responseParser.Verify(p => p.Parse(It.IsAny<DataResponse>(), It.IsAny<LevelOfConfidence>()), Times.Once());
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ReverseGeocodeLocationModel()
        {
            _entityFactory.Setup(p => p.GetInstance<IUrlBuilder>(It.IsAny<DataProviderType>(), FactoryEntity.UrlBuilder))
                .Returns(_urlBuilder.Object)
                .Verifiable();
            _urlBuilder.Setup(p => p.BuildUrl(It.IsAny<LocationModel>()))
                .Returns(It.IsAny<string>())
                .Verifiable();
            _responseProvider.Setup(p => p.GetResponse(It.IsAny<string>(), It.IsAny<DataProviderType>()))
                .Returns(It.IsAny<DataResponse>())
                .Verifiable();
            _entityFactory.Setup(p => p.GetInstance<IResponseParser>(It.IsAny<DataProviderType>(), FactoryEntity.Parser))
                .Returns(_responseParser.Object)
                .Verifiable();
            _responseParser.Setup(p => p.Parse(It.IsAny<DataResponse>(), It.IsAny<LevelOfConfidence>()))
                .Returns(new GeocodeAddressResponseModel())
                .Verifiable();

            var result = new GeocodeAddressDataProvider(_responseProvider.Object, _entityFactory.Object)
                .ReverseGeocode(It.IsAny<LocationModel>(), It.IsAny<DataProviderType>(), It.IsAny<LevelOfConfidence>());

            _entityFactory.Verify(p => p.GetInstance<IUrlBuilder>(It.IsAny<DataProviderType>(), FactoryEntity.UrlBuilder), Times.Once);
            _urlBuilder.Verify(p => p.BuildUrl(It.IsAny<LocationModel>()), Times.Once);
            _responseProvider.Verify(p => p.GetResponse(It.IsAny<string>(), It.IsAny<DataProviderType>()), Times.Once);
            _entityFactory.Verify(p => p.GetInstance<IResponseParser>(It.IsAny<DataProviderType>(), FactoryEntity.Parser), Times.Once);
            _responseParser.Verify(p => p.Parse(It.IsAny<DataResponse>(), It.IsAny<LevelOfConfidence>()), Times.Once());
            Assert.IsNotNull(result);
        }
    }
}
