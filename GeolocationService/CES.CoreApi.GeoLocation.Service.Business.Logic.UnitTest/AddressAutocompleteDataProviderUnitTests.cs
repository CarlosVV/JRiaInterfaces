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
    public class AddressAutocompleteDataProviderUnitTests
    {
        private Mock<IUrlBuilderFactory> _urlBuilderFactory;
        private Mock<IResponseParserFactory> _responseParserFactory;
        private Mock<IDataResponseProvider> _responseProvider;
        private Mock<IUrlBuilder> _urlBuilder;
        private Mock<IResponseParser> _responseParser;

        [TestInitialize]
        public void Setup()
        {
            _urlBuilderFactory = new Mock<IUrlBuilderFactory>();
            _responseParserFactory = new Mock<IResponseParserFactory>();
            _responseProvider = new Mock<IDataResponseProvider>();
            _urlBuilder = new Mock<IUrlBuilder>();
            _responseParser = new Mock<IResponseParser>();
        }

        #region Constructor tests

        [TestMethod]
        public void Constructor_UrlBuilderFactoryIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(
                () => new AddressAutocompleteDataProvider(null, _responseParserFactory.Object, _responseProvider.Object),
                SubSystemError.GeneralRequiredParameterIsUndefined, "urlBuilderFactory");
        }

        [TestMethod]
        public void Constructor_ResponseProviderIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(
                () => new AddressAutocompleteDataProvider(_urlBuilderFactory.Object, _responseParserFactory.Object, null),
                SubSystemError.GeneralRequiredParameterIsUndefined, "responseProvider");
        }

        [TestMethod]
        public void Constructor_HappyPath()
        {
            ExceptionHelper.CheckHappyPath(
                () => new AddressAutocompleteDataProvider(_urlBuilderFactory.Object, _responseParserFactory.Object, _responseProvider.Object));
        }

        #endregion

        [TestMethod]
        public void GetAddressHintList_HappyPath()
        {
            _urlBuilderFactory.Setup(
                p => p.GetInstance<IUrlBuilder>(It.IsAny<DataProviderType>(), It.IsAny<FactoryEntity>()))
                .Returns(_urlBuilder.Object);

            _responseParserFactory.Setup(
                p => p.GetInstance<IResponseParser>(It.IsAny<DataProviderType>(), It.IsAny<FactoryEntity>()))
                .Returns(_responseParser.Object);

            _urlBuilder.Setup(
                p => p.BuildUrl(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(It.IsAny<string>())
                .Verifiable();

            _responseProvider.Setup(p => p.GetResponse(It.IsAny<string>(), It.IsAny<DataProviderType>()))
                .Returns(It.IsAny<DataResponse>())
                .Verifiable();

            _responseParser.Setup(p => p.ParseAutocompleteAddressResponse(It.IsAny<DataResponse>(), It.IsAny<int>(), It.IsAny<LevelOfConfidence>(), It.IsAny<string>()))
                .Returns(new AutocompleteAddressResponseModel())
                .Verifiable();
                
            var result = new AddressAutocompleteDataProvider(_urlBuilderFactory.Object, _responseParserFactory.Object, _responseProvider.Object).GetAddressHintList(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DataProviderType>(), It.IsAny<LevelOfConfidence>());

            _urlBuilder.Verify(p => p.BuildUrl(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()), Times.Once);
            _responseProvider.Verify(p => p.GetResponse(It.IsAny<string>(), It.IsAny<DataProviderType>()), Times.Once);
            _responseParser.Verify(p => p.ParseAutocompleteAddressResponse(It.IsAny<DataResponse>(), It.IsAny<int>(), It.IsAny<LevelOfConfidence>(), It.IsAny<string>()), Times.Once);
            Assert.IsNotNull(result);
        }
    }
}
