

namespace CES.CoreApi.GeoLocation.Api.ViewModels
{
 
    public class GeocodeAddressRequest 
    {
    
        public AddressRequest Address { get; set; }

        /// <summary>
        /// Specifying the minimum confidence required for the result.
        /// </summary>
   
        public Confidence MinimumConfidence { get; set; }
    }
}
