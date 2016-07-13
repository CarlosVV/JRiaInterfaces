using System.Configuration;

namespace CES.CoreApi.GeoLocation.Configuration
{
	public class BasicConfiguration : ConfigurationElement
	{
		[ConfigurationProperty("value")]
		public int Value => (int)this["value"];
	}
}
