using System.Reflection;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Interfaces
{
    public interface IPerformanceLogMonitor
    {
        /// <summary>
        /// Starts performance monitoring
        /// </summary>
        void Start(MethodBase method);

        /// <summary>
        /// Starts performance monitoring
        /// </summary>
        void Start(string methodName);

        /// <summary>
        /// Stops performance monitoring
        /// </summary>
        void Stop();

        /// <summary>
        /// Gets or sets trace log data container instance
        /// </summary>
        PerformanceLogDataContainer DataContainer { get; }
    }
}