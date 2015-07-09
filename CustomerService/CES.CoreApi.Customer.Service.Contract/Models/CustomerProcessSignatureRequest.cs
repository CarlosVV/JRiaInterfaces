using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Customer.Service.Contract.Constants;

namespace CES.CoreApi.Customer.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.CustomerServiceDataContractNamespace)]
    public class CustomerProcessSignatureRequest: BaseRequest
    {
        [DataMember(IsRequired = true)]
        public int OrderId { get; set; }

        [DataMember(IsRequired = true)]
        public byte[] Signature { get; set; }
    }
}