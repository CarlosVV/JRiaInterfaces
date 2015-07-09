using System.ServiceModel;
using CES.CoreApi.Common.Constants;
using CES.CoreApi.Common.Models;
using CES.CoreApi.GeoLocation.Service.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Contract.Interfaces
{
    [ServiceContract(Namespace = Constants.Namespaces.GeolocationServiceContractNamespace)]
    public interface IAddressService
    {
        /// <summary>
        /// Accepts address and returns boolean value if address found, otherwise returns true.
        /// Includes confidence
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(CoreApiServiceFault), Action = Namespaces.ServiceFaultContractAction)]
        ValidateAddressResponse ValidateAddress(ValidateAddressRequest request);

        /// <summary>
        /// Accepts address as a formatted string and returns boolean value if address found, 
        /// otherwise returns true. Includes confidence
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [OperationContract(Name = "ValidateFormattedAddress")]
        [FaultContract(typeof(CoreApiServiceFault), Action = Namespaces.ServiceFaultContractAction)]
        ValidateAddressResponse ValidateAddress(ValidateFormattedAddressRequest request);

        /// <summary>
        /// Accepts country code and address formatted string
        /// </summary>
        /// <param name="request"></param>
        /// <returns>List of address hints</returns>
        [OperationContract]
        [FaultContract(typeof(CoreApiServiceFault), Action = Namespaces.ServiceFaultContractAction)]
        AutocompleteAddressResponse GetAutocompleteList(AutocompleteAddressRequest request);
    }
}