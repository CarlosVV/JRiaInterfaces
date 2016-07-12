using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.GeoLocation.Service.Contract.Models
{

    public class AddressRequest : BaseRequest
    {
    
        public string Address1 { get; set; }

 
        public string Address2 { get; set; }

    
        public string Country { get; set; }

     
        public string AdministrativeArea { get; set; }

   
        public string City { get; set; }

   
        public string PostalCode { get; set; }
    }
}
