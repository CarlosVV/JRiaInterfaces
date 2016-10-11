using System.Runtime.Serialization;



namespace CES.CoreApi.GeoLocation.Api.ViewModels
{
    [DataContract]
    public class ValidateAddressRequest 
    {
        /// <summary>
        /// Specifying address to validate
        /// </summary>
        [DataMember(IsRequired = true)]
        public AddressRequest Address { get; set; }

        /// <summary>
        /// Specifying the minimum confidence required for the result.
        /// </summary>
        [DataMember(IsRequired = true)]
        public Confidence MinimumConfidence { get; set; }
    }
}