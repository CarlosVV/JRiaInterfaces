using System.Configuration;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Logging.Configuration
{
	public class LogConfigurationProvider : ILogConfigurationProvider
	{
		private const string PerformanceLog = "performanceLog";
		private const string DatabasePerformanceLog = "databasePerformanceLog";
		private const string TraceLog = "traceLog";
		private IPerformanceLogConfiguration _performanceLogConfiguration;
		private IDatabasePerformanceLogConfiguration _databasePerformanceLogConfiguration;
		private ITraceLogConfiguration _traceLogConfiguration;

		public IPerformanceLogConfiguration PerformanceLogConfiguration => _performanceLogConfiguration ??
			(_performanceLogConfiguration = ConfigurationManager.GetSection(PerformanceLog) as IPerformanceLogConfiguration);

		public ITraceLogConfiguration TraceLogConfiguration => _traceLogConfiguration ??
			(_traceLogConfiguration = ConfigurationManager.GetSection(TraceLog) as ITraceLogConfiguration);

		public IDatabasePerformanceLogConfiguration DatabasePerformanceLogConfiguration => _databasePerformanceLogConfiguration ??
			(_databasePerformanceLogConfiguration = ConfigurationManager.GetSection(DatabasePerformanceLog) as IDatabasePerformanceLogConfiguration);
	}
}