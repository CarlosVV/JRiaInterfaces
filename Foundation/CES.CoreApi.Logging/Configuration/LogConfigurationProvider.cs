using System.Configuration;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Logging.Configuration
{
	public class LogConfigurationProvider : ILogConfigurationProvider
	{
		#region Core

		private const string PerformanceLog = "performanceLog";
		private const string DatabasePerformanceLog = "databasePerformanceLog";
		private const string TraceLog = "traceLog";
		private IPerformanceLogConfiguration _performanceLogConfiguration;
		private IDatabasePerformanceLogConfiguration _databasePerformanceLogConfiguration;
		private ITraceLogConfiguration _traceLogConfiguration;

		#endregion

		#region Public properties

		/// <summary>
		///     Gets performance log configuration instance
		/// </summary>
		public IPerformanceLogConfiguration PerformanceLogConfiguration => _performanceLogConfiguration ??
			(_performanceLogConfiguration = ConfigurationManager.GetSection(PerformanceLog) as IPerformanceLogConfiguration);

		/// <summary>
		///     Gets trace log configuration instance
		/// </summary>
		public ITraceLogConfiguration TraceLogConfiguration => _traceLogConfiguration ??
			(_traceLogConfiguration = ConfigurationManager.GetSection(TraceLog) as ITraceLogConfiguration);

		/// <summary>
		///     Gets performance log configuration instance
		/// </summary>
		public IDatabasePerformanceLogConfiguration DatabasePerformanceLogConfiguration => _databasePerformanceLogConfiguration ??
			(_databasePerformanceLogConfiguration = ConfigurationManager.GetSection(DatabasePerformanceLog) as IDatabasePerformanceLogConfiguration);

		#endregion
	}
}