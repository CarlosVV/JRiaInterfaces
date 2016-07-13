using System.Collections.Generic;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;


namespace CES.CoreApi.GeoLocation.Api.ViewModels
{
    [DataContract]
    public class AutocompleteAddress 
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
        public List<string> UnitsOrApartments { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string FormattedAddress { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string UnitOrApartment { get; set; }
    }
}
