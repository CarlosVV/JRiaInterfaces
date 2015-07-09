using System.ServiceModel;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Settings.Service.Contract.Constants;

namespace CES.CoreApi.Settings.Service.Contract.Interfaces
{
    [ServiceContract(Namespace = Namespaces.SettingsServiceContractNamespace)]
    public interface IReferenceDataService
    {
        [OperationContract(Name = "GetIdentificationTypeList")]
        [FaultContract(typeof(CoreApiServiceFault), Action = Common.Constants.Namespaces.ServiceFaultContractAction)]
        GetIdentificationTypeListResponse GetIdentificationTypeList(GetIdentificationTypeListRequest request);
    }
}