using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.OrderProcess.Service.Contract.Constants;

namespace CES.CoreApi.OrderProcess.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.OrderProcessDataContractNamespace)]
    public class OrderGetRequest: BaseRequest
    {
        [DataMember(IsRequired = true)]
        public int OrderId { get; set; }
        [DataMember]
        public int DatabaseId { get; set; }
    }
}