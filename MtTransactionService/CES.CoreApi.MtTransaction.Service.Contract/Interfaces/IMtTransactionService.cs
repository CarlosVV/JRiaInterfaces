using System.ServiceModel;
using CES.CoreApi.Common.Models;
using CES.CoreApi.MtTransaction.Service.Contract.Constants;
using CES.CoreApi.MtTransaction.Service.Contract.Models;

namespace CES.CoreApi.MtTransaction.Service.Contract.Interfaces
{
    [ServiceContract(Namespace = Namespaces.MtTransactionServiceContractNamespace)]
    public interface IMtTransactionService
    {
        [OperationContract(Name="Create")]
        [FaultContract(typeof(CoreApiServiceFault), Action = Common.Constants.Namespaces.ServiceFaultContractAction)]
        MtTransactionCreateResponse Create(MtTransactionCreateRequest request);

        [OperationContract(Name = "Get")]
        [FaultContract(typeof(CoreApiServiceFault), Action = Common.Constants.Namespaces.ServiceFaultContractAction)]
        MtTransactionGetResponse Get(MtTransactionGetRequest request);
    }
}
