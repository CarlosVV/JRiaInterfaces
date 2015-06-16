using System.ServiceModel;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Customer.Service.Contract.Constants;
using CES.CoreApi.Customer.Service.Contract.Models;

namespace CES.CoreApi.Customer.Service.Contract.Interfaces
{
    [ServiceContract(Namespace = Namespaces.CustomerServiceContractNamespace)]
    public interface ICustomerService
    {
        [OperationContract(Name = "Get")]
        [FaultContract(typeof(CoreApiServiceFault), Action = Common.Constants.Namespaces.ServiceFaultContractAction)]
        CustomerGetResponse Get(CustomerGetRequest request);

        [OperationContract(Name = "Create")]
        [FaultContract(typeof(CoreApiServiceFault), Action = Common.Constants.Namespaces.ServiceFaultContractAction)]
        CustomerCreateResponse Create(CustomerCreateRequest request);
    }
}
