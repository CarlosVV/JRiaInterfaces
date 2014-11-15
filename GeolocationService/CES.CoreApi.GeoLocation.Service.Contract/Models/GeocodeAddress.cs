﻿using System.Runtime.Serialization;
using CES.CoreApi.Common.Constants;

namespace CES.CoreApi.GeoLocation.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.GeolocationDataContractNamespace, Name = "Address")]
    public class GeocodeAddress
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
