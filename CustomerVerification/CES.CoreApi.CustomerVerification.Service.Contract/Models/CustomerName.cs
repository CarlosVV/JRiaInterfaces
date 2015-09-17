using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.CustomerVerification.Service.Contract.Constants;

namespace CES.CoreApi.CustomerVerification.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.CustomerVerificationServiceDataContractNamespace)]
    public class CustomerName : ExtensibleObject
    {
        [DataMember(IsRequired = true)]
        public string FirstName { get; set; }

        [DataMember(IsRequired = true)]
        public string LastName { get; set; }

        [DataMember]
        public string OtherFirstName { get; set; }
    }
}