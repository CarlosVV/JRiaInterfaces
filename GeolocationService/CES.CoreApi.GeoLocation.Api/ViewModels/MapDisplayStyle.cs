using System.Runtime.Serialization;

namespace CES.CoreApi.GeoLocation.Api.ViewModels
{
    [DataContract]
    public enum MapDisplayStyle
    {
        [EnumMember]
        Undefined = 0,

        [EnumMember]
        Road = 1,
        
        [EnumMember]
        Aerial = 2,

        [EnumMember]
        AerialWithLabels = 3
    }
}