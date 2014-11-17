using System.Runtime.Serialization;
using CES.CoreApi.Common.Constants;
using CES.CoreApi.Foundation.Contract.Models;

namespace CES.CoreApi.GeoLocation.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.GeolocationDataContractNamespace)]
    public class AddressRequest : BaseRequest
    {
        [DataMember(IsRequired = true)]
        public string Address1 { get; set; }

        [DataMember]
        public string Address2 { get; set; }

        [DataMember(IsRequired = true)]
        public string Country { get; set; }

        [DataMember]
        public string AdministrativeArea { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string PostalCode { get; set; }
    }
}
