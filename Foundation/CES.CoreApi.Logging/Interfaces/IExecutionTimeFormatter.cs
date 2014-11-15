using System;

namespace CES.CoreApi.Logging.Interfaces
{
    public interface IExecutionTimeFormatter
    {
        /// <summary>
        /// Returns execution time formatted
        /// </summary>
        string Format(long elapsedMilliseconds, bool alignString = true);

        /// <summary>
        /// Returns execution time formatted as "HH hours MM minutes SS seconds"
        /// </summary>
        /// <param name="elapsed">Elapsed time</param>
        /// <param name="alignString"></param>
        /// <returns></returns>
        string FormatToHoursMinutesSeconds(TimeSpan elapsed, bool alignString = true);
    }
}