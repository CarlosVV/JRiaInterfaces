using System.Runtime.Serialization;

namespace CES.CoreApi.GeoLocation.Api.ViewModels
{
    [DataContract]
    public enum PinColor
    {
        [EnumMember]
        Undefined = 0,
        [EnumMember]
        Black = 1,
        [EnumMember]
        Brown = 2,
        [EnumMember]
        Green = 3,
        [EnumMember]
        Purple = 4,
        [EnumMember]
        Yellow = 5,
        [EnumMember]
        Blue = 6,
        [EnumMember]
        Gray = 7,
        [EnumMember]
        Orange = 8,
        [EnumMember]
        Red = 9,
        [EnumMember]
        White = 10
    }
}
