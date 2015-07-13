using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Customer.Service.Contract.Constants;
using CES.CoreApi.Customer.Service.Contract.Enumerations;

namespace CES.CoreApi.Customer.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.CustomerServiceDataContractNamespace)]
    public class CustomerAddress : ExtensibleObject
    {
        [DataMember(EmitDefaultValue = false)]
        public string UnitNumber { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Address { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string City { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string State { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string PostalCode { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Country { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Location Location { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public AddressValidationStatus ValidationStatus { get; set; }
    }
}