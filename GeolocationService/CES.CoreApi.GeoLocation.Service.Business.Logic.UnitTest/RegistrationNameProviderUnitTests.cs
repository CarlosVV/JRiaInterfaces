using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Providers;
using CES.CoreApi.GeoLocation.Service.UnitTestTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.UnitTest
{
    [TestClass]
    public class RegistrationNameProviderUnitTests
    {
        [TestMethod]
        public void GetRegistrationName_DataProviderTypeUndefined_ExceptionRaised()
        {
            ExceptionHelper.CheckException(
                () => new RegistrationNameProvider().GetRegistrationName(DataProviderType.Undefined, FactoryEntity.Parser),
                SubSystemError.GeneralInvalidParameterValue, "providerType", DataProviderType.Undefined);
        }

        [TestMethod]
        public void GetRegistrationName_FactoryEntityUndefined_ExceptionRaised()
        {
            ExceptionHelper.CheckException(
                () => new RegistrationNameProvider().GetRegistrationName(DataProviderType.Bing, FactoryEntity.Undefined),
                SubSystemError.GeneralInvalidParameterValue, "entity", FactoryEntity.Undefined);
        }

        [TestMethod]
        public void GetRegistrationName_ParserFactoryEntity_ExceptionRaised()
        {
            const string expected = "Bing_ResponseParser";
            var result = new RegistrationNameProvider().GetRegistrationName(DataProviderType.Bing, FactoryEntity.Parser);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetRegistrationName_UrlBuilderEntity_ExceptionRaised()
        {
            const string expected = "Bing_UrlBuilder";
            var result = new RegistrationNameProvider().GetRegistrationName(DataProviderType.Bing, FactoryEntity.UrlBuilder);

            Assert.AreEqual(expected, result);
        }
    }
}
