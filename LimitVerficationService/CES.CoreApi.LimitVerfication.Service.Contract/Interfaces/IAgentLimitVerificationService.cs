using System.ServiceModel;
using CES.CoreApi.Common.Models;
using CES.CoreApi.LimitVerfication.Service.Contract.Constants;
using CES.CoreApi.LimitVerfication.Service.Contract.Models;

namespace CES.CoreApi.LimitVerfication.Service.Contract.Interfaces
{
    [ServiceContract(Namespace = Namespaces.LimitVerificationServiceContractNamespace)]
    public interface IAgentLimitVerificationService
    {
        [OperationContract(Name = "CheckPayingAgentLimits")]
        [FaultContract(typeof(CoreApiServiceFault), Action = Common.Constants.Namespaces.ServiceFaultContractAction)]
        CheckPayingAgentLimitsResponse CheckPayingAgentLimits(CheckPayingAgentLimitsRequest request);

        [OperationContract(Name = "CheckReceivingAgentLimits")]
        [FaultContract(typeof(CoreApiServiceFault), Action = Common.Constants.Namespaces.ServiceFaultContractAction)]
        CheckReceivingAgentLimitsResponse CheckReceivingAgentLimits(CheckReceivingAgentLimitsRequest request);
    }
}