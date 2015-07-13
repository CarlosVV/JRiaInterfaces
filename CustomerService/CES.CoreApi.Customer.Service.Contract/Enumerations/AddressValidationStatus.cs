using System.Runtime.Serialization;

namespace CES.CoreApi.Customer.Service.Contract.Enumerations
{
    [DataContract]
    public enum AddressValidationStatus
    {
        [EnumMember]
        Undefined = 0,
        [EnumMember]
        Verified = 1
    }
}