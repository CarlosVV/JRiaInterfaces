using System.ServiceModel;
using CES.CoreApi.Accounting.Service.Contract.Constants;
using CES.CoreApi.Accounting.Service.Contract.Models;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.Accounting.Service.Contract.Interfaces
{
    [ServiceContract(Namespace = Namespaces.AccountingServiceContractNamespace)]
    public interface ITransactionInformationService
    {
        [OperationContract(Name = "GetTransactionSummary")]
        [FaultContract(typeof(CoreApiServiceFault), Action = Common.Constants.Namespaces.ServiceFaultContractAction)]
        GetTransactionSummaryResponse GetTransactionSummary(GetTransactionSummaryRequest request);
    }
}
