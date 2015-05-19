using System.ServiceModel;
using CES.CoreApi.Common.Constants;
using CES.CoreApi.Common.Models;
using CES.CoreApi.OrderProcess.Service.Contract.Models;

namespace CES.CoreApi.OrderProcess.Service.Contract.Interfaces
{
    [ServiceContract(Namespace = Namespaces.OrderProcessServiceContractNamespace)]
    public interface IOrderProcessService
    {
        [OperationContract]
        [FaultContract(typeof(CoreApiServiceFault), Action = Namespaces.ServiceFaultContractAction)]
        OrderCreateResponse CreateOrder(OrderCreateRequest request);
    }
}
