using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Customer.Service.Contract.Constants;

namespace CES.CoreApi.Customer.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.CustomerServiceDataContractNamespace)]
    public class CustomerGetRequest: BaseRequest
    {
        [DataMember(IsRequired = true)]
        public int CustomerId { get; set; }
    }
}