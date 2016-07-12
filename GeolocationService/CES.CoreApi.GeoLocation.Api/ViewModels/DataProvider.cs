using System.Runtime.Serialization;

namespace CES.CoreApi.GeoLocation.Service.Contract.Enumerations
{
    [DataContract]
    public enum DataProvider
    {

        [EnumMember]
        Undefined = 0,
        [EnumMember]
        MelissaData = 1,
        [EnumMember]
        Bing = 2,
        [EnumMember]
        Google = 3
    }
}
