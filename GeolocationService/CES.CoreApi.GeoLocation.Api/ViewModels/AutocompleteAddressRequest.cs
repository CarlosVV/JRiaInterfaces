using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.GeoLocation.Api.ViewModels;


namespace CES.CoreApi.GeoLocation.Api.ViewModels
{
	[DataContract]
	public class AutocompleteAddressRequest : BaseRequest
    {
        [DataMember(IsRequired = true)]
        public AddressRequest Address { get; set; }

        [DataMember]
        public int MaxRecords { get; set; }

        /// <summary>
        /// Specifying the minimum confidence required for the result.
        /// </summary>
        [DataMember(IsRequired = true)]
        public Confidence MinimumConfidence { get; set; }
    }
}