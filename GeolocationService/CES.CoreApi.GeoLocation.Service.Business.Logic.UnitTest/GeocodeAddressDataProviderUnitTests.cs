using CES.CoreApi.Common.Enumerations;
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
        private Mock<IUrlBuilderFactory> _urlBuilderFactory;
        private Mock<IResponseParserFactory> _responseParserFactory;
        private Mock<IUrlBuilder> _urlBuilder;
        private Mock<IResponseParser> _responseParser;

        private const string Country = "US";

        [TestInitialize]
        public void Setup()
        {
            _responseProvider = new Mock<IDataResponseProvider>();
            _urlBuilderFactory = new Mock<IUrlBuilderFactory>();
            _responseParserFactory = new Mock<IResponseParserFactory>();
            _urlBuilder = new Mock<IUrlBuilder>();
            _responseParser = new Mock<IResponseParser>();
        }

        #region Constructor tests

        [TestMethod]
        public void Constructor_ResponseProviderIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(
                () => new GeocodeAddressDataProvider(null, _urlBuilderFactory.Object, _responseParserFactory.Object),
                 SubSystemError.GeneralRequiredParameterIsUndefined, "responseProvider");
        }

        [TestMethod]
        public void Constructor_UrlBuilderFactoryFactoryIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(
                () => new GeocodeAddressDataProvider(_responseProvider.Object, null, _responseParserFactory.Object),
                SubSystemError.GeneralRequiredParameterIsUndefined, "urlBuilderFactory");
        }

        [TestMethod]
        public void Constructor_HappyPath()
        {
            ExceptionHelper.CheckHappyPath(() => new GeocodeAddressDataProvider(_responseProvider.Object, _urlBuilderFactory.Object, _responseParserFactory.Object));
        }

        #endregion

        [TestMethod]
        public void GeocodeAddressModel()
        {
            _urlBuilderFactory.Setup(p => p.GetInstance<IUrlBuilder>(It.IsAny<DataProviderType>(), FactoryEntity.UrlBuilder))
                .Returns(_urlBuilder.Object)
                .Verifiable();
            _urlBuilder.Setup(p => p.BuildUrl(It.IsAny<AddressModel>()))
                .Returns(It.IsAny<string>())
                .Verifiable();
            _responseProvider.Setup(p => p.GetResponse(It.IsAny<string>(), It.IsAny<DataProviderType>()))
                .Returns(It.IsAny<DataResponse>())
                .Verifiable();
            _responseParserFactory.Setup(p => p.GetInstance<IResponseParser>(It.IsAny<DataProviderType>()))
                .Returns(_responseParser.Object)
                .Verifiable();
            _responseParser.Setup(p => p.ParseGeocodeAddressResponse(It.IsAny<DataResponse>(), It.IsAny<LevelOfConfidence>()))
                .Returns(new GeocodeAddressResponseModel())
                .Verifiable();

            var result = new GeocodeAddressDataProvider(_responseProvider.Object, _urlBuilderFactory.Object, _responseParserFactory.Object)
                .Geocode(It.IsAny<AddressModel>(), It.IsAny<DataProviderType>(), It.IsAny<LevelOfConfidence>());

            _urlBuilderFactory.Verify(p => p.GetInstance<IUrlBuilder>(It.IsAny<DataProviderType>(), FactoryEntity.UrlBuilder), Times.Once);
            _urlBuilder.Verify(p => p.BuildUrl(It.IsAny<AddressModel>()), Times.Once);
            _responseProvider.Verify(p => p.GetResponse(It.IsAny<string>(), It.IsAny<DataProviderType>()), Times.Once);
            _responseParserFactory.Verify(p => p.GetInstance<IResponseParser>(It.IsAny<DataProviderType>()), Times.Once);
            _responseParser.Verify(p => p.ParseGeocodeAddressResponse(It.IsAny<DataResponse>(), It.IsAny<LevelOfConfidence>()), Times.Once());
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GeocodeFormattedAddress()
        {
            _urlBuilderFactory.Setup(p => p.GetInstance<IUrlBuilder>(It.IsAny<DataProviderType>(), FactoryEntity.UrlBuilder))
                .Returns(_urlBuilder.Object)
                .Verifiable();
            _urlBuilder.Setup(p => p.BuildUrl(It.IsAny<string>()))
                .Returns(It.IsAny<string>())
                .Verifiable();
            _responseProvider.Setup(p => p.GetResponse(It.IsAny<string>(), It.IsAny<DataProviderType>()))
                .Returns(It.IsAny<DataResponse>())
                .Verifiable();
            _responseParserFactory.Setup(p => p.GetInstance<IResponseParser>(It.IsAny<DataProviderType>()))
                .Returns(_responseParser.Object)
                .Verifiable();
            _responseParser.Setup(p => p.ParseGeocodeAddressResponse(It.IsAny<DataResponse>(), It.IsAny<LevelOfConfidence>()))
                .Returns(new GeocodeAddressResponseModel())
                .Verifiable();

            var result = new GeocodeAddressDataProvider(_responseProvider.Object, _urlBuilderFactory.Object, _responseParserFactory.Object)
                .Geocode(It.IsAny<string>(), It.IsAny<DataProviderType>(), It.IsAny<LevelOfConfidence>());

            _urlBuilderFactory.Verify(p => p.GetInstance<IUrlBuilder>(It.IsAny<DataProviderType>(), FactoryEntity.UrlBuilder), Times.Once);
            _urlBuilder.Verify(p => p.BuildUrl(It.IsAny<string>()), Times.Once);
            _responseProvider.Verify(p => p.GetResponse(It.IsAny<string>(), It.IsAny<DataProviderType>()), Times.Once);
            _responseParserFactory.Verify(p => p.GetInstance<IResponseParser>(It.IsAny<DataProviderType>()), Times.Once);
            _responseParser.Verify(p => p.ParseGeocodeAddressResponse(It.IsAny<DataResponse>(), It.IsAny<LevelOfConfidence>()), Times.Once());
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ReverseGeocodeLocationModel()
        {
            _urlBuilderFactory.Setup(p => p.GetInstance<IUrlBuilder>(It.IsAny<DataProviderType>(), FactoryEntity.UrlBuilder))
                .Returns(_urlBuilder.Object)
                .Verifiable();
            _urlBuilder.Setup(p => p.BuildUrl(It.IsAny<LocationModel>()))
                .Returns(It.IsAny<string>())
                .Verifiable();
            _responseProvider.Setup(p => p.GetResponse(It.IsAny<string>(), It.IsAny<DataProviderType>()))
                .Returns(It.IsAny<DataResponse>())
                .Verifiable();
            _responseParserFactory.Setup(p => p.GetInstance<IResponseParser>(It.IsAny<DataProviderType>()))
                .Returns(_responseParser.Object)
                .Verifiable();
            _responseParser.Setup(p => p.ParseGeocodeAddressResponse(It.IsAny<DataResponse>(), It.IsAny<LevelOfConfidence>()))
                .Returns(new GeocodeAddressResponseModel())
                .Verifiable();

            var result = new GeocodeAddressDataProvider(_responseProvider.Object, _urlBuilderFactory.Object, _responseParserFactory.Object)
                .ReverseGeocode(It.IsAny<LocationModel>(), It.IsAny<DataProviderType>(), It.IsAny<LevelOfConfidence>());

            _urlBuilderFactory.Verify(p => p.GetInstance<IUrlBuilder>(It.IsAny<DataProviderType>(), FactoryEntity.UrlBuilder), Times.Once);
            _urlBuilder.Verify(p => p.BuildUrl(It.IsAny<LocationModel>()), Times.Once);
            _responseProvider.Verify(p => p.GetResponse(It.IsAny<string>(), It.IsAny<DataProviderType>()), Times.Once);
            _responseParserFactory.Verify(p => p.GetInstance<IResponseParser>(It.IsAny<DataProviderType>()), Times.Once);
            _responseParser.Verify(p => p.ParseGeocodeAddressResponse(It.IsAny<DataResponse>(), It.IsAny<LevelOfConfidence>()), Times.Once());
            Assert.IsNotNull(result);
        }
    }
}
