using System.Runtime.Serialization;



namespace CES.CoreApi.GeoLocation.Api.ViewModels
{
    [DataContract(Name = "Address")]
    public class ValidatedAddress 
    {
        [DataMember(EmitDefaultValue = false)]
        public string Address1 { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Address2 { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Country { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string AdministrativeArea { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string City { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string PostalCode { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FormattedAddress { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string UnitOrApartment { get; set; }
    }
}
