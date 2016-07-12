using System.Runtime.Serialization;

namespace CES.CoreApi.GeoLocation.Service.Contract.Enumerations
{
    [DataContract]
    public enum MapImageFormat
    {
        [EnumMember]
        Undefined = 0,
        [EnumMember]
        Png = 1,
        [EnumMember]
        Jpeg = 2,
        [EnumMember]
        Gif = 3
    }
}