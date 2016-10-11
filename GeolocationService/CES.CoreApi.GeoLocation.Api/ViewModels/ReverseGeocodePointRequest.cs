using System.Runtime.Serialization;



namespace CES.CoreApi.GeoLocation.Api.ViewModels
{
    [DataContract]
    public class ReverseGeocodePointRequest 
    {
        [DataMember(IsRequired = true)]
        public Location Location { get; set; }

        [DataMember(IsRequired = true)]
        public string Country { get; set; }

        /// <summary>
        /// Specifying the minimum confidence required for the result.
        /// </summary>
        [DataMember(IsRequired = true)]
        public Confidence MinimumConfidence { get; set; }
    }
}
