using System.Configuration;

namespace CES.CoreApi.Configuration.Provider
{
	public class GeoLocationConfiguration : ConfigurationElement
	{
		[ConfigurationProperty("key")]
		public string Key => (string)this["key"];

		[ConfigurationProperty("url")]
		public string Url => (string)this["url"];

		[ConfigurationProperty("autoCompleteUrl")]
		public string AutoCompleteUrl => (string)this["autoCompleteUrl"];

		[ConfigurationProperty("addressGeocodeAndVerificationUrl")]
		public string AddressGeocodeAndVerificationUrl => (string)this["addressGeocodeAndVerificationUrl"];

		[ConfigurationProperty("reverseGeocodePointUrl")]
		public string ReverseGeocodePointUrl => (string)this["reverseGeocodePointUrl"];


		


	}
	
}
