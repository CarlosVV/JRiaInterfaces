using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.GeoLocation.Service.Contract.Enumerations;
using Namespaces = CES.CoreApi.GeoLocation.Service.Contract.Constants.Namespaces;

namespace CES.CoreApi.GeoLocation.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.GeolocationServiceDataContractNamespace)]
    public class ValidateFormattedAddressRequest : BaseRequest
    {
        /// <summary>
        /// Specifying formatted address to validate
        /// </summary>
        [DataMember(IsRequired = true)]
        public string FormattedAddress { get; set; }
        
        /// <summary>
        /// Specifying two character country code
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Country { get; set; }

        /// <summary>
        /// Specifying the minimum confidence required for the result.
        /// </summary>
        [DataMember(IsRequired = true)]
        public Confidence MinimumConfidence { get; set; }
    }
}