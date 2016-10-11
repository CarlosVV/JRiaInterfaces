using System.Runtime.Serialization;

using CES.CoreApi.Common.Models;
using CES.CoreApi.GeoLocation.Api.ViewModels;


namespace CES.CoreApi.GeoLocation.Api.ViewModels
{
    [DataContract]
    public class GeocodeAddressResponse : BaseResponse
    {
    

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
