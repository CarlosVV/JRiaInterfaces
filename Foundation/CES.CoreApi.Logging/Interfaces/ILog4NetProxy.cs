using System;
using CES.CoreApi.Common.Enumerations;

namespace CES.CoreApi.Logging.Interfaces
{
    public interface ILog4NetProxy
    {
        /// <summary>
        /// Sets parameter for logging
        /// </summary>
        /// <param name="parameterName">Parameter name</param>
        /// <param name="parameterValue">Parameter value</param>
        void SetParameter(string parameterName, object parameterValue);

        /// <summary>
        /// Removes parameter for logging
        /// </summary>
        /// <param name="parameterName">Parameter name</param>
        void RemoveParameter(string parameterName);

        /// <summary>
        /// Removes all logging parameters
        /// </summary>
        void ClearParameters();

        /// <summary>
        /// Publishes message according provided entry type
        /// </summary>
        /// <param name="entryType">Log entry type</param>
        /// <param name="message">Log message</param>
        /// <param name="exception">Exception instance - optional </param>
        void Publish(LogEntryType entryType, string message, Exception exception = null);

        /// <summary>
        /// Publishes debug message
        /// </summary>
        /// <param name="message">Log message</param>
        void PublishDebug(string message);

        /// <summary>
        /// Formats message using Invariant culture and supplied parameters and publishes DEBUG message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="parameters">Message parameters</param>
        void PublishDebug(string message, params object[] parameters);

        /// <summary>
        /// Publishes warning message
        /// </summary>
        /// <param name="message">Log message</param>
        void PublishWarning(string message);

        /// <summary>
        /// Publishes warning message
        /// </summary>
        /// <param name="dataContainer">Data container instance</param>
        void PublishWarning(IDataContainer dataContainer);

        /// <summary>
        /// Publishes INFORMATION message
        /// </summary>
        /// <param name="message">Log message</param>
        void PublishInformation(string message);

        /// <summary>
        /// Publishes INFORMATION message
        /// </summary>
        /// <param name="dataContainer">Data container instance</param>
        void PublishInformation(IDataContainer dataContainer);

        /// <summary>
        /// Formats message using Current culture and supplied parameters and publishes INFORMATION message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="parameters">Message parameters</param>
        void PublishInformation(string message, params object[] parameters);

        /// <summary>
        /// Publishes ERROR message
        /// </summary>
        /// <param name="message">Log message</param>
        void PublishError(string message);

        /// <summary>
        /// Publishes ERROR message
        /// </summary>
        /// <param name="dataContainer">Data container instance</param>
        void PublishError(IDataContainer dataContainer);

        /// <summary>
        /// Publishes ERROR message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="exception">Exception instance</param>
        void PublishError(string message, Exception exception);

        /// <summary>
        /// Formats message using Current culture and supplied parameters and publishes ERROR message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="parameters">Message parameters</param>
        void PublishError(string message, params object[] parameters);

        /// <summary>
        /// Publishes fatal message
        /// </summary>
        /// <param name="message">Log message</param>
        void PublishFatal(string message);

        /// <summary>
        /// Formats message using Current culture and supplied parameters and publishes FATAL message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="parameters">Message parameters</param>
        void PublishFatal(string message, params object[] parameters);

        /// <summary>
        /// Formats message using Current culture and supplied parameters and publishes FATAL message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="exception">Exception instance</param>
        void PublishFatal(string message, Exception exception);

        ///// <summary>
        ///// Checks if this logger is enabled for the Debug level.
        ///// Returns true if this logger is enabled for Debug events, false otherwise.
        ///// </summary>
        //bool IsDebugEnabled { get; }

        ///// <summary>
        ///// Checks if this logger is enabled for the Info level.
        ///// Returns true if this logger is enabled for Info events, false otherwise.
        ///// </summary>
        //bool IsInformationEnabled { get; }

        ///// <summary>
        ///// Checks if this logger is enabled for the Warn level.
        ///// Returns true if this logger is enabled for Warn events, false otherwise.
        ///// </summary>
        //bool IsWarningEnabled { get; }

        ///// <summary>
        ///// Checks if this logger is enabled for the Error level.
        ///// Returns true if this logger is enabled for Error events, false otherwise.
        ///// </summary>
        //bool IsErrorEnabled { get; }

        ///// <summary>
        ///// Checks if this logger is enabled for the Fatal level.
        ///// Returns true if this logger is enabled for Fatal events, false otherwise.
        ///// </summary>
        //bool IsFatalEnabled { get; }

        ///// <summary>
        ///// Checks if this logger is enabled for the Notice level.
        ///// Returns true if this logger is enabled for Notice events, false otherwise.
        ///// </summary>
        //bool IsNoticeEnabled { get; }

        /// <summary>
        /// Publishes NOTICE message
        /// </summary>
        /// <param name="message">Log message</param>
        void PublishNotice(string message);

        /// <summary>
        /// Publish MOTICE message 
        /// </summary>
        /// <param name="dataContainer">Data container instance</param>
        void PublishNotice(IDataContainer dataContainer);
    }
}