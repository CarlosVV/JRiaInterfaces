using System.Runtime.Serialization;
using CES.CoreApi.Common.Attributes;

namespace CES.CoreApi.Common.Enumerations
{
    [DataContract]
    public enum DatabaseType
    {
        [EnumMember] [ConnectionName("")] Undefined,
        [EnumMember] [ConnectionName("Main")] Main,
        [EnumMember] [ConnectionName("ReadOnly")] ReadOnly,
        [EnumMember] [ConnectionName("FrontEnd")] FrontEnd,
    }
}