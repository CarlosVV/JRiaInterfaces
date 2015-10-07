using System.ServiceModel;
using CES.CoreApi.Common.Constants;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Receipt.Service.Contract.Models;

namespace CES.CoreApi.Receipt.Service.Contract.Interfaces
{
    [ServiceContract(Namespace = Constants.Namespaces.ReceiptServiceContractNamespace)]
    public interface IReceiptService
    {
        [OperationContract]
        [FaultContract(typeof(CoreApiServiceFault), Action = Namespaces.ServiceFaultContractAction)]
        ReceiptResponse Test(ReceiptRequest request);
    }
}
