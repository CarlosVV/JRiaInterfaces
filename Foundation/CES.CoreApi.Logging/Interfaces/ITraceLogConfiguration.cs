using System.Configuration;
using CES.CoreApi.Logging.Configuration;

namespace CES.CoreApi.Logging.Interfaces
{
    public interface ITraceLogConfiguration
    {
        /// <summary>
        /// Returns whether Trace log enabled
        /// </summary>
        bool IsEnabled { get; }

        /// <summary>
        /// Returns whether Trace log asynchronous
        /// </summary>
        bool IsAsynchronous { get; }

        /// <summary>
        /// Returns whether request logging enabled
        /// </summary>
        bool IsRequestLoggingEnabled { get; }

        /// <summary>
        /// Returns whether response logging enabled
        /// </summary>
        bool IsResponseLoggingEnabled { get; }
    }
}