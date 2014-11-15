using System;
using System.Globalization;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Logging.Formatters
{
    public class DateTimeFormatter : IDateTimeFormatter
    {
        private const string StartTime = "yyyy-MM-dd h:mm:ss.fff tt";

        #region Public methods
        
        /// <summary>
        /// Returns execution start time formatted
        /// </summary>
        public string Format(DateTime date)
        {
            return date.ToString(StartTime,
                                 CultureInfo.InvariantCulture).PadRight(24, ' ');
        }

        #endregion //Public methods
    }
}