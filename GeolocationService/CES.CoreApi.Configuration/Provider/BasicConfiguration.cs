using System.Configuration;

namespace CES.CoreApi.Configuration.Provider
{
	public class BasicConfiguration : ConfigurationElement
	{
		[ConfigurationProperty("value")]
		public int Value => (int)this["value"];
	}
}
