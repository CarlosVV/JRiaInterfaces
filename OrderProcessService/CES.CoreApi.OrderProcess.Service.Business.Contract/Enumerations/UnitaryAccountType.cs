using System.ComponentModel;

namespace CES.CoreApi.OrderProcess.Service.Business.Contract.Enumerations
{
    public enum UnitaryAccountType
    {
        [Description("")]
        None = 0,
        [Description("CLABE")]
        Clabe = 1,
        [Description("IBAN")]
        Iban = 2
    }
}
