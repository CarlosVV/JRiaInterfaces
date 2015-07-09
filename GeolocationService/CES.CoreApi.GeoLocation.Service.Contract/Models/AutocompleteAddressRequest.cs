﻿using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using CES.CoreApi.GeoLocation.Service.Contract.Enumerations;
using Namespaces = CES.CoreApi.GeoLocation.Service.Contract.Constants.Namespaces;

namespace CES.CoreApi.GeoLocation.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.GeolocationServiceDataContractNamespace)]
    public class AutocompleteAddressRequest : BaseRequest
    {
        [DataMember]
        public int MaxRecords { get; set; }

        [DataMember(IsRequired = true)]
        public string Country { get; set; }

        [DataMember(IsRequired = true)]
        public string Address { get; set; }

        [DataMember]
        public string AdministrativeArea { get; set; }
        /// <summary>
        /// Specifying the minimum confidence required for the result.
        /// </summary>
        [DataMember(IsRequired = true)]
        public Confidence MinimumConfidence { get; set; }
    }
}