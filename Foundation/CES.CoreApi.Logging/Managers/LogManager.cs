using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using System.Threading.Tasks;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Managers
{
    public class LogManager : ILogManager
    {
        #region Core

        private readonly IIocContainer _container;
        private readonly ILogConfigurationProvider _configuration;
        private readonly ILog4NetProxy _log4NetProxy;

        /// <summary>
        /// Initializes LogManager instance
        /// </summary>
        /// <param name="container">IoC container instance</param>
        public LogManager(IIocContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");
            _container = container;
            _configuration = _container.Resolve<ILogConfigurationProvider>();
            _log4NetProxy = _container.Resolve<ILog4NetProxy>();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Provides new instance of Log monitor
        /// </summary>
        /// <returns></returns>
        public T GetMonitorInstance<T>() where T : class
        {
            return _container.Resolve<T>();
        }

        /// <summary>
        /// Provides new instance of data container
        /// </summary>
        /// <returns></returns>
        public T GetContainerInstance<T>() where T : class, IDataContainer
        {
            return _container.Resolve<T>();
        }

        /// <summary>
        /// Gets exception log data contianer and populates it by service call details
        /// </summary>
        /// <param name="context">Service operation context</param>
        /// <param name="getClientDetails"></param>
        /// <returns></returns>
        public ExceptionLogDataContainer GetExceptionLogDataContainerWithCallDetails(OperationContext context,
            Func<IDictionary<string, object>> getClientDetails)
        {
            ExceptionLogDataContainer dataContainer = null;

            if (context == null) return null;

            //Calling application should not fail if logging failed
            try
            {
                dataContainer = _container.Resolve<ExceptionLogDataContainer>();

                //Collect web service call details
                var webServiceCallInformationProvider = _container.Resolve<IServiceCallInformationProvider>();
                webServiceCallInformationProvider.AddDetails(dataContainer, context, getClientDetails);
            }
                // ReSharper disable EmptyGeneralCatchClause
            catch (Exception)
                // ReSharper restore EmptyGeneralCatchClause
            {
            }
            return dataContainer;
        }

        /// <summary>
        /// Logs entry data
        /// </summary>
        /// <param name="dataContainer">Logs entry data container</param>
        public void Publish(IDataContainer dataContainer)
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

        public void Publish(Exception exception, string customMessage = null,
            ExceptionLogDataContainer dataContainer = null)
        {
            //Calling application should not fail if logging failed
            try
            {
                customMessage = string.IsNullOrEmpty(customMessage) ? string.Empty : customMessage;
                dataContainer = dataContainer ?? _container.Resolve<ExceptionLogDataContainer>();

                dataContainer.SetException(exception);
                dataContainer.CustomMessage = customMessage;

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
                    _log4NetProxy.PublishInformation(dataContainer);
                    break;

                case LogType.PerformanceLog:
                    _log4NetProxy.PublishWarning(dataContainer);
                    break;

                case LogType.ExceptionLog:
                    _log4NetProxy.PublishError(dataContainer);
                    break;

                case LogType.DbPerformanceLog:
                    _log4NetProxy.PublishNotice(dataContainer);
                    break;
            }
        }

        private bool IsLogEnabled(LogType logType)
        {
            switch (logType)
            {
                case LogType.DbPerformanceLog:
                    return _configuration.DatabasePerformanceLogConfiguration.IsEnabled;

                case LogType.TraceLog:
                    return _configuration.TraceLogConfiguration.IsEnabled;

                case LogType.PerformanceLog:
                    return _configuration.PerformanceLogConfiguration.IsEnabled;

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
                    return _configuration.DatabasePerformanceLogConfiguration.IsAsynchronous;

                case LogType.TraceLog:
                    return _configuration.TraceLogConfiguration.IsAsynchronous;

                case LogType.PerformanceLog:
                    return _configuration.PerformanceLogConfiguration.IsAsynchronous;
            }
            return false;
        }

        #endregion
    }
}
