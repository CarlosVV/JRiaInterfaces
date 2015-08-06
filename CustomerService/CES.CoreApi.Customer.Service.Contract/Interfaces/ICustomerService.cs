using System.ServiceModel;
using System.Threading.Tasks;
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
        Task<CustomerGetResponse> Get(CustomerGetRequest request);

        [OperationContract(Name = "Create")]
        [FaultContract(typeof(CoreApiServiceFault), Action = Common.Constants.Namespaces.ServiceFaultContractAction)]
        Task<CustomerCreateResponse> Create(CustomerCreateRequest request);

        [OperationContract(Name = "ProcessSignature")]
        [FaultContract(typeof(CoreApiServiceFault), Action = Common.Constants.Namespaces.ServiceFaultContractAction)]
        Task<CustomerProcessSignatureResponse> ProcessSignature(CustomerProcessSignatureRequest request);
    }
}
