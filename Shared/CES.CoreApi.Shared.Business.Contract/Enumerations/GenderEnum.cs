using System.ComponentModel;

namespace CES.CoreApi.Shared.Business.Contract.Enumerations
{
    public enum GenderEnum
    {
        Undefined = 0,
        [Description("M")]
        Male = 1,
        [Description("F")]
        Female = 2
    }
}
