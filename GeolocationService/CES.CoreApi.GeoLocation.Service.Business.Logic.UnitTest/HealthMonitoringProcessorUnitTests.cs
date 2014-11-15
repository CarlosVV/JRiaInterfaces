using System;
using CES.CoreApi.Caching.Interfaces;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Processors;
using CES.CoreApi.GeoLocation.Service.UnitTestTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.UnitTest
{
    [TestClass]
    public class HealthMonitoringProcessorUnitTests
    {
        private Mock<ICacheProvider> _cacheProvider;
        private Mock<IApplicationRepository> _applicationRepository;

        [TestInitialize]
        public void Setup()
        {
            _cacheProvider = new Mock<ICacheProvider>();
            _applicationRepository = new Mock<IApplicationRepository>();
        }
        #region Constructor tests

        [TestMethod]
        public void Constructor_CacheProviderIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(
                () => new HealthMonitoringProcessor(null, _applicationRepository.Object),
                 SubSystemError.GeneralRequiredParameterIsUndefined, "cacheProvider");
        }

        [TestMethod]
        public void Constructor_GeocodeAddressDataProviderIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(
                () => new HealthMonitoringProcessor(_cacheProvider.Object, null),
                SubSystemError.GeneralRequiredParameterIsUndefined, "applicationRepository");
        }

        [TestMethod]
        public void Constructor_HappyPath()
        {
            ExceptionHelper.CheckHappyPath(() => new HealthMonitoringProcessor(_cacheProvider.Object, _applicationRepository.Object));
        }

        #endregion

        [TestMethod]
        public void ClearCache_HappyPath()
        {
            _cacheProvider.Setup(p => p.ClearCache()).Verifiable();

            var result = new HealthMonitoringProcessor(_cacheProvider.Object, _applicationRepository.Object).ClearCache();
            
            _cacheProvider.Verify(p => p.ClearCache(), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual("Core API Geolocation service cache successfully cleaned up.", result.Message);
        }

        [TestMethod]
        public void ClearCache_ExceptionRaisedAndExceptionMessageReturned()
        {
            
            _cacheProvider.Setup(p => p.ClearCache()).Throws(new ApplicationException("Test Exception")).Verifiable();

            var result = new HealthMonitoringProcessor(_cacheProvider.Object, _applicationRepository.Object).ClearCache();

            _cacheProvider.Verify(p => p.ClearCache(), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual("Test Exception", result.Message);
        }

        [TestMethod]
        public void Ping_HappyPath()
        {
            _applicationRepository.Setup(p => p.Ping()).Verifiable();

            var result = new HealthMonitoringProcessor(_cacheProvider.Object, _applicationRepository.Object).Ping();

            _applicationRepository.Verify(p => p.Ping(), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual("OK", result.MainDatabaseStatus);
        }

        [TestMethod]
        public void Ping_ExceptionRaisedAndExceptionMessageReturned()
        {
            _applicationRepository.Setup(p => p.Ping()).Throws(new ApplicationException("Test Exception")).Verifiable();

            var result = new HealthMonitoringProcessor(_cacheProvider.Object, _applicationRepository.Object).Ping();

            _applicationRepository.Verify(p => p.Ping(), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual("Test Exception", result.MainDatabaseStatus);
        }
    }
}
