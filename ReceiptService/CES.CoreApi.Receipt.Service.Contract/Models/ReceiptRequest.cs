using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Receipt.Service.Contract.Constants;

namespace CES.CoreApi.Receipt.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.ReceiptServiceDataContractNamespace)]
    public class ReceiptRequest: BaseRequest
    {
        [DataMember(IsRequired = true)]
        public int Id { get; set; }
    }
}
