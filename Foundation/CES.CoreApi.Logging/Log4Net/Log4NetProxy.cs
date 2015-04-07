﻿using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Logging.Interfaces;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;

namespace CES.CoreApi.Logging.Log4Net
{
    public class Log4NetProxy : ILoggerProxy
    {
        #region Core

        /// <summary>
        /// Initializes Log4NetProxy instance
        /// It declared as static to be able to use it effectively without IoC container also
        /// </summary>
        static Log4NetProxy()
        {
            XmlConfigurator.Configure();
            Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

            IsDebugEnabled = Log.IsDebugEnabled;
            IsErrorEnabled = Log.IsErrorEnabled;
            IsFatalEnabled = Log.IsFatalEnabled;
            IsInformationEnabled = Log.IsInfoEnabled;
            IsWarningEnabled = Log.IsWarnEnabled;
            IsNoticeEnabled = Log.Logger.IsEnabledFor(Level.Notice);
        }

        private static readonly ILog Log;

        #endregion //Core

        #region Implementation of ILoggerProxy

        #region Parameter manipulation methods
        

        /// <summary>
        /// Sets parameter for logging
        /// </summary>
        /// <param name="parameterName">Parameter name</param>
        /// <param name="parameterValue">Parameter value</param>
        public void SetParameter(string parameterName, object parameterValue)
        {
            GlobalContext.Properties[parameterName] = parameterValue;
        }

        /// <summary>
        /// Removes parameter for logging
        /// </summary>
        /// <param name="parameterName">Parameter name</param>
        public void RemoveParameter(string parameterName)
        {
            GlobalContext.Properties.Remove(parameterName);
        }

        /// <summary>
        /// Removes all logging parameters
        /// </summary>
        public void ClearParameters()
        {
            GlobalContext.Properties.Clear();
        }

        #endregion

        #region Generic publish  method

        /// <summary>
        /// Publishes message according provided entry type
        /// </summary>
        /// <param name="entryType">Log entry type</param>
        /// <param name="message">Log message</param>
        /// <param name="exception">Exception instance - optional </param>
        public void Publish(LogEntryType entryType, string message, Exception exception = null)
        {
            switch (entryType)
            {
                case LogEntryType.Error:
                    if (IsErrorEnabled)
                    {
                        if (!IsErrorAppenderEnabled)
                            MakeSureAppenderEnabled(entryType);
                        Log.Error(message, exception);
                    }
                    break;

                case LogEntryType.Fatal:
                    if (IsFatalEnabled)
                    {
                        if (!IsFatalAppenderEnabled)
                            MakeSureAppenderEnabled(entryType);
                        Log.Fatal(message, exception);
                    }
                    break;

                case LogEntryType.Information:
                    if (IsInformationEnabled)
                    {
                        if (!IsInformationAppenderEnabled)
                            MakeSureAppenderEnabled(entryType);
                        Log.Info(message, exception);
                    }
                    break;

                case LogEntryType.Warning:
                    if (IsWarningEnabled)
                    {
                        if (!IsWarningAppenderEnabled)
                            MakeSureAppenderEnabled(entryType);
                        Log.Warn(message, exception);
                    }
                    break;

                case LogEntryType.Debug:
                    if (IsDebugEnabled)
                    {
                        if (!IsDebugAppenderEnabled)
                            MakeSureAppenderEnabled(entryType);
                        Log.Debug(message, exception);
                    }
                    break;

                case LogEntryType.Notice:
                    if (IsNoticeEnabled)
                    {
                        if (!IsNoticeAppenderEnabled)
                            MakeSureAppenderEnabled(entryType);
                        Log.Logger.Log(MethodBase.GetCurrentMethod().DeclaringType, Level.Notice, message,
                                       exception);
                    }
                    break;

                default:
                    if (IsInformationEnabled)
                    {
                        if (!IsInformationAppenderEnabled)
                            MakeSureAppenderEnabled(entryType);
                        Log.Info(message, exception);
                    }
                    break;
            }
        }

        #endregion

        #region Publish debug

        /// <summary>
        /// Publishes debug message
        /// </summary>
        /// <param name="message">Log message</param>
        public void PublishDebug(string message)
        {
            Publish(LogEntryType.Debug, message);
        }

        /// <summary>
        /// Formats message using Invariant culture and supplied parameters and publishes DEBUG message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="parameters">Message parameters</param>
        public void PublishDebug(string message, params object[] parameters)
        {
            var formattedMessage = string.Format(CultureInfo.InvariantCulture, message, parameters);
            Publish(LogEntryType.Debug, formattedMessage);
        }

        #endregion

        #region Publish warning
        
        /// <summary>
        /// Publishes warning message
        /// </summary>
        /// <param name="message">Log message</param>
        public void PublishWarning(string message)
        {
            Publish(LogEntryType.Warning, message);
        }

        /// <summary>
        /// Publishes warning message
        /// </summary>
        /// <param name="dataContainer">Data container instance</param>
        public void PublishWarning(IDataContainer dataContainer)
        {
            Publish(LogEntryType.Warning, dataContainer.ToString());
        }

        #endregion

        #region Publish information

        /// <summary>
        /// Publishes INFORMATION message
        /// </summary>
        /// <param name="message">Log message</param>
        public void PublishInformation(string message)
        {
            Publish(LogEntryType.Information, message);
        }

        /// <summary>
        /// Publishes INFORMATION message
        /// </summary>
        /// <param name="dataContainer">Data container instance</param>
        public void PublishInformation(IDataContainer dataContainer)
        {
            if (dataContainer.LogType != LogType.TraceLog && 
                dataContainer.LogType != LogType.EventLog &&
                dataContainer.LogType != LogType.SecurityAudit) return;
            Publish(LogEntryType.Information, dataContainer.ToString());
        }

        /// <summary>
        /// Formats message using Current culture and supplied parameters and publishes INFORMATION message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="parameters">Message parameters</param>
        public void PublishInformation(string message, params object[] parameters)
        {
            var formattedMessage = string.Format(CultureInfo.InvariantCulture, message, parameters);
            Publish(LogEntryType.Information, formattedMessage);
        }

        #endregion

        #region Publish error
        
        /// <summary>
        /// Publishes ERROR message
        /// </summary>
        /// <param name="message">Log message</param>
        public void PublishError(string message)
        {
            Publish(LogEntryType.Error, message);
        }

        /// <summary>
        /// Publishes ERROR message
        /// </summary>
        /// <param name="dataContainer">Data container instance</param>
        public void PublishError(IDataContainer dataContainer)
        {
            if (dataContainer.LogType != LogType.ExceptionLog) return;
            Publish(LogEntryType.Error, dataContainer.ToString());
        }

        /// <summary>
        /// Publishes ERROR message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="exception">Exception instance</param>
        public void PublishError(string message, Exception exception)
        {
            Publish(LogEntryType.Error, message, exception);
        }

        /// <summary>
        /// Formats message using Current culture and supplied parameters and publishes ERROR message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="parameters">Message parameters</param>
        public void PublishError(string message, params object[] parameters)
        {
            if (!IsErrorEnabled) return;
            var formattedMessage = string.Format(CultureInfo.InvariantCulture, message, parameters);
            Publish(LogEntryType.Error, formattedMessage);
        }

        #endregion

        #region Publish fatal message
        
        /// <summary>
        /// Publishes fatal message
        /// </summary>
        /// <param name="message">Log message</param>
        public void PublishFatal(string message)
        {
            Publish(LogEntryType.Fatal, message);
        }

        /// <summary>
        /// Formats message using Current culture and supplied parameters and publishes FATAL message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="parameters">Message parameters</param>
        public void PublishFatal(string message, params object[] parameters)
        {
            var formattedMessage = string.Format(CultureInfo.InvariantCulture, message, parameters);
            Publish(LogEntryType.Fatal, formattedMessage);
        }

        /// <summary>
        /// Formats message using Current culture and supplied parameters and publishes FATAL message
        /// </summary>
        /// <param name="message">Log message</param>
        /// <param name="exception">Exception instance</param>
        public void PublishFatal(string message, Exception exception)
        {
            Publish(LogEntryType.Fatal, message, exception);
        }

        #endregion

        #region Publish notice

        /// <summary>
        /// Publishes NOTICE message
        /// </summary>
        /// <param name="message">Log message</param>
        public void PublishNotice(string message)
        {
            Publish(LogEntryType.Notice, message);
        }

        /// <summary>
        /// Publish MOTICE message 
        /// </summary>
        /// <param name="dataContainer">Data container instance</param>
        public void PublishNotice(IDataContainer dataContainer)
        {
            if (dataContainer.LogType != LogType.DbPerformanceLog) return;
            Publish(LogEntryType.Notice, dataContainer.ToString());
        }

        #endregion

        #region Public properties
        
        /// <summary>
        /// Checks if this logger is enabled for the Debug level.
        /// Returns true if this logger is enabled for Debug events, false otherwise.
        /// </summary>
        public static bool IsDebugEnabled
        {
            get;
            private set;
        }
       
        /// <summary>
        /// Checks if this logger is enabled for the Info level.
        /// Returns true if this logger is enabled for Info events, false otherwise.
        /// </summary>
        public static bool IsInformationEnabled
        {
            get;
            private set;
        }

        /// <summary>
        /// Checks if this logger is enabled for the Warn level.
        /// Returns true if this logger is enabled for Warn events, false otherwise.
        /// </summary>
        public static bool IsWarningEnabled
        {
            get;
            private set;
        }

        /// <summary>
        /// Checks if this logger is enabled for the Error level.
        /// Returns true if this logger is enabled for Error events, false otherwise.
        /// </summary>
        public static bool IsErrorEnabled
        {
            get;
            private set;
        }

        /// <summary>
        /// Checks if this logger is enabled for the Fatal level.
        /// Returns true if this logger is enabled for Fatal events, false otherwise.
        /// </summary>
        public static bool IsFatalEnabled
        {
            get;
            private set;
        }

        /// <summary>
        /// Checks if this logger is enabled for the Notice level.
        /// Returns true if this logger is enabled for Notice events, false otherwise.
        /// </summary>
        public static bool IsNoticeEnabled
        {
            get;
            private set;
        }

        #endregion

        #endregion //Implementation of ILoggerProxy

        #region Private methods

        /// <summary>
        /// Checks if appender enabled, if not enables it by setting appropriate threshold level
        /// </summary>
        /// <param name="entryType">Threshold level</param>
        private void MakeSureAppenderEnabled(LogEntryType entryType)
        {
            var appender = LogManager.GetRepository().GetAppenders()
                .OfType<FileAppender>()
                .FirstOrDefault(fileAppender => !string.IsNullOrEmpty(fileAppender.File) &&
                                                fileAppender.Name.StartsWith(entryType.ToString(),
                                                                             StringComparison.OrdinalIgnoreCase));

            //Found appender can be null - in this case we suppose it is not a FileAppender 
            //and set appropriate property to TRUE, just not to search it every time

            switch (entryType)
            {
                case LogEntryType.Debug:
                    EnableAppender(appender, Level.Debug);
                    IsDebugAppenderEnabled = true;
                    break;

                case LogEntryType.Warning:
                    EnableAppender(appender, Level.Warn);
                    IsWarningAppenderEnabled = true;
                    break;

                case LogEntryType.Error:
                    EnableAppender(appender, Level.Error);
                    IsErrorAppenderEnabled = true;
                    break;

                case LogEntryType.Fatal:
                    EnableAppender(appender, Level.Fatal);
                    IsFatalAppenderEnabled = true;
                    break;

                case LogEntryType.Information:
                    EnableAppender(appender, Level.Info);
                    IsInformationAppenderEnabled = true;
                    break;

                case LogEntryType.Notice:
                    EnableAppender(appender, Level.Notice);
                    IsNoticeAppenderEnabled = true;
                    break;
            }
        }

        /// <summary>
        /// Enables appender
        /// </summary>
        /// <param name="appender">Appender instance</param>
        /// <param name="threshold">Threshold level to enable</param>
        private static void EnableAppender(AppenderSkeleton appender, Level threshold)
        {
            if (appender != null && appender.Threshold == Level.Off)
            {
                appender.Threshold = threshold;
            }
        }

        #endregion

        #region Private properties

        /// <summary>
        /// Internal properties used to check if appender already enable in oreder not to do expensive LINQ search every time
        /// </summary>
        private bool IsErrorAppenderEnabled { get; set; }
        private bool IsFatalAppenderEnabled { get; set; }
        private bool IsInformationAppenderEnabled { get; set; }
        private bool IsWarningAppenderEnabled { get; set; }
        private bool IsDebugAppenderEnabled { get; set; }
        private bool IsNoticeAppenderEnabled { get; set; }
        
        #endregion
    }
}