using System.Runtime.Serialization;

namespace CES.CoreApi.Settings.Service.Contract.Enumerations
{
    [DataContract]
    public enum RecurrentBeneficiarySetting
    {
        [EnumMember] Disabled = 0,
        [EnumMember] Enabled = 1,
        [EnumMember] EnabledForSelectedAgents = 2
    }
}
