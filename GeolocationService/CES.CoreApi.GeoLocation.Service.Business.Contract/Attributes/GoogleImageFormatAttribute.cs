using System;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class GoogleImageFormatAttribute: Attribute
    {
        public string ImageFormat { get; private set; }

        public GoogleImageFormatAttribute(string imageFormat)
        {
            ImageFormat = imageFormat;
        }
    }
}
