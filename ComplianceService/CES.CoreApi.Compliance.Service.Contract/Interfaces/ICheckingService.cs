using System.ServiceModel;
using CES.CoreApi.Common.Constants;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Compliance.Service.Contract.Models;

namespace CES.CoreApi.Compliance.Service.Contract.Interfaces
{
    [ServiceContract(Namespace = Constants.Namespaces.ComplianceServiceContractNamespace)]
    public interface ICheckingService
    {
        [OperationContract]
        [FaultContract(typeof(CoreApiServiceFault), Action = Namespaces.ServiceFaultContractAction)]
        CheckOrderResponse CheckOrder(CheckOrderRequest request);

        [OperationContract]
        [FaultContract(typeof(CoreApiServiceFault), Action = Namespaces.ServiceFaultContractAction)]
        CheckPayoutResponse CheckPayout(CheckPayoutRequest request);


    }
}
