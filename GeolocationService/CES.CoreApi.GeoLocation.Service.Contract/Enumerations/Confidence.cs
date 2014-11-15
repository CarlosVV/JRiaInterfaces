using System.Runtime.Serialization;

namespace CES.CoreApi.GeoLocation.Service.Contract.Enumerations
{
    [DataContract]
    public enum Confidence
    {
        [EnumMember]
        Undefined = 0,
        [EnumMember]
        NotFound = 1,
        [EnumMember]
        Low = 2,
        [EnumMember]
        Medium = 3,
        [EnumMember]
        High = 4
    }
}