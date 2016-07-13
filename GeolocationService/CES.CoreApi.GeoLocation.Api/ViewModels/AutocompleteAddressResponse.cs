using System.Collections.Generic;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.GeoLocation.Api.ViewModels
{
	[DataContract]
    public class AutocompleteAddressResponse: BaseResponse
    {
       
        [DataMember]
        public List<AutocompleteSuggestion> Suggestions { get; set; }

        /// <summary>
        /// Specify data provider used to get address hint list
        /// </summary>     
	
		[DataMember]
        public bool IsValid { get; set; }

		[DataMember]
		public DataProvider DataProvider { get; set; }
		[DataMember]
		public string Provider
		{
			get
			{
				return DataProvider.ToString();
			}
		}
	}
}