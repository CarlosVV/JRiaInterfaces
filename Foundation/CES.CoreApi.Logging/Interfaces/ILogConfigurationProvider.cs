namespace CES.CoreApi.Logging.Interfaces
{
	public interface ILogConfigurationProvider
	{
		IPerformanceLogConfiguration PerformanceLogConfiguration { get; }

		ITraceLogConfiguration TraceLogConfiguration { get; }

		IDatabasePerformanceLogConfiguration DatabasePerformanceLogConfiguration { get; }
	}
}