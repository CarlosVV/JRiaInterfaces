using System;
using System.Configuration;
using System.Globalization;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Logging.Configuration
{
	public class PerformanceLogConfiguration : ConfigurationSection, IPerformanceLogConfiguration
	{
		private const string ConfigItemIsEnabled = "isPerformanceLogEnabled";
		private const string ConfigItemThreshold = "performanceLogThreshold";
		private const string ConfigItemIsAsynchronous = "isPerformanceLogAsynchronous";

		[ConfigurationProperty(ConfigItemThreshold, DefaultValue = 1000, IsRequired = true)]
		[IntegerValidator(MinValue = 0)]
		public int Threshold => Convert.ToInt32(this[ConfigItemThreshold], CultureInfo.InvariantCulture);

		[ConfigurationProperty(ConfigItemIsEnabled, DefaultValue = false, IsRequired = true)]
		public bool IsEnabled => Convert.ToBoolean(this[ConfigItemIsEnabled], CultureInfo.InvariantCulture);

		[ConfigurationProperty(ConfigItemIsAsynchronous, DefaultValue = true, IsRequired = true)]
		public bool IsAsynchronous => Convert.ToBoolean(this[ConfigItemIsAsynchronous], CultureInfo.InvariantCulture);
	}
}