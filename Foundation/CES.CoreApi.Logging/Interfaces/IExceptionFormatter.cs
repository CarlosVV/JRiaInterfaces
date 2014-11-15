using System;

namespace CES.CoreApi.Logging.Interfaces
{
    public interface IExceptionFormatter
    {
        /// <summary>
        /// Gets exception formatted as a string
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <param name="timestamp">Exception timestamp </param>
        /// <returns></returns>
        string Format(Exception exception, DateTime timestamp);
    }
}