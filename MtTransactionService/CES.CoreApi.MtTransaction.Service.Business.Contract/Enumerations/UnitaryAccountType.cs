using System.ComponentModel;

namespace CES.CoreApi.MtTransaction.Service.Business.Contract.Enumerations
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
