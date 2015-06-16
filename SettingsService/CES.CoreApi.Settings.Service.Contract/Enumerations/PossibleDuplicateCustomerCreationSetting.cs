using System.Runtime.Serialization;

namespace CES.CoreApi.Settings.Service.Contract.Enumerations
{
    [DataContract]
    public enum PossibleDuplicateCustomerCreationSetting
    {
        [EnumMember]
        DoNotCreate = 0,
        [EnumMember]
        Create = 1
    }
}
