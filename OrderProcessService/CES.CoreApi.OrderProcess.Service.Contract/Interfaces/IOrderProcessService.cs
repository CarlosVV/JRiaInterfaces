using System.ServiceModel;
using CES.CoreApi.Common.Models;
using CES.CoreApi.OrderProcess.Service.Contract.Constants;
using CES.CoreApi.OrderProcess.Service.Contract.Models;

namespace CES.CoreApi.OrderProcess.Service.Contract.Interfaces
{
    [ServiceContract(Namespace = Namespaces.OrderProcessServiceContractNamespace)]
    public interface IOrderProcessService
    {
        [OperationContract(Name="Create")]
        [FaultContract(typeof(CoreApiServiceFault), Action = Common.Constants.Namespaces.ServiceFaultContractAction)]
        OrderCreateResponse Create(OrderCreateRequest request);

        [OperationContract(Name = "Get")]
        [FaultContract(typeof(CoreApiServiceFault), Action = Common.Constants.Namespaces.ServiceFaultContractAction)]
        OrderGetResponse Get(OrderGetRequest request);
    }
}
