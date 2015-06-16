using System.Collections.Generic;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.GeoLocation.Service.Contract.Enumerations;
using Namespaces = CES.CoreApi.GeoLocation.Service.Contract.Constants.Namespaces;

namespace CES.CoreApi.GeoLocation.Service.Contract.Models
{
    [DataContract(Namespace = Namespaces.GeolocationServiceDataContractNamespace)]
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