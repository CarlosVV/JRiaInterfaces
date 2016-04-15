using System.Net.Http;
using System.Threading.Tasks;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Providers;
using CES.CoreApi.GeoLocation.Service.UnitTestTools;
using CES.CoreApi.Logging.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.UnitTest
{
    [TestClass]
    public class DataResponseProviderUnitTests
    {
        private Mock<IHttpClientProxy> _httpClientProxy;
        private Mock<IRequestHeadersProvider> _headerProvider;
        private Mock<HttpClient> _httpClient;
        private Mock<HttpResponseMessage> _httpResponseMessage;
        private Mock<ILogMonitorFactory> _logMonitorFactory;
        private Mock<Task<HttpResponseMessage>> _getAsyncResult;
        private const string ResponseXml = "<someXml></someXml>";
        private const string TestUrl = "http://testurl.com";

        [TestInitialize]
        public void Setup()
        {
            _httpClientProxy = new Mock<IHttpClientProxy>();
            _httpClient = new Mock<HttpClient>();
            _httpResponseMessage = new Mock<HttpResponseMessage>();
            _logMonitorFactory = new Mock<ILogMonitorFactory>();
            _getAsyncResult = new Mock<Task<HttpResponseMessage>>();
            _headerProvider = new Mock<IRequestHeadersProvider>();
        }

        [TestMethod]
        public void Constructor_HttpClientProxyIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(() => new DataResponseProvider(null),
               SubSystemError.GeneralRequiredParameterIsUndefined, "httpClientProxy");
        }
		
        [TestMethod]
        public void Constructor_HappyPath()
        {
            ExceptionHelper.CheckHappyPath(() => new DataResponseProvider(_httpClientProxy.Object));
        }

        [TestMethod]
        public void GetResponse_UrlIsNullOrEmpty_ExceptionRaised()
        {
            ExceptionHelper.CheckException(() => new DataResponseProvider(_httpClientProxy.Object).GetResponse(string.Empty, It.IsAny<DataProviderType>()),
              SubSystemError.GeneralRequiredParameterIsUndefined, "requestUrl");
        }

        [TestMethod]
        public void GetBinaryResponse_UrlIsNullOrEmpty_ExceptionRaised()
        {
            ExceptionHelper.CheckException(() => new DataResponseProvider(_httpClientProxy.Object).GetBinaryResponse(string.Empty, It.IsAny<DataProviderType>()),
              SubSystemError.GeneralRequiredParameterIsUndefined, "requestUrl");
        }

        //[TestMethod]
        //public void GetResponse_HappyPath()
        //{
        //    _traceLogManager.Setup(p => p.GetMonitorInstance())
        //        .Returns(_logMonitorFactory.Object)
        //        .Verifiable();
        //    _httpClientProxy.Setup(p => p.GetHttpClient())
        //        .Returns(_httpClient.Object)
        //        .Verifiable();
        //    _logMonitorFactory.Setup(p=>p.Start(It.IsAny<string>(), It.IsAny<IDictionary<string, object>>()))
        //        .Verifiable();
        //    _httpClient.Setup(p => p.GetAsync(TestUrl)).Returns(_getAsyncResult.Object);
        //    //_httpResponseMessage.Setup(p => p.Content.ReadAsStringAsync().Result).Returns(ResponseXml);
        //    //_httpResponseMessage.Setup(p => p.StatusCode).Returns(HttpStatusCode.OK);
        //    //_httpResponseMessage.Setup(p => p.Content.ReadAsStringAsync().Result).Returns(ResponseXml);

        //    var result = new DataResponseProvider(_httpClientProxy.Object, _traceLogManager.Object).GetResponse(TestUrl);

        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        //    Assert.AreEqual(ResponseXml, result.RawResponse);
        //    Assert.IsTrue(result.IsSuccessStatusCode);
        //}
    }
}
