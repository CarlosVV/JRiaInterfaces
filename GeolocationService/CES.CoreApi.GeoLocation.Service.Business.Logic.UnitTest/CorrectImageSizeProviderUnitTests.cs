using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Providers;
using CES.CoreApi.GeoLocation.Service.UnitTestTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.UnitTest
{
    [TestClass]
    public class CorrectImageSizeProviderUnitTests
    {
        private static class BingLimits
        {
            public const int MinWidth = 80;
            public const int MaxWidth = 900;
            public const int CorrectWidth = 100;
            public const int BiggerThanLimitWidth = 1000;
            public const int LowerThanLimitWidth = 1;
            public const int MinHeight = 80;
            public const int MaxHeight = 834;
            public const int CorrectHeight = 500;
            public const int BiggerThanLimitHeight = 900;
            public const int LowerThanLimitHeight = 1;
        }

        private static class GoogleLimits
        {
            public const int MinWidth = 80;
            public const int MaxWidth = 2048;
            public const int CorrectWidth = 100;
            public const int BiggerThanLimitWidth = 4000;
            public const int LowerThanLimitWidth = 1;
            public const int MinHeight = 80;
            public const int MaxHeight = 2048;
            public const int CorrectHeight = 500;
            public const int BiggerThanLimitHeight = 4000;
            public const int LowerThanLimitHeight = 1;
        }


        #region Bing limits

        [TestMethod]
        public void CorrectImageSizeProvider_BingCorrectWidth_RequestedValueReturned()
        {
            var result = new CorrectImageSizeProvider().GetCorrectImageSize(DataProviderType.Bing, ImageDimension.Width, BingLimits.CorrectWidth);
            Assert.AreEqual(BingLimits.CorrectWidth, result);
        }

        [TestMethod]
        public void CorrectImageSizeProvider_BingWidthBiggerThanLimit_MaxWidthValueReturned()
        {
            var result = new CorrectImageSizeProvider().GetCorrectImageSize(DataProviderType.Bing, ImageDimension.Width, BingLimits.BiggerThanLimitWidth);
            Assert.AreEqual(BingLimits.MaxWidth, result);
        }

        [TestMethod]
        public void CorrectImageSizeProvider_BingWidthLowerThanLimit_MinWidthValueReturned()
        {
            var result = new CorrectImageSizeProvider().GetCorrectImageSize(DataProviderType.Bing, ImageDimension.Width, BingLimits.LowerThanLimitWidth);
            Assert.AreEqual(BingLimits.MinWidth, result);
        }

        [TestMethod]
        public void CorrectImageSizeProvider_BingCorrectHeight_RequestedValueReturned()
        {
            var result = new CorrectImageSizeProvider().GetCorrectImageSize(DataProviderType.Bing, ImageDimension.Height, BingLimits.CorrectHeight);
            Assert.AreEqual(BingLimits.CorrectHeight, result);
        }

        [TestMethod]
        public void CorrectImageSizeProvider_BingHeightBiggerThanLimit_MaxWidthValueReturned()
        {
            var result = new CorrectImageSizeProvider().GetCorrectImageSize(DataProviderType.Bing, ImageDimension.Height, BingLimits.BiggerThanLimitHeight);
            Assert.AreEqual(BingLimits.MaxHeight, result);
        }

        [TestMethod]
        public void CorrectImageSizeProvider_BingHeightLowerThanLimit_MinWidthValueReturned()
        {
            var result = new CorrectImageSizeProvider().GetCorrectImageSize(DataProviderType.Bing, ImageDimension.Height, BingLimits.LowerThanLimitHeight);
            Assert.AreEqual(BingLimits.MinHeight, result);
        }

        #endregion

        #region Google limits

        [TestMethod]
        public void CorrectImageSizeProvider_GoogleCorrectWidth_RequestedValueReturned()
        {
            var result = new CorrectImageSizeProvider().GetCorrectImageSize(DataProviderType.Google, ImageDimension.Width, GoogleLimits.CorrectWidth);
            Assert.AreEqual(GoogleLimits.CorrectWidth, result);
        }

        [TestMethod]
        public void CorrectImageSizeProvider_GoogleWidthBiggerThanLimit_MaxWidthValueReturned()
        {
            var result = new CorrectImageSizeProvider().GetCorrectImageSize(DataProviderType.Google, ImageDimension.Width, GoogleLimits.BiggerThanLimitWidth);
            Assert.AreEqual(GoogleLimits.MaxWidth, result);
        }

        [TestMethod]
        public void CorrectImageSizeProvider_GoogleWidthLowerThanLimit_MinWidthValueReturned()
        {
            var result = new CorrectImageSizeProvider().GetCorrectImageSize(DataProviderType.Google, ImageDimension.Width, GoogleLimits.LowerThanLimitWidth);
            Assert.AreEqual(GoogleLimits.MinWidth, result);
        }

        [TestMethod]
        public void CorrectImageSizeProvider_GoogleCorrectHeight_RequestedValueReturned()
        {
            var result = new CorrectImageSizeProvider().GetCorrectImageSize(DataProviderType.Google, ImageDimension.Height, GoogleLimits.CorrectHeight);
            Assert.AreEqual(GoogleLimits.CorrectHeight, result);
        }

        [TestMethod]
        public void CorrectImageSizeProvider_GoogleHeightBiggerThanLimit_MaxWidthValueReturned()
        {
            var result = new CorrectImageSizeProvider().GetCorrectImageSize(DataProviderType.Google, ImageDimension.Height, GoogleLimits.BiggerThanLimitHeight);
            Assert.AreEqual(GoogleLimits.MaxHeight, result);
        }

        [TestMethod]
        public void CorrectImageSizeProvider_GoogleHeightLowerThanLimit_MinWidthValueReturned()
        {
            var result = new CorrectImageSizeProvider().GetCorrectImageSize(DataProviderType.Google, ImageDimension.Height, GoogleLimits.LowerThanLimitHeight);
            Assert.AreEqual(GoogleLimits.MinHeight, result);
        }

        #endregion

        [TestMethod]
        public void GetCorrectImageSize_UnsuppoertedDataProvider_ExceptionRaised()
        {
            ExceptionHelper.CheckException(
                () => new CorrectImageSizeProvider().GetCorrectImageSize(DataProviderType.MelissaData, It.IsAny<ImageDimension>(), It.IsAny<int>()),
                SubSystemError.GeolocationMappingIsNotSupported, DataProviderType.MelissaData);
        }
    }
}
