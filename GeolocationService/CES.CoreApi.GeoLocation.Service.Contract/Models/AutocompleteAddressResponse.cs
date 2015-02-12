﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Constants;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.GeoLocation.Service.Contract.Enumerations;

namespace CES.CoreApi.GeoLocation.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.GeolocationDataContractNamespace)]
    public class AutocompleteAddressResponse: BaseResponse
    {
        public AutocompleteAddressResponse(ICurrentDateTimeProvider currentDateTimeProvider) 
            : base(currentDateTimeProvider)
        {
        }

        [DataMember(EmitDefaultValue = false)]
        public List<AutocompleteSuggestion> Suggestions { get; set; }

        /// <summary>
        /// Specify data provider used to get address hint list
        /// </summary>
        [DataMember]
        public DataProvider DataProvider { get; set; }

        [DataMember]
        public bool IsValid { get; set; }
    }
}