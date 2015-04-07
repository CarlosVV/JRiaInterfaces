using System;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Logging.Formatters;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Log4Net;
using CES.CoreApi.Logging.Models;
using CES.CoreApi.Logging.Monitors;
using CES.CoreApi.Logging.Providers;

namespace CES.CoreApi.Logging.Configuration
{
    public class LogConfigurator
    {
        #region Core
        
        /// <summary>
        /// Initializes LogConfigurator instance
        /// </summary>
        /// <param name="container">IoC container instance</param>
        public LogConfigurator(IIocContainer container)
        {
            if (container == null) 
                throw new ArgumentNullException("container");
            _container = container;
        }

        private readonly IIocContainer _container;

        #endregion //Core

        #region Public methods

        /// <summary>
        /// Configures application logs
        /// </summary>
        /// <param name="logTypes">List of log types to be configured</param>
        public void ConfigureLog(LogType logTypes)
        {
            //Register common classes
            _container.RegisterType<ILoggerProxy, Log4NetProxy>();

            //Registers common formatters
            _container.RegisterType<IFileSizeFormatter, FileSizeFormatter>()
                .RegisterType<IDateTimeFormatter, DateTimeFormatter>()
                .RegisterType<IFullMethodNameFormatter, FullMethodNameFormatter>()
                .RegisterType<IDefaultValueFormatter, DefaultValueFormatter>()
                .RegisterType<IJsonDataContainerFormatter, JsonDataContainerFormatter>();

            //Exception log related
            if ((logTypes & LogType.ExceptionLog) == LogType.ExceptionLog)
            {
                _container
                    .RegisterType<IExceptionLogMonitor, ExceptionLogMonitor>(LifetimeManagerType.AlwaysNew)
                    .RegisterType<IDataContainer, ExceptionLogDataContainer>(LifetimeManagerType.AlwaysNew)
                    .RegisterType<ExceptionLogItemGroup, ExceptionLogItemGroup>(LifetimeManagerType.AlwaysNew)
                    .RegisterType<ExceptionLogItem, ExceptionLogItem>(LifetimeManagerType.AlwaysNew)
                    .RegisterType<IServiceCallInformationProvider, ServiceCallInformationProvider>()
                    .RegisterType<IRemoteClientInformationProvider, RemoteClientInformationProvider>()
                    .RegisterType<IHttpRequestInformationProvider, HttpRequestInformationProvider>()
                    .RegisterType<IServerInformationProvider, ServerInformationProvider>();
            }

             //Performance log related
             if ((logTypes & LogType.PerformanceLog) == LogType.PerformanceLog)
             {
                 _container
                     .RegisterType<IPerformanceLogMonitor, PerformanceLogMonitor>(LifetimeManagerType.AlwaysNew)
                     .RegisterType<IDataContainer, PerformanceLogDataContainer>(LifetimeManagerType.AlwaysNew);
             }

             //Trace log related
             if ((logTypes & LogType.TraceLog) == LogType.TraceLog)
             {
                 _container
                     .RegisterType<ITraceLogMonitor, TraceLogMonitor>(LifetimeManagerType.AlwaysNew)
                     .RegisterType<IDataContainer, TraceLogDataContainer>(LifetimeManagerType.AlwaysNew);
             }

             //Database performance log related
             if ((logTypes & LogType.DbPerformanceLog) == LogType.DbPerformanceLog)
             {
                 _container
                     .RegisterType<IDatabasePerformanceLogMonitor, DatabasePerformanceLogMonitor>(LifetimeManagerType.AlwaysNew)
                     .RegisterType<IDataContainer, DatabasePerformanceLogDataContainer>(LifetimeManagerType.AlwaysNew)
                     .RegisterType<ISqlQueryFormatter, SqlQueryFormatter>();
             }

            //Security audit log related
            if ((logTypes & LogType.SecurityAudit) == LogType.SecurityAudit)
            {
                _container.RegisterType<IDataContainer, SecurityLogDataContainer>(LifetimeManagerType.AlwaysNew);
                _container.RegisterType<ISecurityLogMonitor, SecurityLogMonitor>(LifetimeManagerType.AlwaysNew);
            }

            //Configuration related
            _container
                .RegisterType<ILogConfigurationProvider, LogConfigurationProvider>();
        }

        #endregion //Public methods
    }
}