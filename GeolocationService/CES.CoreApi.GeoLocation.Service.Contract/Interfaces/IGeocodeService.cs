using System.ServiceModel;
using CES.CoreApi.Common.Constants;
using CES.CoreApi.Common.Models;
using CES.CoreApi.GeoLocation.Service.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Contract.Interfaces
{
    [ServiceContract(Namespace = Namespaces.GeolocationServiceContractNamespace)]
    public interface IGeocodeService
    {
        /// <summary>
        /// Accepts address and returns geographic coordinates
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(CoreApiServiceFault), Action = Namespaces.ServiceFaultContractAction)]
        GeocodeAddressResponse GeocodeAddress(GeocodeAddressRequest request);

        /// <summary>
        /// Accepts address as a formatted string and returns geographic coordinates
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract(Name = "GeocodeFormattedAddress")]
        [FaultContract(typeof(CoreApiServiceFault), Action = Namespaces.ServiceFaultContractAction)]
        GeocodeAddressResponse GeocodeAddress(GeocodeFormattedAddressRequest request);

        /// <summary>
        /// Returns address by geo point coordinates
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(CoreApiServiceFault), Action = Namespaces.ServiceFaultContractAction)]
        GeocodeAddressResponse ReverseGeocodePoint(ReverseGeocodePointRequest request);
    }
}