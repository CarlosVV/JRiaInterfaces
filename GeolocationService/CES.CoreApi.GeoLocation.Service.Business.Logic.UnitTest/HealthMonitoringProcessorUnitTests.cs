using System;
using System.Linq;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
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
            Assert.AreEqual("Core API services cache successfully cleaned up.", result.Message);
            Assert.IsTrue(result.IsOk);
        }

        [TestMethod]
        public void ClearCache_ExceptionRaisedAndExceptionMessageReturned()
        {
            
            _cacheProvider.Setup(p => p.ClearCache()).Throws(new ApplicationException("Test Exception")).Verifiable();

            var result = new HealthMonitoringProcessor(_cacheProvider.Object, _applicationRepository.Object).ClearCache();

            _cacheProvider.Verify(p => p.ClearCache(), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual("Core API services cache clean up failed. Exception details were logged out.", result.Message);
            Assert.IsFalse(result.IsOk);
        }

        [TestMethod]
        public void Ping_HappyPath()
        {
            var pingModel = new DatabasePingModel {Database = DatabaseType.Main, IsOk = true};
            _applicationRepository.Setup(p => p.Ping()).Returns(pingModel).Verifiable();

            var processor = new HealthMonitoringProcessor(_cacheProvider.Object, _applicationRepository.Object);
            
            var result = processor.Ping();
            _applicationRepository.Verify(p => p.Ping(), Times.Once);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Databases.ToList()[0].IsOk);
            Assert.AreEqual(DatabaseType.Main, result.Databases.ToList()[0].Database);
        }
    }
}
