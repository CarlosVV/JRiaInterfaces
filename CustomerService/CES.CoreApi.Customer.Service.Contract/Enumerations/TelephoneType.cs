using System.Runtime.Serialization;

namespace CES.CoreApi.Customer.Service.Contract.Enumerations
{
    [DataContract]
    public enum TelephoneType
    {
        [EnumMember] Undefined = 0,
        [EnumMember] Home = 1,
        [EnumMember] Cell = 2,
        [EnumMember] Work = 3,
        [EnumMember] Fax
    }
}