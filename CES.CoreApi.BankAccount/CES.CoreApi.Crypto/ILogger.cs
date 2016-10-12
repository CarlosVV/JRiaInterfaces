using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.CoreApi.Crypto
{
    /// <summary>
    /// This interface exposes the set of functions for logging
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs any message
        /// </summary>
        /// <param name="message">The message to log</param>
        void LogDebug(string message);//@@2015-08-28 lb SCR# 2395411 Created


        /// <summary>
        /// Logs an informative message
        /// </summary>
        /// <param name="message">The message to log</param>
        void LogMessage(string message);

        /// <summary>
        /// Logs a message as error
        /// </summary>
        /// <param name="message">The message to log</param>
        void LogError(string message);

        /// <summary>
        /// Logs a message as a warning
        /// </summary>
        /// <param name="message">The message to log</param>
        void LogWarning(string message);

        /// <summary>
        /// Logs an exception in an standard way
        /// </summary>
        /// <param name="ex"></param>
        void LogException(Exception ex); //@@2008-03-25 lb SCR# 502311 Created
    }
}
