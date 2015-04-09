using System.Collections.Generic;
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
    public class MappingDataProviderUnitTests
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
                () => new MappingDataProvider(null, _responseProvider.Object, _responseParserFactory.Object),
                SubSystemError.GeneralRequiredParameterIsUndefined, "entityFactory");
        }

        [TestMethod]
        public void Constructor_ResponseProviderIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(
                () => new MappingDataProvider(_urlBuilderFactory.Object, null, _responseParserFactory.Object),
                SubSystemError.GeneralRequiredParameterIsUndefined, "responseProvider");
        }

        [TestMethod]
        public void Constructor_HappyPath()
        {
            ExceptionHelper.CheckHappyPath(
                () => new MappingDataProvider(_urlBuilderFactory.Object, _responseProvider.Object, _responseParserFactory.Object));
        }

        #endregion

        [TestMethod]
        public void GetMap_HappyPath()
        {
            _urlBuilderFactory.Setup(
                p => p.GetInstance<IUrlBuilder>(It.IsAny<DataProviderType>(), It.IsAny<FactoryEntity>()))
                .Returns(_urlBuilder.Object)
                .Verifiable();

            _urlBuilder.Setup(
               p => p.BuildUrl(It.IsAny<LocationModel>(), It.IsAny<MapSizeModel>(), It.IsAny<MapOutputParametersModel>(), It.IsAny<ICollection<PushPinModel>>()))
               .Returns(It.IsAny<string>())
               .Verifiable();

            _responseProvider.Setup(p => p.GetBinaryResponse(It.IsAny<string>(), It.IsAny<DataProviderType>()))
                .Returns(It.IsAny<BinaryDataResponse>())
                .Verifiable();

            _responseParserFactory.Setup(
                p => p.GetInstance<IResponseParser>(It.IsAny<DataProviderType>(), It.IsAny<FactoryEntity>()))
                .Returns(_responseParser.Object)
                .Verifiable();

            _responseParser.Setup(p => p.ParseMapResponse(It.IsAny<BinaryDataResponse>()))
                .Returns(new GetMapResponseModel())
                .Verifiable();

            new MappingDataProvider(_urlBuilderFactory.Object, _responseProvider.Object, _responseParserFactory.Object).GetMap(It.IsAny<LocationModel>(),
                It.IsAny<MapSizeModel>(), It.IsAny<MapOutputParametersModel>(), It.IsAny<ICollection<PushPinModel>>(),
                It.IsAny<DataProviderType>());

            _urlBuilderFactory.Verify(p => p.GetInstance<IUrlBuilder>(It.IsAny<DataProviderType>(), It.IsAny<FactoryEntity>()), Times.Once);
            _urlBuilder.Verify(p => p.BuildUrl(It.IsAny<LocationModel>(), It.IsAny<MapSizeModel>(), It.IsAny<MapOutputParametersModel>(), It.IsAny<ICollection<PushPinModel>>()), Times.Once());
            _responseProvider.Verify(p => p.GetBinaryResponse(It.IsAny<string>(), It.IsAny<DataProviderType>()), Times.Once);
            _responseParserFactory.Verify(p => p.GetInstance<IResponseParser>(It.IsAny<DataProviderType>(), It.IsAny<FactoryEntity>()), Times.Once);
            _responseParser.Verify(p => p.ParseMapResponse(It.IsAny<BinaryDataResponse>()), Times.Once);
        }

    }
}
