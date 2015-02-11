using System.Collections.Generic;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Enumerations;

namespace CES.CoreApi.GeoLocation.Service.Business.Contract.Models
{
    public class AutocompleteAddressResponseModel
    {
        public List<AutocompleteSuggestionModel> Suggestions { get; set; }

        /// <summary>
        /// Specify data provider used to get autocomplete list
        /// </summary>
        public DataProviderType DataProvider { get; set; }

        public bool IsValid { get; set; }
    }
}