using System.Runtime.Serialization;

namespace CES.CoreApi.Settings.Service.Contract.Enumerations
{
    [DataContract]
    public enum ComplianceWarningPeriodicity
    {
        [EnumMember] Nothing = 0,
        [EnumMember] ShowOneTimePerMonth = 1,
        [EnumMember] ShowtwiceByDay = 2
    }
}
