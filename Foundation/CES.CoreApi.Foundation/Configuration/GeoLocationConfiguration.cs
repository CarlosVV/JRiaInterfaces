using System.Configuration;

namespace CES.CoreApi.Foundation.Configuration
{
	public class GeoLocationConfiguration : ConfigurationElement
	{
		[ConfigurationProperty("url")]
		public string Url => (string)this["url"];

		[ConfigurationProperty("key")]
		public string Key => (string)this["key"];

	}
}
