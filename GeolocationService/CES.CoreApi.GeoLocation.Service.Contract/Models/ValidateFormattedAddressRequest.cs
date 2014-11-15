﻿using System.Runtime.Serialization;
using CES.CoreApi.Common.Constants;
using CES.CoreApi.Foundation.Contract.Models;
using CES.CoreApi.GeoLocation.Service.Contract.Enumerations;

namespace CES.CoreApi.GeoLocation.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.GeolocationDataContractNamespace)]
    public class ValidateFormattedAddressRequest : BaseRequest
    {
        /// <summary>
        /// Specifying formatted address to validate
        /// </summary>
        [DataMember(IsRequired = true)]
        public string FormattedAddress { get; set; }
        
        /// <summary>
        /// Specifying two character country code
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Country { get; set; }

        /// <summary>
        /// Specifying the minimum confidence required for the result.
        /// </summary>
        [DataMember(IsRequired = true)]
        public Confidence MinimumConfidence { get; set; }
    }
}