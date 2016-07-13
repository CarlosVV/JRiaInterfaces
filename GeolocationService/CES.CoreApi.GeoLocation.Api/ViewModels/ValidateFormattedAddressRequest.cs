using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.GeoLocation.Api.ViewModels;


namespace CES.CoreApi.GeoLocation.Api.ViewModels
{
    [DataContract]
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