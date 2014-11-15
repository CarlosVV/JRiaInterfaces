using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Interfaces
{
    public interface IPerformanceLogFormatter
    {
        /// <summary>
        /// Gets log entry formatted
        /// </summary>
        /// <param name="dataContainer">Log entry data</param>
        /// <returns></returns>
        string Format(PerformanceLogDataContainer dataContainer);
    }
}