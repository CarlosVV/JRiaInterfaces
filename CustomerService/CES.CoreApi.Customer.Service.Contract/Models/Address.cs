using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Customer.Service.Contract.Constants;
using CES.CoreApi.Customer.Service.Contract.Enumerations;

namespace CES.CoreApi.Customer.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.CustomerServiceDataContractNamespace)]
    public class Address : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public string UnitNumber { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Address1 { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string City { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string State { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string PostalCode { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Country { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Geolocation Geolocation { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public AddressValidationStatus ValidationStatus { get; set; }
    }
}