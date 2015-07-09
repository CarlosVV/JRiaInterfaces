using System.Runtime.Serialization;

namespace CES.CoreApi.Settings.Service.Contract.Enumerations
{
    [DataContract]
    public enum AccountReceivableLimitDisplayOption
    {
        [EnumMember] Nothing = 0,
        [EnumMember] ShowPercentOfRemainingCredit = 1,
        [EnumMember] ShowPercentUsed = 2
    }
}