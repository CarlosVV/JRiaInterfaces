using System.Runtime.Serialization;

namespace CES.CoreApi.Settings.Service.Contract.Enumerations
{
    [DataContract]
    public enum WatchListPayoutSetting
    {
        [EnumMember] Off = 0,
        [EnumMember] EnabledForAllCorrespondents = 1,
        [EnumMember] EnabledForSpecificCorrespondents = 2
    }
}
