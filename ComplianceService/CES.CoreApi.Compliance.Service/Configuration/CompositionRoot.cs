using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Foundation.Service;
using CES.CoreApi.Compliance.Service.Business.Contract.Interfaces;
using CES.CoreApi.Compliance.Service.Business.Logic.Processors;
using CES.CoreApi.Compliance.Service.Business.Logic.Provider;
using CES.CoreApi.Compliance.Service.Business.Logic.Factories;
using CES.CoreApi.Compliance.Service.Contract.Interfaces;
using CES.CoreApi.Compliance.Service.Contract.Models;
using CES.CoreApi.Compliance.Service.Utilites;
using CES.CoreApi.SimpleInjectorProxy;
using SimpleInjector;
using CES.CoreApi.Security.Interfaces;
using CES.CoreApi.Security;
using CES.CoreApi.Foundation.Repositories;
using CES.CoreApi.Caching.Providers;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Log4Net;
using CES.CoreApi.Logging.Models;
using CES.CoreApi.Logging.Formatters;
using CES.CoreApi.Logging.Monitors;
using CES.CoreApi.Logging.Providers;
using CES.CoreApi.Logging.Factories;
using CES.CoreApi.Logging.Configuration;
using AutoMapper;

namespace CES.CoreApi.Compliance.Service.Configuration
{
	class CompositionRoot
    {
        public static void RegisterDependencies(Container container)
        {
            RegisterFoundation(container);
            RegisterAutomapper(container);
            RegisterProcessors(container);
            RegisterProviders(container);
            RegisterResponses(container);
            RegisterOthers(container);
            RegisterLoggging(container);
            RegisterInterceptions(container);
         

            container.Verify();
        }

        private static void RegisterFoundation(Container container)
        {

			container.Register<IAuthenticationManager, AuthenticationManager>();
			container.Register<IApplicationAuthenticator, ApplicationAuthenticator>();
			container.Register<IApplicationRepository, ApplicationRepository>();
			container.Register<IAuthorizationManager, AuthorizationManager>();
			container.Register<IApplicationAuthorizator, ApplicationAuthorizator>();
			container.Register<Caching.Interfaces.ICacheProvider>(() => new RedisCacheProvider());
			container.Register<IServiceExceptionHandler, ServiceExceptionHandler>();
			//container.Register<IConfigurationProvider, ConfigurationProvider>();
			container.Register<IIdentityManager, IdentityManager>();
		}

        private static void RegisterInterceptions(Container container)
        {
            //container.InterceptWith<PerformanceInterceptor>(type => type == typeof(IDataResponseProvider));
            //container.InterceptWith<PerformanceInterceptor>(type => type == typeof(IHealthMonitoringProcessor));
            //container.InterceptWith<PerformanceInterceptor>(type => type == typeof(IAddressServiceRequestProcessor));
            //container.InterceptWith<PerformanceInterceptor>(type => type == typeof(IGeocodeServiceRequestProcessor));
            //container.InterceptWith<PerformanceInterceptor>(type => type == typeof(IMapServiceRequestProcessor));
            //container.InterceptWith<PerformanceInterceptor>(type => type == typeof(IClientSideSupportServiceProcessor));
            container.InterceptWith<PerformanceInterceptor>(type => type == typeof(IApplicationRepository));
        }

        private static void RegisterAutomapper(Container container)
        {
			
			var config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new MapperConfiguratorProfile());
				cfg.ConstructServicesUsing(type => container.GetInstance(type));
			});
			container.RegisterSingleton(config);
			container.Register(() => config.CreateMapper(container.GetInstance));

		}

        private static void RegisterOthers(Container container)
        {
            // When the interceptor (and its dependencies) are thread-safe,
            // it can be registered as singleton to prevent a new instance
            // from being created and each call. When the intercepted service
            // and both the interceptor are both singletons, the returned
            // (proxy) instance will be a singleton as well.
            //container.RegisterSingle<PerformanceInterceptor>();

            container.Register<IRequestValidator, RequestValidator>();
           
        }
        
        private static void RegisterProcessors(Container container)
        {
            container.Register<ICheckPayoutRequestProcessor, CheckPayoutRequestProcessor>();
           
        }
        
        private static void RegisterProviders(Container container)
        {
          

            container.RegisterCollection<ICheckPayoutServiceProvider>(new[] {
			   typeof(RiaCheckPayoutServiceProvider),
			   typeof(RiaCheckPayoutServiceProvider)});
        }

        private static void RegisterFactories(Container container)
        {
            container.RegisterSingleton<ICheckPayoutProviderFactory>(new CheckPayoutProviderFactory
            {
                {"IRiaCheckPayoutProvider",container.GetInstance<RiaCheckPayoutServiceProvider>},
                {"INiceCheckPayoutProvider", container.GetInstance<NiceCheckPayoutServiceProvider>}
            });
        }

        private static void RegisterResponses(Container container)
        {
            //container.Register<ValidateAddressResponse, ValidateAddressResponse>();
            //container.Register<ClearCacheResponse, ClearCacheResponse>();
            //container.Register<AutocompleteAddressResponse, AutocompleteAddressResponse>();
            container.Register<CheckOrderResponse, CheckOrderResponse>();
            //container.Register<PingResponse, PingResponse>();
            //container.Register<GetMapResponse, GetMapResponse>();
        }

        private static void RegisterLoggging(Container container)
        {
            //Register common classes
            container.Register<ILoggerProxy, Log4NetProxy>();

            //Registers common formatters
            container.Register<IFileSizeFormatter, FileSizeFormatter>();
            container.Register<IDateTimeFormatter, DateTimeFormatter>();
            container.Register<IFullMethodNameFormatter, FullMethodNameFormatter>();
            container.Register<IDefaultValueFormatter, DefaultValueFormatter>();
            container.Register<IJsonDataContainerFormatter, JsonDataContainerFormatter>();

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
            container.Register<ISqlQueryFormatter, SqlQueryFormatter>();

            //Register data containers
            container.RegisterCollection<IDataContainer>( new []{
				typeof(DatabasePerformanceLogDataContainer),
				typeof(PerformanceLogDataContainer),
				typeof(TraceLogDataContainer),
				typeof(ExceptionLogDataContainer),
				typeof(SecurityLogDataContainer) });


            container.RegisterSingleton<ILogMonitorFactory>(new LogMonitorFactory
            {
                {"IDatabasePerformanceLogMonitor", container.GetInstance<DatabasePerformanceLogMonitor>},
                {"ITraceLogMonitor", container.GetInstance<TraceLogMonitor>},
                {"IPerformanceLogMonitor", container.GetInstance<PerformanceLogMonitor>},
                {"IExceptionLogMonitor", container.GetInstance<ExceptionLogMonitor>},
                {"ISecurityLogMonitor", container.GetInstance<SecurityLogMonitor>}
            });

            //Configuration related
            container.Register<ILogConfigurationProvider, LogConfigurationProvider>();

            //Security logging related
            container.Register<ISecurityLogMonitor, SecurityLogMonitor>();
        }

        
    }
}
