
using CES.CoreApi.GeoLocation.Enumerations;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using System;

namespace CES.CoreApi.GeoLocation.Service.Business.Logic.Providers
{
    public class CorrectImageSizeProvider : ICorrectImageSizeProvider
    {
        #region Core

        private static class BingLimits
        {
            public const int MinWidth = 80;
            public const int MaxWidth = 900;
            public const int MinHeight = 80;
            public const int MaxHeight = 834;
        }

        private static class GoogleLimits
        {
            public const int MinWidth = 80;
            public const int MaxWidth = 2048;
            public const int MinHeight = 80;
            public const int MaxHeight = 2048;
        }

        #endregion

        #region Public methods

        public int GetCorrectImageSize(DataProviderType providerType, ImageDimension dimension, int requestValue)
        {
            switch (providerType)
            {
                case DataProviderType.Bing:
                    return GetCorrectBingSize(dimension, requestValue);

                case DataProviderType.Google:
                    return GetCorrectGoogleSize(dimension, requestValue);

                default:
                    throw new Exception("Invalid provider");
            }
        }

        #endregion

        #region private methods

        private static int GetCorrectBingSize(ImageDimension dimension, int requestValue)
        {
            var minValue = dimension == ImageDimension.Width ? BingLimits.MinWidth : BingLimits.MinHeight;
            var maxValue = dimension == ImageDimension.Height ? BingLimits.MaxHeight : BingLimits.MaxWidth;

            return GetCorrectValue(minValue, maxValue, requestValue);
        }

        private static int GetCorrectGoogleSize(ImageDimension dimension, int requestValue)
        {
            var minValue = dimension == ImageDimension.Width ? GoogleLimits.MinWidth : GoogleLimits.MinHeight;
            var maxValue = dimension == ImageDimension.Height ? GoogleLimits.MaxHeight : GoogleLimits.MaxWidth;

            return GetCorrectValue(minValue, maxValue, requestValue);
        }

        private static int GetCorrectValue(int minValue, int maxValue, int requestValue)
        {
            if (requestValue < minValue)
                return minValue;

            return requestValue > maxValue ? maxValue : requestValue;
        }

        #endregion
    }
}
