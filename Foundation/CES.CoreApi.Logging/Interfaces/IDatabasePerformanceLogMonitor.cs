using System.Data.Common;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Interfaces
{
    public interface IDatabasePerformanceLogMonitor
    {
        /// <summary>
        /// Gets or sets database performance log data container instance
        /// </summary>
        DatabasePerformanceLogDataContainer DataContainer { get; }

        /// <summary>
        /// Starts performance log monitoring
        /// </summary>
        void Start(DbCommand command);

        /// <summary>
        /// Stops performance log monitoring
        /// </summary>
        void Stop();
    }
}