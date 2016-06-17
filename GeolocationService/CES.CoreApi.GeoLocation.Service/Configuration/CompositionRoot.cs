using AutoMapper;
using AutoMapper.Mappers;
using CES.CoreApi.Common.Interfaces;
//using CES.CoreApi.Foundation.Contract.Interfaces;
//using CES.CoreApi.Foundation.Providers;
//using CES.CoreApi.Foundation.Service;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Builders;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Factories;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Parsers;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Processors;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Providers;
using CES.CoreApi.GeoLocation.Service.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Contract.Models;
using CES.CoreApi.GeoLocation.Service.Utilities;
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
using CES.CoreApi.Caching.Providers;
using CES.CoreApi.Security.Interfaces;
using CES.CoreApi.Security;
using CES.CoreApi.Security.Wcf.Interfaces;
using CES.CoreApi.Security.Wcf.Managers;
using CES.CoreApi.Security.Wcf.Services;
using CES.CoreApi.Security.Managers;
using CES.CoreApi.Security.Providers;
//using CES.CoreApi.Foundation.Service;
//using CES.CoreApi.Foundation.Providers;
//using CES.CoreApi.Security.Providers;
//using CES.CoreApi.Security.WebApi.Interfaces;
//using CES.CoreApi.Foundation.Repositories;

namespace CES.CoreApi.GeoLocation.Service.Configuration
{
	public class CompositionRoot
    {
        public static void RegisterDependencies(Container container)
        {
            RegisterFoundation(container);
            RegisterAutomapper(container);
            RegisterProcessors(container);
            RegisterUrlBuilders(container);
            RegisterParsers(container);
            RegisterProviders(container);
            RegisterResponses(container);
            RegisterOthers(container);
            RegisterLoggging(container);
            RegisterFactories(container);
			RegisterSecurity(container);
            RegisterInterceptions(container);
            container.Verify();
        }

        private static void RegisterFoundation(Container container)
		{
		
			container.Register<IAuthenticationManager, AuthenticationManager>();
            container.Register<IApplicationAuthenticator, ApplicationAuthenticator>();
            container.Register<Foundation.Contract.Interfaces.IApplicationRepository, Foundation.Repositories.ApplicationRepository>();
			container.Register<IAuthorizationManager, AuthorizationManager>();
			container.Register<IApplicationAuthorizator, ApplicationAuthorizator>();
			container.Register<CES.CoreApi.Security.Interfaces.IIdentityProvider, CES.CoreApi.Security.Providers.IdentityProvider>();

			container.Register<Foundation.Contract.Interfaces.IIdentityProvider, CES.CoreApi.Foundation.Providers.IdentityProvider>();
			container.Register<Caching.Interfaces.ICacheProvider>(() => new RedisCacheProvider());   
            container.Register<Foundation.Contract.Interfaces.IServiceExceptionHandler, Foundation.Service.ServiceExceptionHandler>();        
			//container.Register<IConfigurationProvider, ConfigurationProvider>();
          
        }
		private static void RegisterFoundation2(Container container)
		{
			container.Register<IApplicationAuthenticator, ApplicationAuthenticator>();
			container.Register<IApplicationAuthorizator, ApplicationAuthorizator>();
			container.Register<Caching.Interfaces.ICacheProvider>(() => new RedisCacheProvider());
			//  container.Register<Foundation.Contract.Interfaces.IServiceExceptionHandler, Foundation.Service.ServiceExceptionHandler>();       
			container.Register<IIdentityProvider, IdentityProvider>();
		}

		private static void RegisterInterceptions(Container container)
        {
            container.InterceptWith<PerformanceInterceptor>(type => type == typeof(IDataResponseProvider));
            container.InterceptWith<PerformanceInterceptor>(type => type == typeof(IHealthMonitoringProcessor));
            container.InterceptWith<PerformanceInterceptor>(type => type == typeof(IAddressServiceRequestProcessor));
            container.InterceptWith<PerformanceInterceptor>(type => type == typeof(IGeocodeServiceRequestProcessor));
            container.InterceptWith<PerformanceInterceptor>(type => type == typeof(IMapServiceRequestProcessor));
           //container.InterceptWith<PerformanceInterceptor>(type => type == typeof(IApplicationRepository));
			container.InterceptWith<SecurityLogMonitorInterceptor>(type => type == typeof(IHealthMonitoringProcessor));
			
		}

        private static void RegisterAutomapper(Container container)
        {			

			var config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new GeoLocationMapperProfile());
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
            container.Register<PerformanceInterceptor>();
			container.Register<SecurityLogMonitorInterceptor>();
			container.Register<IRequestValidator, RequestValidator>();
        
        }

        private static void RegisterFactories(Container container)
        {
            container.RegisterSingleton<IUrlBuilderFactory>(new UrlBuilderFactory
            {
                {"IBingUrlBuilder", container.GetInstance<BingUrlBuilder>},
                {"IGoogleUrlBuilder", container.GetInstance<GoogleUrlBuilder>},
                {"IMelissaDataUrlBuilder", container.GetInstance<MelissaUrlBuilder>}
            });

            container.RegisterSingleton<IResponseParserFactory>(new ResponseParserFactory
            {
                {"IBingResponseParser", container.GetInstance<BingResponseParser>},
                {"IGoogleResponseParser", container.GetInstance<GoogleResponseParser>},
                {"IMelissaDataResponseParser", container.GetInstance<MelissaResponseParser>}
            });
        }

        private static void RegisterProcessors(Container container)
        {
            container.Register<IHealthMonitoringProcessor, HealthMonitoringProcessor>();
            container.Register<IAddressServiceRequestProcessor, AddressServiceRequestProcessor>();
            container.Register<IGeocodeServiceRequestProcessor, GeocodeServiceRequestProcessor>();
            container.Register<IMapServiceRequestProcessor, MapServiceRequestProcessor>();
          
        }

        private static void RegisterUrlBuilders(Container container)
        {
            container.RegisterCollection<IUrlBuilder>(new[] {
				typeof(BingUrlBuilder),
				typeof(GoogleUrlBuilder),
				typeof(MelissaUrlBuilder)});
        }

        private static void RegisterParsers(Container container)
        {
            container.RegisterCollection<IResponseParser>(new[] {
				typeof(BingResponseParser),
				typeof(GoogleResponseParser),
				typeof(MelissaResponseParser) });

            container.Register<IBingAddressParser, BingAddressParser>();
            container.Register<IMelissaAddressParser, MelissaAddressParser>();
            container.Register<IAddressQueryBuilder, AddressQueryBuilder>();
            container.Register<IGoogleAddressParser, GoogleAddressParser>();
        }

        private static void RegisterProviders(Container container)
        {
            container.Register<IAddressVerificationDataProvider, AddressVerificationDataProvider>();
           // container.Register<ICountryConfigurationProvider, CountryConfigurationProvider>();
            container.Register<IMappingDataProvider, MappingDataProvider>();
            container.Register<IDataResponseProvider, DataResponseProvider>();
            container.Register<IMelissaLevelOfConfidenceProvider, MelissaLevelOfConfidenceProvider>();
            container.Register<IGoogleLevelOfConfidenceProvider, GoogleLevelOfConfidenceProvider>();
         
            container.Register<IAddressAutocompleteDataProvider, AddressAutocompleteDataProvider>();
            container.Register<IGeocodeAddressDataProvider, GeocodeAddressDataProvider>();
            container.Register<IBingPushPinParameterProvider, BingPushPinParameterProvider>();
            container.Register<IGooglePushPinParameterProvider, GooglePushPinParameterProvider>();
            container.Register<ICorrectImageSizeProvider, CorrectImageSizeProvider>();
        }

        private static void RegisterResponses(Container container)
        {
            container.Register<ValidateAddressResponse, ValidateAddressResponse>();
            container.Register<ClearCacheResponse, ClearCacheResponse>();
            container.Register<AutocompleteAddressResponse, AutocompleteAddressResponse>();
            container.Register<PingResponse, PingResponse>();
            container.Register<GetMapResponse, GetMapResponse>();
        }

        private static void RegisterLoggging(Container container)
        {
            //Register common classes
            container.Register<ILoggerProxy, Log4NetProxy>();

            //Registers common formatters
			//container.Register<IFileSizeFormatter, FileSizeFormatter>();
            container.Register<IDateTimeFormatter, DateTimeFormatter>();
            container.Register<IFullMethodNameFormatter, FullMethodNameFormatter>();
            container.Register<IDefaultValueFormatter, DefaultValueFormatter>();
            container.Register<IJsonDataContainerFormatter, JsonDataContainerFormatter>();

            //Exception log related
            container.Register<IExceptionLogMonitor, ExceptionLogMonitor>();
            container.Register<ExceptionLogItemGroup>();
            container.Register<ExceptionLogItem>();
            container.Register<IServiceCallInformationProvider, ServiceCallInformationProvider>();
            //container.Register<IRemoteClientInformationProvider, RemoteClientInformationProvider>();
            //container.Register<IHttpRequestInformationProvider, HttpRequestInformationProvider>();
            //container.Register<IServerInformationProvider, ServerInformationProvider>();


            //Performance log related
            container.Register<IPerformanceLogMonitor, PerformanceLogMonitor>();

            //Trace log related
            container.Register<ITraceLogMonitor, TraceLogMonitor>();

            //Database performance log related
            container.Register<IDatabasePerformanceLogMonitor, DatabasePerformanceLogMonitor>();
            container.Register<ISqlQueryFormatter, SqlQueryFormatter>();

			//Register data containers
			container.RegisterCollection<IDataContainer>(new[] {
				typeof(DatabasePerformanceLogDataContainer),
				typeof(PerformanceLogDataContainer),
				typeof(TraceLogDataContainer),
				typeof(ExceptionLogDataContainer),
				typeof(SecurityLogDataContainer)});

            //Configuration related
            container.Register<ILogConfigurationProvider, LogConfigurationProvider>();

            //Security logging related
            container.Register<ISecurityLogMonitor, SecurityLogMonitor>();
        }

		private static void RegisterSecurity(Container container)
		{
			container.Register<IWcfRequestHeaderParametersService, WcfRequestHeaderParametersService>();
			//container.Register<IWebApiRequestHeaderParametersService, WebApiRequestHeaderParametersService>();
		}
    }
}
