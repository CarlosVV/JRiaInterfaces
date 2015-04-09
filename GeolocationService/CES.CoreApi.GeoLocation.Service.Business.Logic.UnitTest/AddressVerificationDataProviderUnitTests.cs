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
    public class AddressVerificationDataProviderUnitTests
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
                () => new AddressVerificationDataProvider(_responseProvider.Object, null, _responseParserFactory.Object),
                SubSystemError.GeneralRequiredParameterIsUndefined, "urlBuilderFactory");
        }

        [TestMethod]
        public void Constructor_ResponseProviderIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(
                () => new AddressVerificationDataProvider(null, _urlBuilderFactory.Object, _responseParserFactory.Object),
                SubSystemError.GeneralRequiredParameterIsUndefined, "responseProvider");
        }

        [TestMethod]
        public void Constructor_HappyPath()
        {
            ExceptionHelper.CheckHappyPath(
                () => new AddressVerificationDataProvider(_responseProvider.Object, _urlBuilderFactory.Object, _responseParserFactory.Object));
        }

        #endregion

        [TestMethod]
        public void Verify_AddressModel_HappyPath()
        {
            _urlBuilderFactory.Setup(
                p => p.GetInstance<IUrlBuilder>(It.IsAny<DataProviderType>(), It.IsAny<FactoryEntity>()))
                .Returns(_urlBuilder.Object);

            _urlBuilder.Setup(p => p.BuildUrl(It.IsAny<AddressModel>()))
                .Returns(It.IsAny<string>())
                .Verifiable();

            _responseProvider.Setup(p => p.GetResponse(It.IsAny<string>(), It.IsAny<DataProviderType>()))
               .Returns(It.IsAny<DataResponse>())
               .Verifiable();
            
            _urlBuilderFactory.Setup(
                p => p.GetInstance<IResponseParser>(It.IsAny<DataProviderType>(), It.IsAny<FactoryEntity>()))
                .Returns(_responseParser.Object);
            
            _responseParser.Setup(p => p.ParseValidateAddressResponse(It.IsAny<DataResponse>(), It.IsAny<LevelOfConfidence>()))
                .Returns(new ValidateAddressResponseModel())
                .Verifiable();

            _responseParserFactory.Setup(p => p.GetInstance<IResponseParser>(It.IsAny<DataProviderType>(), It.IsAny<FactoryEntity>())).Returns(_responseParser.Object);

            var result = new AddressVerificationDataProvider(_responseProvider.Object, _urlBuilderFactory.Object, _responseParserFactory.Object)
                .Verify(It.IsAny<AddressModel>(), It.IsAny<DataProviderType>(), It.IsAny<LevelOfConfidence>());

            _urlBuilder.Verify(p => p.BuildUrl(It.IsAny<AddressModel>()), Times.Once);
            _responseProvider.Verify(p => p.GetResponse(It.IsAny<string>(), It.IsAny<DataProviderType>()), Times.Once);
            _responseParser.Verify(p => p.ParseValidateAddressResponse(It.IsAny<DataResponse>(), It.IsAny<LevelOfConfidence>()), Times.Once);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Verify_FormattedAddress_HappyPath()
        {
            _urlBuilderFactory.Setup(
                p => p.GetInstance<IUrlBuilder>(It.IsAny<DataProviderType>(), It.IsAny<FactoryEntity>()))
                .Returns(_urlBuilder.Object);

            _urlBuilder.Setup(p => p.BuildUrl(It.IsAny<string>()))
                .Returns(It.IsAny<string>())
                .Verifiable();

            _responseProvider.Setup(p => p.GetResponse(It.IsAny<string>(), It.IsAny<DataProviderType>()))
               .Returns(It.IsAny<DataResponse>())
               .Verifiable();

            _urlBuilderFactory.Setup(
                p => p.GetInstance<IResponseParser>(It.IsAny<DataProviderType>(), It.IsAny<FactoryEntity>()))
                .Returns(_responseParser.Object);

            _responseParser.Setup(p => p.ParseValidateAddressResponse(It.IsAny<DataResponse>(), It.IsAny<LevelOfConfidence>()))
                .Returns(new ValidateAddressResponseModel())
                .Verifiable();

            _responseParserFactory.Setup(p => p.GetInstance<IResponseParser>(It.IsAny<DataProviderType>(), It.IsAny<FactoryEntity>())).Returns(_responseParser.Object);

            var result = new AddressVerificationDataProvider(_responseProvider.Object, _urlBuilderFactory.Object, _responseParserFactory.Object).Verify(
                It.IsAny<string>(), It.IsAny<DataProviderType>(), It.IsAny<LevelOfConfidence>());

            _urlBuilder.Verify(p => p.BuildUrl(It.IsAny<string>()), Times.Once);
            _responseProvider.Verify(p => p.GetResponse(It.IsAny<string>(), It.IsAny<DataProviderType>()), Times.Once);
            _responseParser.Verify(p => p.ParseValidateAddressResponse(It.IsAny<DataResponse>(), It.IsAny<LevelOfConfidence>()), Times.Once);
            Assert.IsNotNull(result);
        }
    }
}
