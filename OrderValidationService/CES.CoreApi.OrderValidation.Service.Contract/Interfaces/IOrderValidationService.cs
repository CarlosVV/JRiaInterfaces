using System.ServiceModel;
using CES.CoreApi.Common.Constants;
using CES.CoreApi.Common.Models;
using CES.CoreApi.OrderValidation.Service.Contract.Models;

namespace CES.CoreApi.OrderValidation.Service.Contract.Interfaces
{
     [ServiceContract(Namespace = Constants.Namespaces.OrderValidationServiceContractNamespace)]
     public interface IOrderValidationService
     {
         [OperationContract(Name = "ValidateOrder")]
         [FaultContract(typeof(CoreApiServiceFault), Action = Namespaces.ServiceFaultContractAction)]
         OrderValidateResponse ValidateOrder(OrderValidateRequest request);
     }
}