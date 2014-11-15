using System.Collections.ObjectModel;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Models;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Providers;
using CES.CoreApi.GeoLocation.Service.UnitTestTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.UnitTest
{
    [TestClass]
    public class GooglePushPinParameterProviderUnitTests
    {
        [TestMethod]
        public void GetPushPinParameter_HappyPath()
        {
            const string expected = "&markers=color:green%7Clabel:1%7C35.4567,-100.5678&markers=color:red%7Clabel:2%7C36.4567,-101.5678";
            var result = new GooglePushPinParameterProvider().GetPushPinParameter(TestModelsProvider.GetPushPins());
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetPushPinParameter_PushPinsCollectionIsNull_EmptyStringReturned()
        {
            var result = new GooglePushPinParameterProvider().GetPushPinParameter(null);
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void GetPushPinParameter_PushPinsCollectionIsEmpty_EmptyStringReturned()
        {
            var result = new GooglePushPinParameterProvider().GetPushPinParameter(new Collection<PushPinModel>());
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void GetPushPinParameter_PushPinsIsUndefined_NoColorAddedToQueryString()
        {
            const string expected = "&markers=label:1%7C35.4567,-100.5678&markers=color:red%7Clabel:2%7C36.4567,-101.5678";
            var result = new GooglePushPinParameterProvider().GetPushPinParameter(TestModelsProvider.GetPushPins(pinColor: Color.Undefined));
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetPushPinParameter_LabelIsUndefined_NoLabelAddedToQueryString()
        {
            const string expected = "&markers=color:green%7C35.4567,-100.5678&markers=color:red%7Clabel:2%7C36.4567,-101.5678";
            var result = new GooglePushPinParameterProvider().GetPushPinParameter(TestModelsProvider.GetPushPins(label: null));
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetPushPinParameter_LabelLongerThanLimit_LabelCutAndAddedToQueryString()
        {
            const string expected = "&markers=color:green%7Clabel:S%7C35.4567,-100.5678&markers=color:red%7Clabel:2%7C36.4567,-101.5678";
            var result = new GooglePushPinParameterProvider().GetPushPinParameter(TestModelsProvider.GetPushPins(label: "Some long label"));
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }
    }
}
