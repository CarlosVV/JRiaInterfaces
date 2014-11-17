using System.ServiceModel;
using CES.CoreApi.Common.Constants;
using CES.CoreApi.Foundation.Contract.Models;
using CES.CoreApi.GeoLocation.Service.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Contract.Interfaces
{
    [ServiceContract(Namespace = Namespaces.GeolocationServiceContractNamespace)]
    public interface IClientSideSupportService
    {
        /// <summary>
        /// Gets data provider key for using on client side like with mapping ajax control
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(CoreApiServiceFault), Action = Namespaces.ServiceFaultContractAction)]
        GetProviderKeyResponse GetProviderKey(GetProviderKeyRequest request);

        /// <summary>
        /// Logs event in log
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(CoreApiServiceFault), Action = Namespaces.ServiceFaultContractAction)]
        void LogEvent(LogEventRequest request);
    }
}
