namespace CES.CoreApi.Logging.Interfaces
{
    public interface ILogConfigurationProvider
    {
        /// <summary>
        /// Gets trace log configuration instance
        /// </summary>
        IPerformanceLogConfiguration PerformanceLogConfiguration { get; }

        /// <summary>
        /// Gets trace log configuration instance
        /// </summary>
        ITraceLogConfiguration TraceLogConfiguration { get; }

        /// <summary>
        /// Gets performance log configuration instance
        /// </summary>
        IDatabasePerformanceLogConfiguration DatabasePerformanceLogConfiguration { get; }
    }
}