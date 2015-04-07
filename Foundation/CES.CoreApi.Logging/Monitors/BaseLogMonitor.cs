using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Logging.Monitors
{
    public abstract class BaseLogMonitor
    {
        #region Core

        private readonly ILoggerProxy _logProxy;
        
        protected BaseLogMonitor(ILoggerProxy logProxy, ILogConfigurationProvider configuration)
        {
            if (logProxy == null) throw new ArgumentNullException("logProxy");
            if (configuration == null) throw new ArgumentNullException("configuration");
            _logProxy = logProxy;
            Configuration = configuration;
        }

        #endregion

        #region

        protected ILogConfigurationProvider Configuration { get; private set; }

        /// <summary>
        /// Logs entry data
        /// </summary>
        /// <param name="dataContainer">Logs entry data container</param>
        protected void Publish(IDataContainer dataContainer)
        {
            if (!IsLogEnabled(dataContainer.LogType))
                return;

            //Calling application should not fail if logging failed
            try
            {
                PublishDataContainer(dataContainer);
            }
                // ReSharper disable EmptyGeneralCatchClause
            catch (Exception)
                // ReSharper restore EmptyGeneralCatchClause
            {
            }
        }

        #endregion

        #region private methods

        /// <summary>
        /// Logs entry data
        /// </summary>
        /// <param name="dataContainer">Log entry data container</param>
        private void PublishDataContainer(IDataContainer dataContainer)
        {
            if (Debugger.IsAttached || !IsLogAsynchronous(dataContainer.LogType))
                PublishLogEntry(dataContainer);
            else
                Task.Factory.StartNew(() => PublishLogEntry(dataContainer)); //Save log entry asynchronously
        }

        /// <summary>
        /// Logs entry 
        /// </summary>
        /// <param name="dataContainer">Log entry data container</param>
        private void PublishLogEntry(IDataContainer dataContainer)
        {
            switch (dataContainer.LogType)
            {
                case LogType.TraceLog:
                case LogType.SecurityAudit:
                    _logProxy.PublishInformation(dataContainer);
                    break;

                case LogType.PerformanceLog:
                    _logProxy.PublishWarning(dataContainer);
                    break;

                case LogType.ExceptionLog:
                    _logProxy.PublishError(dataContainer);
                    break;

                case LogType.DbPerformanceLog:
                    _logProxy.PublishNotice(dataContainer);
                    break;
            }
        }

        private bool IsLogEnabled(LogType logType)
        {
            switch (logType)
            {
                case LogType.DbPerformanceLog:
                    return Configuration.DatabasePerformanceLogConfiguration.IsEnabled;

                case LogType.TraceLog:
                    return Configuration.TraceLogConfiguration.IsEnabled;

                case LogType.PerformanceLog:
                    return Configuration.PerformanceLogConfiguration.IsEnabled;

                case LogType.ExceptionLog:
                    return true;

                case LogType.SecurityAudit:
                    return true;
            }
            return false;
        }

        private bool IsLogAsynchronous(LogType logType)
        {
            switch (logType)
            {
                case LogType.DbPerformanceLog:
                    return Configuration.DatabasePerformanceLogConfiguration.IsAsynchronous;

                case LogType.TraceLog:
                    return Configuration.TraceLogConfiguration.IsAsynchronous;

                case LogType.PerformanceLog:
                    return Configuration.PerformanceLogConfiguration.IsAsynchronous;
            }
            return false;
        }

        #endregion
    }
}
