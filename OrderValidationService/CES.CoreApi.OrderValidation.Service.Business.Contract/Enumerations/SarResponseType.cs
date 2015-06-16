using System.ComponentModel;

namespace CES.CoreApi.OrderValidation.Service.Business.Contract.Enumerations
{
    public enum SarResponseType
    {
        [Description("")]
        None,
        [Description("On Hold")]
        OnHold,
        [Description("On Hold Without Information")]
        OnHoldWithoutInfo,
        [Description("Reject")]
        Reject,
        [Description("Reject Without Information")]
        RejectWithoutInfo,
        RejectByRequirements //Should it be handled
    }
}