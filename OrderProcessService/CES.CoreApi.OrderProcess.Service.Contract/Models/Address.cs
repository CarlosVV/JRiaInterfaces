using System.Runtime.Serialization;
using CES.CoreApi.OrderProcess.Service.Contract.Constants;

namespace CES.CoreApi.OrderProcess.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.OrderProcessDataContractNamespace)]
    public class Address
    {
        [DataMember(EmitDefaultValue = false)]
        public string UnitNumber { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Address1 { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Address2 { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Address3 { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string City { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int? CityId { set; get; }

        [DataMember(EmitDefaultValue = false)]
        public string State { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int? StateId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string PostalCode { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Country { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int? CountryId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Geolocation Geolocation { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string ValidationStatus { get; set; }
    }
}