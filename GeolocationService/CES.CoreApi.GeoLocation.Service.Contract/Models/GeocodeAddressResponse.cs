using System.Runtime.Serialization;
using CES.CoreApi.Common.Constants;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Foundation.Contract.Models;
using CES.CoreApi.GeoLocation.Service.Contract.Enumerations;

namespace CES.CoreApi.GeoLocation.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.GeolocationDataContractNamespace)]
    public class GeocodeAddressResponse : BaseResponse
    {
        public GeocodeAddressResponse(ICurrentDateTimeProvider currentDateTimeProvider) 
            : base(currentDateTimeProvider)
        {
        }

        [DataMember]
        public Location Location { get; set; }
        
        /// <summary>
        /// A string specifying the confidence of the result.
        /// </summary>
        [DataMember]
        public Confidence Confidence { get; set; }

        /// <summary>
        /// Address geocode status.
        /// Returns true if address was found with requested MinimumConfidence level,
        /// otherwise false
        /// </summary>
        [DataMember]
        public bool IsValid { get; set; }
  
        /// <summary>
        /// Specify data provider used to geocode address
        /// </summary>
        [DataMember]
        public DataProvider DataProvider { get; set; }

        /// <summary>
        /// Address which was really geo coded by data provider
        /// </summary>
        [DataMember]
        public GeocodeAddress Address { get; set; }
    }
}
