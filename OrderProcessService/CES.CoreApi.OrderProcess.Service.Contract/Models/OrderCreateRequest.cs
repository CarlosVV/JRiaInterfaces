using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.OrderProcess.Service.Contract.Constants;

namespace CES.CoreApi.OrderProcess.Service.Contract.Models
{
     [DataContract(Namespace = Namespaces.OrderProcessDataContractNamespace)]
    public class OrderCreateRequest : BaseRequest
    {
    }
}