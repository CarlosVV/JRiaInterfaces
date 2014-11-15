using System.ServiceModel;
using CES.CoreApi.Common.Constants;
using CES.CoreApi.Foundation.Contract.Models;
using CES.CoreApi.GeoLocation.Service.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Contract.Interfaces
{
    [ServiceContract(Namespace = Namespaces.GeolocationServiceContractNamespace)]
    public interface IMapService
    {
        [OperationContract]
        [FaultContract(typeof(CoreApiServiceFault), Action = Namespaces.ServiceFaultContractAction)]
        GetMapResponse GetMap(GetMapRequest request);
    }
}