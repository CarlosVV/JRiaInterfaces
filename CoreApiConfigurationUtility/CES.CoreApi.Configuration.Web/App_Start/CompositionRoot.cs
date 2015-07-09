using System.Configuration;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.Mappers;
using CES.CoreApi.Caching.Providers;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Managers;
using CES.CoreApi.Common.Providers;
using CES.CoreApi.Common.Proxies;
using CES.CoreApi.Configuration.Business.Services;
using CES.CoreApi.Configuration.Data;
using CES.CoreApi.Configuration.Model.Interfaces;
using CES.CoreApi.Logging.Configuration;
using CES.CoreApi.Logging.Factories;
using CES.CoreApi.Logging.Formatters;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Log4Net;
using CES.CoreApi.Logging.Models;
using CES.CoreApi.Logging.Monitors;
using CES.CoreApi.Logging.Providers;
using CES.CoreApi.Logging.Utilities;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;

namespace CES.CoreApi.Configuration.Web
{
    public class CompositionRoot
    {
        public static void RegisterDependencies()
        {
            var container = new Container();

            RegisterMvcControllers(container);
            RegisterRepositories(container);
            RegisterManagers(container);
            RegisterFilters(container);
            RegisterAutomapper(container);
            RegisterOthers(container);
            RegisterLoggging(container);

            container.Verify();

            SetResolver(container);
        }

        private static void RegisterAutomapper(Container container)
        {
            container.Register<ITypeMapFactory, TypeMapFactory>();
            container.RegisterAll<IObjectMapper>(MapperRegistry.Mappers);
            container.RegisterSingle<ConfigurationStore>();
            container.Register<IConfiguration>(container.GetInstance<ConfigurationStore>);
            container.Register<IConfigurationProvider>(container.GetInstance<ConfigurationStore>);
        }

        private static void SetResolver(Container container)
        {
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }

        private static void RegisterFilters(Container container)
        {
            container.RegisterMvcIntegratedFilterProvider();
        }

        private static void RegisterMvcControllers(Container container)
        {
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
        }
        
        private static void RegisterRepositories(Container container)
        {
            container.RegisterSingle<IServicesRepository, ServicesRepository>();
            container.RegisterSingle<ISettingsRepository, SettingsRepository>();
        }

        private static void RegisterManagers(Container container)
        {
            container.RegisterSingle<IServiceManager, ServiceManager>();
            container.RegisterSingle<ISettingManager, SettingManager>();
        }

        private static void RegisterOthers(Container container)
        {
            var cacheName = ConfigurationManager.AppSettings["cacheName"];

            container.RegisterSingle<IIdentityManager, IdentityManager>();
            container.RegisterSingle<IAutoMapperProxy, AutoMapperProxy>();
            container.RegisterSingle<ICacheProvider>(
                () =>
                    new AppFabricCacheProvider(container.GetInstance<ILogMonitorFactory>(),
                        container.GetInstance<IIdentityManager>(), cacheName));
            container.RegisterSingle<ICurrentDateTimeProvider, CurrentDateTimeProvider>();
        }

        private static void RegisterLoggging(Container container)
        {
            //Register common classes
            container.RegisterSingle<ILoggerProxy, Log4NetProxy>();

            //Registers common formatters
            container.RegisterSingle<IFileSizeFormatter, FileSizeFormatter>();
            container.RegisterSingle<IDateTimeFormatter, DateTimeFormatter>();
            container.RegisterSingle<IFullMethodNameFormatter, FullMethodNameFormatter>();
            container.RegisterSingle<IDefaultValueFormatter, DefaultValueFormatter>();
            container.RegisterSingle<IJsonDataContainerFormatter, JsonDataContainerFormatter>();

            //Exception log related
            container.Register<IExceptionLogMonitor, ExceptionLogMonitor>();
            container.Register<ExceptionLogItemGroup>();
            container.Register<ExceptionLogItem>();
            container.Register<IServiceCallInformationProvider, ServiceCallInformationProvider>();
            container.Register<IRemoteClientInformationProvider, RemoteClientInformationProvider>();
            container.Register<IHttpRequestInformationProvider, HttpRequestInformationProvider>();
            container.Register<IServerInformationProvider, ServerInformationProvider>();
            
            //Performance log related
            container.Register<IPerformanceLogMonitor, PerformanceLogMonitor>();
            
            //Trace log related
            container.Register<ITraceLogMonitor, TraceLogMonitor>();
            
            //Database performance log related
            container.Register<IDatabasePerformanceLogMonitor, DatabasePerformanceLogMonitor>();
            container.RegisterSingle<ISqlQueryFormatter, SqlQueryFormatter>();
        
            //Register data containers
            container.RegisterAll<IDataContainer>(
                typeof (DatabasePerformanceLogDataContainer),
                typeof (PerformanceLogDataContainer),
                typeof (TraceLogDataContainer),
                typeof (ExceptionLogDataContainer));

            
            container.RegisterSingle<ILogMonitorFactory>(new LogMonitorFactory
            {
                {"IDatabasePerformanceLogMonitor", container.GetInstance<DatabasePerformanceLogMonitor>},
                {"ITraceLogMonitor", container.GetInstance<TraceLogMonitor>},
                {"IPerformanceLogMonitor", container.GetInstance<PerformanceLogMonitor>},
                {"IExceptionLogMonitor", container.GetInstance<ExceptionLogMonitor>}
            });

            //Configuration related
            container.RegisterSingle<ILogConfigurationProvider, LogConfigurationProvider>();

            //Web API exception logger
            container.RegisterSingle<IExceptionLogger, WebApiGlobalExceptionLogger>();
            container.RegisterSingle<IWebApiCallInformationProvider, WebApiCallInformationProvider>();
        }
    }
}