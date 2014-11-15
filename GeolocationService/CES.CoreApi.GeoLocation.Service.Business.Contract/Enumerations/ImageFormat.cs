using CES.CoreApi.GeoLocation.Service.Business.Contract.Attributes;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations
{
    public enum ImageFormat
    {
        Undefined = 0,
        [GoogleImageFormat("png")]
        Png = 1,
        [GoogleImageFormat("jpg")]
        Jpeg = 2,
        [GoogleImageFormat("gif")]
        Gif = 3
    }
}