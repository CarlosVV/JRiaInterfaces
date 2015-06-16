using CES.CoreApi.Common.Attributes;

namespace CES.CoreApi.Common.Enumerations
{
    public enum DatabaseType
    {
        [ConnectionName("")]
        Undefined,
        [ConnectionName("Main")]
        Main,
        [ConnectionName("ReadOnly")]
        ReadOnly
    }
}