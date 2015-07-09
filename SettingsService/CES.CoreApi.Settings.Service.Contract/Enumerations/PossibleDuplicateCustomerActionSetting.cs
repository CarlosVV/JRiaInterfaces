using System.Runtime.Serialization;

namespace CES.CoreApi.Settings.Service.Contract.Enumerations
{
    [DataContract]
    public enum PossibleDuplicateCustomerActionSetting
    {
        [EnumMember] Reject = 1,
        [EnumMember] RejectWithoutInformation = 2,
        [EnumMember] OnHold = 10,
        [EnumMember] OnHoldWithoutInformation = 11,
        [EnumMember] LogOnly = 20
    }
}
