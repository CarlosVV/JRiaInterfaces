using System.ServiceModel;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Settings.Service.Contract.Constants;
using CES.CoreApi.Settings.Service.Contract.Models;

namespace CES.CoreApi.Settings.Service.Contract.Interfaces
{
    [ServiceContract(Namespace = Namespaces.SettingsServiceContractNamespace)]
    public interface ICountrySettingsService
    {
        [OperationContract(Name = "GetCountry")]
        [FaultContract(typeof(CoreApiServiceFault), Action = Common.Constants.Namespaces.ServiceFaultContractAction)]
        GetCountryResponse GetCountry(GetCountryRequest request);

        [OperationContract(Name = "GetCountryList")]
        [FaultContract(typeof(CoreApiServiceFault), Action = Common.Constants.Namespaces.ServiceFaultContractAction)]
        GetCountryListResponse GetCountryList(GetCountryListRequest request);

        [OperationContract(Name = "GetCountrySettings")]
        [FaultContract(typeof(CoreApiServiceFault), Action = Common.Constants.Namespaces.ServiceFaultContractAction)]
        GetCountrySettingsResponse GetCountrySettings(GetCountrySettingsRequest request);
    }
}
