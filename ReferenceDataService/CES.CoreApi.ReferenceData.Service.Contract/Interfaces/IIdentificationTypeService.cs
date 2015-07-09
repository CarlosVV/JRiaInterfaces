using System.ServiceModel;
using CES.CoreApi.Common.Models;
using CES.CoreApi.ReferenceData.Service.Contract.Constants;
using CES.CoreApi.ReferenceData.Service.Contract.Models;

namespace CES.CoreApi.ReferenceData.Service.Contract.Interfaces
{
    [ServiceContract(Namespace = Namespaces.ReferenceDataServiceContractNamespace)]
    public interface IIdentificationTypeService
    {
        [OperationContract(Name = "GetList")]
        [FaultContract(typeof(CoreApiServiceFault), Action = Common.Constants.Namespaces.ServiceFaultContractAction)]
        GetIdentificationTypeListResponse GetList(GetIdentificationTypeListRequest request);

        [OperationContract(Name = "Get")]
        [FaultContract(typeof(CoreApiServiceFault), Action = Common.Constants.Namespaces.ServiceFaultContractAction)]
        GetIdentificationTypeResponse Get(GetIdentificationTypeRequest request);
    }
}