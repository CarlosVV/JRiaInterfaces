using System.Configuration;
using AutoMapper;
using AutoMapper.Mappers;
using CES.CoreApi.Caching.Providers;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Managers;
using CES.CoreApi.Common.Providers;
using CES.CoreApi.Common.Proxies;
using CES.CoreApi.CustomerVerification.Business.Contract.Interfaces;
using CES.CoreApi.CustomerVerification.Business.Logic.Factories;
using CES.CoreApi.CustomerVerification.Business.Logic.Processors;
using CES.CoreApi.CustomerVerification.Business.Logic.Providers;
using CES.CoreApi.CustomerVerification.Service.Contract.Models;
using CES.CoreApi.CustomerVerification.Service.Interfaces;
using CES.CoreApi.CustomerVerification.Service.Utilities;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Foundation.Data;
using CES.CoreApi.Foundation.Providers;
using CES.CoreApi.Foundation.Security;
using CES.CoreApi.Foundation.Service;
using CES.CoreApi.Foundation.Tools;
using CES.CoreApi.Foundation.Validation;
using CES.CoreApi.Logging.Configuration;
using CES.CoreApi.Logging.Factories;
using CES.CoreApi.Logging.Formatters;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Log4Net;
using CES.CoreApi.Logging.Models;
using CES.CoreApi.Logging.Monitors;
using CES.CoreApi.Logging.Providers;
using CES.CoreApi.SimpleInjectorProxy;
using SimpleInjector;
using IConfigurationProvider = CES.CoreApi.Foundation.Contract.Interfaces.IConfigurationProvider;

namespace CES.CoreApi.CustomerVerification.Service.Configuration
{
    internal class CompositionRoot
    {
        public static void RegisterDependencies(Container container)
        {
            RegisterFoundation(container);
            RegisterAutomapper(container);
            RegisterResponses(container);
            RegisterOthers(container);
            RegisterLoggging(container);
            RegisterInterceptions(container);
            RegisterProcessors(container);
            RegisterProviders(container);
            RegisterDataAccess(container);

            container.Verify();
        }

        private static void RegisterProviders(Container container)
        {
            container.RegisterSingle<ICurrentDateTimeProvider, CurrentDateTimeProvider>();
            container.RegisterSingle<ICountryConfigurationProvider, CountryConfigurationProvider>();

            container.RegisterSingle<IVedaCustomerIdVerificationProvider, VedaCustomerIdVerificationProvider>();
        }

        private static void RegisterProcessors(Container container)
        {
            container.RegisterSingle<ICustomerVerificationRequestProcessor, CustomerVerificationRequestProcessor>();
            container.RegisterSingle<IHealthMonitoringProcessor, HealthMonitoringProcessor>();
        }

        private static void RegisterFoundation(Container container)
        {
            var cacheName = ConfigurationManager.AppSettings["cacheName"];

            container.RegisterSingle<IAuthenticationManager, AuthenticationManager>();
            container.RegisterSingle<IApplicationAuthenticator, ApplicationAuthenticator>();
            container.RegisterSingle<IApplicationRepository, ApplicationRepository>();
            container.RegisterSingle<IApplicationValidator, ApplicationValidator>();
            container.RegisterSingle<IRequestHeadersProvider, RequestHeadersProvider>();
            container.RegisterSingle<IServiceCallHeaderParametersProvider, ServiceCallHeaderParametersProvider>();
            container.RegisterSingle<IAuthorizationManager, AuthorizationManager>();
            container.RegisterSingle<IAuthorizationAdministrator, AuthorizationAdministrator>();
            container.RegisterSingle<ICacheProvider>(() => new AppFabricCacheProvider(container.GetInstance<ILogMonitorFactory>(), container.GetInstance<IIdentityManager>(), cacheName));
            container.RegisterSingle<IHostApplicationProvider, HostApplicationProvider>();
            container.RegisterSingle<IClientSecurityContextProvider, ClientDetailsProvider>();
            container.RegisterSingle<IServiceExceptionHandler, ServiceExceptionHandler>();
            container.RegisterSingle<IAutoMapperProxy, AutoMapperProxy>();
            container.RegisterSingle<IHttpClientProxy, HttpClientProxy>();
            container.RegisterSingle<IConfigurationProvider, ConfigurationProvider>();
            container.RegisterSingle<IServiceConfigurationProvider, ServiceConfigurationProvider>();
            container.RegisterSingle<IIdentityManager, IdentityManager>();
        }

        private static void RegisterAutomapper(Container container)
        {
            container.Register<ITypeMapFactory, TypeMapFactory>();
            container.RegisterAll<IObjectMapper>(MapperRegistry.Mappers);
            container.RegisterSingle<ConfigurationStore>();
            container.Register<IConfiguration>(container.GetInstance<ConfigurationStore>);
            container.Register<AutoMapper.IConfigurationProvider>(container.GetInstance<ConfigurationStore>);
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
                typeof(DatabasePerformanceLogDataContainer),
                typeof(PerformanceLogDataContainer),
                typeof(TraceLogDataContainer),
                typeof(ExceptionLogDataContainer),
                typeof(SecurityLogDataContainer));


            container.RegisterSingle<ILogMonitorFactory>(new LogMonitorFactory
            {
                {"IDatabasePerformanceLogMonitor", container.GetInstance<DatabasePerformanceLogMonitor>},
                {"ITraceLogMonitor", container.GetInstance<TraceLogMonitor>},
                {"IPerformanceLogMonitor", container.GetInstance<PerformanceLogMonitor>},
                {"IExceptionLogMonitor", container.GetInstance<ExceptionLogMonitor>},
                {"ISecurityLogMonitor", container.GetInstance<SecurityLogMonitor>}
            });

            //Configuration related
            container.RegisterSingle<ILogConfigurationProvider, LogConfigurationProvider>();

            //Security logging related
            container.Register<ISecurityLogMonitor, SecurityLogMonitor>();
        }

        private static void RegisterOthers(Container container)
        {
            // When the interceptor (and its dependencies) are thread-safe,
            // it can be registered as singleton to prevent a new instance
            // from being created and each call. When the intercepted service
            // and both the interceptor are both singletons, the returned
            // (proxy) instance will be a singleton as well.
            container.RegisterSingle<PerformanceInterceptor>();

            container.RegisterSingle<IRequestValidator, RequestValidator>();
            container.RegisterSingle<IMappingHelper, MappingHelper>();
            container.RegisterSingle<IExceptionHelper, ExceptionHelper>();

            container.RegisterSingle<IIdVerificationProviderFactory>(new IdVerificationProviderFactory(container.GetInstance<ICountryConfigurationProvider>())
            {
                {"IVedaCustomerIdVerificationProvider", container.GetInstance<VedaCustomerIdVerificationProvider>},
            });
        }

        private static void RegisterDataAccess(Container container)
        {
            //container.RegisterSingle<ICustomerRepository, CustomerRepository>();
            //container.RegisterSingle<IDatabaseConfigurationProvider, DatabaseConfigurationProvider>();
            //container.RegisterSingle<IDatabaseInstanceProvider, DatabaseInstanceProvider>();
            //container.RegisterSingle<IDatabasePingProvider, DatabasePingProvider>();
        }

        private static void RegisterInterceptions(Container container)
        {
            container.InterceptWith<PerformanceInterceptor>(type => type == typeof(IApplicationRepository));
            //container.InterceptWith<PerformanceInterceptor>(type => type == typeof(ICustomerRepository));
            container.InterceptWith<PerformanceInterceptor>(type => type == typeof(IHealthMonitoringProcessor));
            container.InterceptWith<PerformanceInterceptor>(type => type == typeof(ICustomerVerificationRequestProcessor));
        }

        private static void RegisterResponses(Container container)
        {
            container.Register<PingResponse, PingResponse>();
            container.Register<ClearCacheResponse, ClearCacheResponse>();
        }
    }
}
