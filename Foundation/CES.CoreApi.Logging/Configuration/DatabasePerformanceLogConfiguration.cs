using System;
using System.Configuration;
using System.Globalization;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Logging.Configuration
{
	public class DatabasePerformanceLogConfiguration : ConfigurationSection, IDatabasePerformanceLogConfiguration
	{
		private const string ConfigItemIsEnabled = "isDatabasePerformanceLogEnabled";
		private const string ConfigItemThreshold = "databasePerformanceLogThreshold";
		private const string ConfigItemIsAsynchronous = "isDatabasePerformanceLogAsynchronous";

		[ConfigurationProperty(ConfigItemThreshold, DefaultValue = 500, IsRequired = true)]
		[IntegerValidator(MinValue = 0)]
		public int Threshold => Convert.ToInt32(this[ConfigItemThreshold], CultureInfo.InvariantCulture);

		[ConfigurationProperty(ConfigItemIsEnabled, DefaultValue = false, IsRequired = true)]
		public bool IsEnabled => Convert.ToBoolean(this[ConfigItemIsEnabled], CultureInfo.InvariantCulture);

		[ConfigurationProperty(ConfigItemIsAsynchronous, DefaultValue = true, IsRequired = true)]
		public bool IsAsynchronous => Convert.ToBoolean(this[ConfigItemIsAsynchronous], CultureInfo.InvariantCulture);
	}
}
