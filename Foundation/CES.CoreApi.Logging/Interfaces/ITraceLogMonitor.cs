using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Interfaces
{
    public interface ITraceLogMonitor
    {
        /// <summary>
        /// Gets or sets trace log data container instance
        /// </summary>
        ITraceLogDataContainer DataContainer { get; }

        /// <summary>
        /// Starts performance log monitoring
        /// </summary>
        void Start();

        /// <summary>
        /// Stops performance log monitoring
        /// </summary>
        void Stop();
    }
}