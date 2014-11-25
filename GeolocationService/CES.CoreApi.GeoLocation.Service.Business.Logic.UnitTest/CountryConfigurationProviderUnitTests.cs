using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Configuration;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Constants;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Providers;
using CES.CoreApi.GeoLocation.Service.UnitTestTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.UnitTest
{
    [TestClass]
    public class CountryConfigurationProviderUnitTests
    {
        private Mock<IConfigurationProvider> _configurationProvider;
        private const string FoundCountry = "US";
        private const string NotFoundCountry = "USA";

        [TestInitialize]
        public void Setup()
        {
            _configurationProvider = new Mock<IConfigurationProvider>();
        }

        [TestMethod]
        public void Constructor_ConfigurationProviderIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(() => new CountryConfigurationProvider(null),
               SubSystemError.GeneralRequiredParameterIsUndefined, "configurationProvider");
        }

        [TestMethod]
        public void Constructor_HappyPath()
        {
            ExceptionHelper.CheckHappyPath(() => new CountryConfigurationProvider(_configurationProvider.Object));
        }

        [TestMethod]
        public void GetProviderConfigurationByCountry_CountryIsFound_HappyPath()
        {
            _configurationProvider.Setup(p => p.ReadFromJson<DataProviderServiceConfiguration>(
                ConfigurationConstants.DataProviderServiceConfiguration))
                .Returns(TestModelsProvider.GetCountryConfigurations());
            
            var result = new CountryConfigurationProvider(_configurationProvider.Object).GetProviderConfigurationByCountry(FoundCountry);

            Assert.IsNotNull(result);
            Assert.AreEqual(FoundCountry, result.CountryCode);
        }

        [TestMethod]
        public void GetProviderConfigurationByCountry_DefaultCountryUsed_HappyPath()
        {
            _configurationProvider.Setup(p => p.ReadFromJson<DataProviderServiceConfiguration>(
                ConfigurationConstants.DataProviderServiceConfiguration))
                .Returns(TestModelsProvider.GetCountryConfigurations());

            var result = new CountryConfigurationProvider(_configurationProvider.Object).GetProviderConfigurationByCountry(NotFoundCountry);

            Assert.IsNotNull(result);
            Assert.AreEqual("Default", result.CountryCode, true);
        }

    }
}
