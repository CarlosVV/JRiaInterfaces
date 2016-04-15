using System;
using System.Configuration;
using System.Globalization;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Logging.Configuration
{
	public class TraceLogConfiguration : ConfigurationSection, ITraceLogConfiguration
	{
		private const string ConfigItemIsEnabled = "isTraceLogEnabled";
		private const string ConfigItemIsAsynchronous = "isTraceLogAsynchronous";
		private const string ConfigItemIsRequestLoggingEnabled = "isRequestLoggingEnabled";
		private const string ConfigItemIsResponseLoggingEnabled = "isResponseLoggingEnabled";

		[ConfigurationProperty(ConfigItemIsEnabled, DefaultValue = false, IsRequired = true)]
		public bool IsEnabled => Convert.ToBoolean(this[ConfigItemIsEnabled], CultureInfo.InvariantCulture);

		[ConfigurationProperty(ConfigItemIsAsynchronous, DefaultValue = true, IsRequired = true)]
		public bool IsAsynchronous => Convert.ToBoolean(this[ConfigItemIsAsynchronous], CultureInfo.InvariantCulture);

		[ConfigurationProperty(ConfigItemIsRequestLoggingEnabled, DefaultValue = true, IsRequired = true)]
		public bool IsRequestLoggingEnabled => Convert.ToBoolean(this[ConfigItemIsRequestLoggingEnabled], CultureInfo.InvariantCulture);

		[ConfigurationProperty(ConfigItemIsResponseLoggingEnabled, DefaultValue = false, IsRequired = true)]
		public bool IsResponseLoggingEnabled => Convert.ToBoolean(this[ConfigItemIsResponseLoggingEnabled], CultureInfo.InvariantCulture);
	}
}
