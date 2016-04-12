//using System;
//using CES.CoreApi.Common.Enumerations;
//using CES.CoreApi.Common.Interfaces;
//using CES.CoreApi.Common.Models;
//using CES.CoreApi.Foundation.Contract.Interfaces;
//using CES.CoreApi.GeoLocation.Service.Business.Contract.Configuration;
//using CES.CoreApi.GeoLocation.Service.Business.Logic.Constants;
//using CES.CoreApi.GeoLocation.Service.Business.Logic.Providers;
//using CES.CoreApi.GeoLocation.Service.UnitTestTools;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;

//namespace CES.CoreApi.GeoLocation.Service.Business.Logic.UnitTest
//{
//    [TestClass]
//    public class CountryConfigurationProviderUnitTests
//    {
//        private Mock<IConfigurationProvider> _configurationProvider;
//        private Mock<IIdentityManager> _identityManager;
//        private const string FoundCountry = "US";
//        private const string NotFoundCountry = "USA";

//        [TestInitialize]
//        public void Setup()
//        {
//            _configurationProvider = new Mock<IConfigurationProvider>();
//            _identityManager = new Mock<IIdentityManager>();
//        }

//        [TestMethod]
//        public void Constructor_ConfigurationProviderIsNull_ExceptionRaised()
//        {
//            ExceptionHelper.CheckException(() => new CountryConfigurationProvider(null, null),
//               SubSystemError.GeneralRequiredParameterIsUndefined, "configurationProvider");
//        }

//        [TestMethod]
//        public void Constructor_HappyPath()
//        {
//            ExceptionHelper.CheckHappyPath(() => new CountryConfigurationProvider(_configurationProvider.Object, _identityManager.Object));
//        }

//        [TestMethod]
//        public void GetProviderConfigurationByCountry_CountryIsFound_HappyPath()
//        {
//            var application = new Application(1, "Test", true);
//            var header = new ServiceCallHeaderParameters(1,"Test",DateTime.Now, "123456", "ABC12345", "transactionID", Guid.NewGuid().ToString());
//            var applicationIdentity = new ClientApplicationIdentity(application,header);

//            _configurationProvider.Setup(p => p.ReadFromJson<DataProviderServiceConfiguration>(
//                ConfigurationConstants.DataProviderServiceConfiguration))
//                .Returns(TestModelsProvider.GetCountryConfigurations());
//            _identityManager.Setup(p => p.GetClientApplicationIdentity()).Returns(applicationIdentity);
            
//            var result = new CountryConfigurationProvider(_configurationProvider.Object, _identityManager.Object).GetProviderConfigurationByCountry(FoundCountry);

//            Assert.IsNotNull(result);
//            Assert.AreEqual(FoundCountry, result.CountryCode);
//        }

//        [TestMethod]
//        public void GetProviderConfigurationByCountry_DefaultCountryUsed_HappyPath()
//        {
//            var application = new Application(1, "Test", true);
//            var header = new ServiceCallHeaderParameters(1, "Test", DateTime.Now, "123456", "ABC12345", "transactionID", Guid.NewGuid().ToString());
//            var applicationIdentity = new ClientApplicationIdentity(application, header);

//            _configurationProvider.Setup(p => p.ReadFromJson<DataProviderServiceConfiguration>(
//                ConfigurationConstants.DataProviderServiceConfiguration))
//                .Returns(TestModelsProvider.GetCountryConfigurations());
//            _identityManager.Setup(p => p.GetClientApplicationIdentity()).Returns(applicationIdentity);

//            var result = new CountryConfigurationProvider(_configurationProvider.Object, _identityManager.Object).GetProviderConfigurationByCountry(NotFoundCountry);

//            Assert.IsNotNull(result);
//            Assert.AreEqual("Default", result.CountryCode, true);
//        }

//    }
//}
