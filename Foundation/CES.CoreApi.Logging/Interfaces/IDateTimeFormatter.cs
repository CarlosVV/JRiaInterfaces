using System;

namespace CES.CoreApi.Logging.Interfaces
{
    public interface IDateTimeFormatter
    {
        /// <summary>
        /// Returns execution start time formatted
        /// </summary>
        string Format(DateTime date);
    }
}