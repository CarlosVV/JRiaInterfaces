using System;
using System.Globalization;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Logging.Formatters
{
    public class ExecutionTimeFormatter : IExecutionTimeFormatter
    {
        #region Core

        private const string ExecutionTime = "{0:N} ms.";
        private const string HoursMinutesSeconds = "{0} hours {1} minutes {2} seconds";
        private const string MinutesSeconds = "{0} minutes {1} seconds";
        private const string Seconds = "{0} seconds";

        #endregion

        #region Public methods

        /// <summary>
        /// Returns execution time formatted
        /// </summary>
        public string Format(long elapsedMilliseconds, bool alignString = true)
        {
            var result = string.Format(CultureInfo.InvariantCulture,
                                       ExecutionTime,
                                       elapsedMilliseconds);
            if (alignString)
                result = result.PadLeft(18, ' ');
            return result;
        }

        /// <summary>
        /// Returns execution time formatted as "HH hours MM minutes SS seconds"
        /// </summary>
        /// <param name="elapsed">Elapsed time</param>
        /// <param name="alignString"></param>
        /// <returns></returns>
        public string FormatToHoursMinutesSeconds(TimeSpan elapsed, bool alignString = true)
        {
            string result;

            if (elapsed.Hours > 0)
            {
                result = string.Format(CultureInfo.InvariantCulture,
                                       HoursMinutesSeconds,
                                       elapsed.Hours,
                                       elapsed.Minutes,
                                       elapsed.Seconds);
            }
            else if (elapsed.Hours == 0 && elapsed.Minutes > 0)
            {
                result = string.Format(CultureInfo.InvariantCulture,
                                       MinutesSeconds,
                                       elapsed.Minutes,
                                       elapsed.Seconds);
            }
            else
            {
                result = string.Format(CultureInfo.InvariantCulture,
                                       Seconds,
                                       elapsed.Seconds);
            }

            if (alignString)
                result = result.PadLeft(18, ' ');
            return result;
        }

        #endregion //Public methods
    }
}