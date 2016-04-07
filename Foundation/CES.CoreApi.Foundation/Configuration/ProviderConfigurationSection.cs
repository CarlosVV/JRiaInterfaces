using System.Configuration;

namespace CES.CoreApi.Foundation.Configuration
{
	class ProviderConfigurationSection : ConfigurationSection
	{
		private static ProviderConfigurationSection instance;


		public static ProviderConfigurationSection Instance => instance ??
			(instance = ConfigurationManager.GetSection("providerConfigurationSection") 
			as ProviderConfigurationSection);

		[ConfigurationProperty("Bing")]
		public GeoLocationConfiguration Bing => (GeoLocationConfiguration)this["Bing"];


		[ConfigurationProperty("Google")]
		public GeoLocationConfiguration Google => (GeoLocationConfiguration)this["Google"];

		[ConfigurationProperty("MelissaData")]
		public GeoLocationConfiguration MelissaData => (GeoLocationConfiguration)this["MelissaData"];
	}
}
