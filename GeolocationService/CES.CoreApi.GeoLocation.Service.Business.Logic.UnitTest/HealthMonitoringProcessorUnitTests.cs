using System;
using System.Collections.ObjectModel;
using System.Linq;
using CES.CoreApi.Data.Enumerations;
using CES.CoreApi.Caching.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Foundation.Data.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Processors;
using CES.CoreApi.GeoLocation.Service.UnitTestTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CES.CoreApi.Data.Models;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.UnitTest
{
    [TestClass]
    public class HealthMonitoringProcessorUnitTests
    {
        private Mock<ICacheProvider> _cacheProvider;
        private Mock<IDatabasePingProvider> _databasePingProvider;

        [TestInitialize]
        public void Setup()
        {
            _cacheProvider = new Mock<ICacheProvider>();
            _databasePingProvider = new Mock<IDatabasePingProvider>();
        }
        #region Constructor tests

     //   [TestMethod]
     //   public void Constructor_CacheProviderIsNull_ExceptionRaised()
     //   {
     //       ExceptionHelper.CheckException(
     //           () => new HealthMonitoringProcessor(null, _databasePingProvider.Object),
				 //Common.Enumerations.SubSystemError.GeneralRequiredParameterIsUndefined, "cacheProvider");
     //   }

    //    [TestMethod]
    //    public void Constructor_DatabasePingProviderIsNull_ExceptionRaised()
    //    {
    //        ExceptionHelper.CheckException(
    //            () => new HealthMonitoringProcessor(_cacheProvider.Object),
				//Common.Enumerations.SubSystemError.GeneralRequiredParameterIsUndefined, "pingProvider");
    //    }

        [TestMethod]
        public void Constructor_HappyPath()
        {
            ExceptionHelper.CheckHappyPath(() => new HealthMonitoringProcessor(_cacheProvider.Object));
        }

        #endregion

        [TestMethod]
        public void ClearCache_HappyPath()
        {
            _cacheProvider.Setup(p => p.ClearCache()).Verifiable();

            var result = new HealthMonitoringProcessor(_cacheProvider.Object).ClearCache();
            
            _cacheProvider.Verify(p => p.ClearCache(), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual("Core API services cache successfully cleaned up.", result.Message);
            Assert.IsTrue(result.IsOk);
        }

        [TestMethod]
        public void ClearCache_ExceptionRaisedAndExceptionMessageReturned()
        {
            
            _cacheProvider.Setup(p => p.ClearCache()).Throws(new ApplicationException("Test Exception")).Verifiable();

            var result = new HealthMonitoringProcessor(_cacheProvider.Object).ClearCache();

            _cacheProvider.Verify(p => p.ClearCache(), Times.Once);
            Assert.IsNotNull(result);
            Assert.AreEqual("Core API services cache clean up failed. Exception details were logged out.", result.Message);
            Assert.IsFalse(result.IsOk);
        }

   //     [TestMethod]
   //     public void Ping_HappyPath()
   //     {
   //         var pingModel = new PingResponseModel
   //         {
   //             Databases = new Collection<DatabasePingModel>
   //             {
   //                 new DatabasePingModel {Database = DatabaseType.Main.ToString(), IsOk = true}
   //             }
   //         };
   //         _databasePingProvider.Setup(p => p.PingDatabases()).Returns(pingModel).Verifiable();

   //         var processor = new HealthMonitoringProcessor(_cacheProvider.Object);
            
   //         var result = processor.Ping() as PingResponseModel;
   //         _databasePingProvider.Verify(p => p.PingDatabases(), Times.Once);
   //         Assert.IsNotNull(result);
			//Assert.IsTrue(result.Databases.ToList()[0].IsOk);
			// Assert.AreEqual(DatabaseType.Main.ToString(), result.Databases.ToList()[0].Database);
   //     }
    }
}
