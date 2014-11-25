using System;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Factories;
using CES.CoreApi.GeoLocation.Service.UnitTestTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.UnitTest
{
    [TestClass]
    public class EntityFactoryUnitTests
    {
        private Mock<IIocContainer> _container;
        private Mock<IRegistrationNameProvider> _registrationNameProvider;

        [TestInitialize]
        public void Setup()
        {
            _container = new Mock<IIocContainer>();
            _registrationNameProvider = new Mock<IRegistrationNameProvider>();

            _container.Setup(p => p.Resolve<IRegistrationNameProvider>()).Returns(_registrationNameProvider.Object);
        }

        [TestMethod]
        public void Constructor_IoCContainerIsNull_ExceptionRaised()
        {
            ExceptionHelper.CheckException(() => new EntityFactory(null),
                SubSystemError.GeneralRequiredParameterIsUndefined, "container");
        }

        [TestMethod]
        public void GetInstance_HappyPath()
        {
            const string testName = "TestName";
            var expected = new Mock<IUrlBuilder>();

            _registrationNameProvider.Setup(p => p.GetRegistrationName(DataProviderType.Bing, FactoryEntity.UrlBuilder)).Returns(testName);
            _container.Setup(p => p.Resolve<IUrlBuilder>(testName)).Returns(expected.Object);

            var factory = new EntityFactory(_container.Object);
            var result = factory.GetInstance<IUrlBuilder>(DataProviderType.Bing, FactoryEntity.UrlBuilder);

            Assert.AreEqual(expected.Object, result);
        }

        [TestMethod]
        public void GetInstance_ResolveFailed_ExceptionRaised()
        {
            const string testName = "TestName";

            _registrationNameProvider.Setup(p => p.GetRegistrationName(DataProviderType.Bing, FactoryEntity.UrlBuilder)).Returns(testName);
            _container.Setup(p => p.Resolve<IUrlBuilder>(testName)).Throws<ArgumentNullException>();
            var factory = new EntityFactory(_container.Object);

            ExceptionHelper.CheckException(() => factory.GetInstance<IUrlBuilder>(DataProviderType.Bing, FactoryEntity.UrlBuilder),
                SubSystemError.GeolocationFactoryEntityNotRegisteredInContainer, DataProviderType.Bing,
                        FactoryEntity.UrlBuilder);
        }
    }
}
