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

        /// <summary>
        /// Returns threshold in milliseconds, after which system logs call, 0 means log all calls
        /// </summary>
        [ConfigurationProperty(ConfigItemThreshold, DefaultValue = 500, IsRequired = true)]
        [IntegerValidator(MinValue = 0)]
        public int Threshold
        {
            get
            {
                return Convert.ToInt32(this[ConfigItemThreshold], CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Returns whether db performance log enabled 
        /// </summary>
        [ConfigurationProperty(ConfigItemIsEnabled, DefaultValue = false, IsRequired = true)]
        public bool IsEnabled
        {
            get
            {
                return Convert.ToBoolean(this[ConfigItemIsEnabled], CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Returns whether db performance log asynchronous
        /// </summary>
        [ConfigurationProperty(ConfigItemIsAsynchronous, DefaultValue = true, IsRequired = true)]
        public bool IsAsynchronous
        {
            get
            {
                return Convert.ToBoolean(this[ConfigItemIsAsynchronous], CultureInfo.InvariantCulture);
            }
        }
    }
}
