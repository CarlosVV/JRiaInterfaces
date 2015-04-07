using System.Collections.ObjectModel;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Constants;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Processors;
using CES.CoreApi.GeoLocation.Service.UnitTestTools;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.UnitTest
{
    [TestClass]
    public class ClientSideSupportServiceProcessorUnitTests
    {
        private Mock<IHostApplicationProvider> _hostApplicationProvider;
        private Mock<ITraceLogMonitor> _traceLogMonitor;
        private Mock<IApplication> _application;
        private Mock<TraceLogDataContainer> _traceLogDataContainer;
        private Mock<ICountryConfigurationProvider> _countryConfigurationProvider;
        private const string BingKey = "Bing Key";
        private const string GoogleKey = "Google Key";
        private const string MelissaKey = "MelissaData Key";

        [TestInitialize]
        public void Setup()
        {
            _hostApplicationProvider = new Mock<IHostApplicationProvider>();
            _traceLogMonitor = new Mock<ITraceLogMonitor>();
            _application = new Mock<IApplication>();
            _traceLogDataContainer = new Mock<TraceLogDataContainer>();
            _countryConfigurationProvider = new Mock<ICountryConfigurationProvider>();
        }

        #region Constructor tests

        [TestMethod]
        public void Constructor_HappyPath_NoExceptionRaised()
        {
            ExceptionHelper.CheckHappyPath(
                () => new ClientSideSupportServiceProcessor(_countryConfigurationProvider.Object, _hostApplicationProvider.Object, _traceLogMonitor.Object));
        }

        [TestMethod]
        public void Constructor_CountryConfigurationProviderIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(
                () => new ClientSideSupportServiceProcessor(null, _hostApplicationProvider.Object, _traceLogMonitor.Object),
                 SubSystemError.GeneralRequiredParameterIsUndefined, "configurationProvider");
        }

        [TestMethod]
        public void Constructor_HostApplicationProviderIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(
                () => new ClientSideSupportServiceProcessor(_countryConfigurationProvider.Object, null, _traceLogMonitor.Object),
                SubSystemError.GeneralRequiredParameterIsUndefined, "hostApplicationProvider");
        }

        [TestMethod]
        public void Constructor_LogManagerIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(
                () => new ClientSideSupportServiceProcessor(_countryConfigurationProvider.Object, _hostApplicationProvider.Object, null),
                SubSystemError.GeneralRequiredParameterIsUndefined, "logManager");
        }

        #endregion

        [TestMethod]
        public void LogEvent_HappyPath()
        {
            _traceLogMonitor.Setup(p => p.Start()).Verifiable();
            _traceLogMonitor.Setup(p => p.Stop()).Verifiable();
            _traceLogMonitor.SetupGet(p => p.DataContainer).Returns(_traceLogDataContainer.Object).Verifiable();

            new ClientSideSupportServiceProcessor(_countryConfigurationProvider.Object, _hostApplicationProvider.Object, _traceLogMonitor.Object).LogEvent(DataProviderType.Google, string.Empty);

            _traceLogMonitor.Verify(p => p.DataContainer, Times.Exactly(2));
            _traceLogMonitor.Verify(p => p.Start(), Times.Once);
            _traceLogMonitor.Verify(p => p.Stop(), Times.Once);
        }

        [TestMethod]
        public void GetProviderKey_Google_HappyPath()
        {
            _hostApplicationProvider.Setup(p=>p.GetApplication()).Returns(_application.Object).Verifiable();
            _application.SetupGet(p => p.Configuration).Returns(GetConfiguration()).Verifiable();

            var result = new ClientSideSupportServiceProcessor(_countryConfigurationProvider.Object, _hostApplicationProvider.Object, _traceLogMonitor.Object).GetProviderKey(
                DataProviderType.Google);

            Assert.AreEqual(GoogleKey, result.ProviderKey);
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void GetProviderKey_Melissa_HappyPath()
        {
            _hostApplicationProvider.Setup(p => p.GetApplication()).Returns(_application.Object).Verifiable();
            _application.SetupGet(p => p.Configuration).Returns(GetConfiguration()).Verifiable();

            var result = new ClientSideSupportServiceProcessor(_countryConfigurationProvider.Object, _hostApplicationProvider.Object, _traceLogMonitor.Object).GetProviderKey(
                DataProviderType.MelissaData);

            Assert.AreEqual(MelissaKey, result.ProviderKey);
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void GetProviderKey_Bing_HappyPath()
        {
            _hostApplicationProvider.Setup(p => p.GetApplication()).Returns(_application.Object).Verifiable();
            _application.SetupGet(p => p.Configuration).Returns(GetConfiguration()).Verifiable();

            var result = new ClientSideSupportServiceProcessor(_countryConfigurationProvider.Object, _hostApplicationProvider.Object, _traceLogMonitor.Object).GetProviderKey(
                DataProviderType.Bing);

            Assert.AreEqual(BingKey, result.ProviderKey);
            Assert.IsTrue(result.IsValid);
        }

        [TestMethod]
        public void GetProviderKey_GoogleKeyNotFound_InValidResultReturned()
        {
            _hostApplicationProvider.Setup(p => p.GetApplication()).Returns(_application.Object).Verifiable();
            _application.SetupGet(p => p.Configuration).Returns(GetConfiguration(false)).Verifiable();

            var result = new ClientSideSupportServiceProcessor(_countryConfigurationProvider.Object, _hostApplicationProvider.Object, _traceLogMonitor.Object).GetProviderKey(
                DataProviderType.Google);

            Assert.IsNull(result.ProviderKey);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public void GetProviderKey_UnknownProvider_ExceptionRaised()
        {
            _hostApplicationProvider.Setup(p => p.GetApplication()).Returns(_application.Object).Verifiable();
            _application.SetupGet(p => p.Configuration).Returns(GetConfiguration(false)).Verifiable();

            ExceptionHelper.CheckException(
                () => new ClientSideSupportServiceProcessor(_countryConfigurationProvider.Object, _hostApplicationProvider.Object, _traceLogMonitor.Object).GetProviderKey(DataProviderType.Undefined),
                SubSystemError.GeneralInvalidParameterValue, "providerType", DataProviderType.Undefined);
        }

        private static Collection<ApplicationConfiguration> GetConfiguration(bool addGoogle = true)
        {
            var config = new Collection<ApplicationConfiguration>
            {
                new ApplicationConfiguration(ConfigurationConstants.BingLicenseKeyConfigurationName, BingKey),
                new ApplicationConfiguration(ConfigurationConstants.MelissaDataLicenseKeyConfigurationName, MelissaKey)
            };

            if (addGoogle)
                config.Add(new ApplicationConfiguration(ConfigurationConstants.GoogleLicenseKeyConfigurationName,
                    GoogleKey));

            return config;
        }
    }
}
