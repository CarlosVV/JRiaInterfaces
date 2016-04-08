using System.Configuration;

namespace CES.CoreApi.Configuration.Provider
{
	public class GeoLocationConfigurationSection : ConfigurationSection
	{
		private static GeoLocationConfigurationSection instance;


		public static GeoLocationConfigurationSection Instance => instance ??
			(instance = ConfigurationManager.GetSection("geoLocationConfigurationSection") 
			as GeoLocationConfigurationSection);

		[ConfigurationProperty("Bing")]
		public GeoLocationConfiguration Bing => (GeoLocationConfiguration)this["Bing"];


		[ConfigurationProperty("Google")]
		public GeoLocationConfiguration Google => (GeoLocationConfiguration)this["Google"];

		[ConfigurationProperty("MelissaData")]
		public GeoLocationConfiguration MelissaData => (GeoLocationConfiguration)this["MelissaData"];
	}
}
