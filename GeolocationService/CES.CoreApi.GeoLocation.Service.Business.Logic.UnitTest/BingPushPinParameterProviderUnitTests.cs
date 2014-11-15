using System.Collections.ObjectModel;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Providers;
using CES.CoreApi.GeoLocation.Service.UnitTestTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.UnitTest
{
    [TestClass]
    public class BingPushPinParameterProviderUnitTests
    {
        [TestMethod]
        public void GetPushPinParameter_HappyPath()
        {
            const string expected = "&pp=35.4567,-100.5678;1;1&pp=36.4567,-101.5678;2;2";
            var result = new BingPushPinParameterProvider().GetPushPinParameter(TestModelsProvider.GetPushPins());
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetPushPinParameter_PushPinsCollectionIsNull_EmptyStringReturned()
        {
            var result = new BingPushPinParameterProvider().GetPushPinParameter(null);
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void GetPushPinParameter_PushPinsCollectionIsEmpty_EmptyStringReturned()
        {
            var result = new BingPushPinParameterProvider().GetPushPinParameter(new Collection<PushPinModel>());
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void GetPushPinParameter_PushPinsIconStyleValueLessThanLowerLimit_DefaultIconStyleValueUsed()
        {
            const string expected = "&pp=35.4567,-100.5678;1;1&pp=36.4567,-101.5678;2;2";
            var result = new BingPushPinParameterProvider().GetPushPinParameter(TestModelsProvider.GetPushPins(-10));
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetPushPinParameter_PushPinsIconStyleValueBiggerThanUpperLimit_DefaultIconStyleValueUsed()
        {
            const string expected = "&pp=35.4567,-100.5678;1;1&pp=36.4567,-101.5678;2;2";
            var result = new BingPushPinParameterProvider().GetPushPinParameter(TestModelsProvider.GetPushPins(115));
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetPushPinParameter_PushPinsIconStyleValueIsNull_DefaultIconStyleValueUsed()
        {
            const string expected = "&pp=35.4567,-100.5678;1;1&pp=36.4567,-101.5678;2;2";
            var result = new BingPushPinParameterProvider().GetPushPinParameter(TestModelsProvider.GetPushPins(null));
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetPushPinParameter_LabelIsNull_EmptyLabelParameterReturned()
        {
            const string expected = "&pp=35.4567,-100.5678;1;&pp=36.4567,-101.5678;2;2";
            var result = new BingPushPinParameterProvider().GetPushPinParameter(TestModelsProvider.GetPushPins(null, null));
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetPushPinParameter_LabelIsLongerThanMaxLimit_EmptyLabelParameterReturned()
        {
            const string expected = "&pp=35.4567,-100.5678;1;lon&pp=36.4567,-101.5678;2;2";
            var result = new BingPushPinParameterProvider().GetPushPinParameter(TestModelsProvider.GetPushPins(null, "long label"));
            Assert.AreEqual(expected, result);
        }
    }
}
