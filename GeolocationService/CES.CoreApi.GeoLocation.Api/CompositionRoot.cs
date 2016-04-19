using SimpleInjector;
using CES.CoreApi.GeoLocation.Api.Models;
using System.Configuration;
using CES.CoreApi.SimpleInjectorProxy;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.GeoLocation.Service.Contract.Interfaces;
using CES.CoreApi.Foundation.Tools;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Factories;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Builders;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Parsers;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Processors;
using CES.CoreApi.Security.Interfaces;
using CES.CoreApi.Security;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Proxies;
using CES.CoreApi.Foundation.Providers;
using CES.CoreApi.Data.Repositories;
using CES.CoreApi.Caching.Providers;
using CES.CoreApi.Foundation.Service;
using AutoMapper;
using AutoMapper.Mappers;
using CES.CoreApi.GeoLocation.Service.Utilities;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Providers;
using CES.CoreApi.GeoLocation.Service.Contract.Models;
using CES.CoreApi.Logging.Log4Net;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Models;
using CES.CoreApi.Logging.Providers;
using CES.CoreApi.Logging.Monitors;
using CES.CoreApi.Logging.Formatters;
using CES.CoreApi.Logging.Factories;
using CES.CoreApi.Logging.Configuration;
using CES.CoreApi.Security.Factories;
using CES.CoreApi.Foundation.Data.Providers;
using CES.CoreApi.Foundation.Data.Interfaces;
using CES.CoreApi.Security.Wcf.Interfaces;
using CES.CoreApi.Security.Wcf;

namespace CES.CoreApi.GeoLocation.Api
{
	public class CompositionRoot
    {
        public static void RegisterDependencies(Container container)
		{
			container.Register<IUserRepository, UserRepository>(Lifestyle.Scoped);

			RegisterFoundation(container);
			RegisterAutomapper(container);
			RegisterProcessors(container);
			RegisterUrlBuilders(container);
			RegisterParsers(container);
			RegisterProviders(container);
			RegisterResponses(container);
			RegisterOthers(container);
			RegisterLoggging(container);
			RegisterSecurity(container);
			RegisterFactories(container);
			RegisterInterceptions(container);

			container.Verify();
		}

		private static void RegisterFoundation(Container container)
		{
			var cacheName = ConfigurationManager.AppSettings["cacheName"];
			container.RegisterSingleton<IAuthenticationManager, AuthenticationManager>();
			container.RegisterSingleton<IApplicationAuthenticator, ApplicationAuthenticator>();
			container.RegisterSingleton<IApplicationRepository, ApplicationRepository>();
			container.RegisterSingleton<IAuthorizationManager, AuthorizationManager>();
			container.RegisterSingleton<IApplicationAuthorizator, ApplicationAuthorizator>();
			container.RegisterSingleton<Caching.Interfaces.ICacheProvider>(() => new RedisCacheProvider());
			container.RegisterSingleton<IClientSecurityContextProvider, ClientDetailsProvider>();
			container.RegisterSingleton<IServiceExceptionHandler, ServiceExceptionHandler>();
			container.RegisterSingleton<IAutoMapperProxy, AutoMapperProxy>();
			container.RegisterSingleton<Foundation.Contract.Interfaces.IConfigurationProvider, ConfigurationProvider>();
			container.RegisterSingleton<IIdentityManager, IdentityManager>();
		}

		private static void RegisterInterceptions(Container container)
		{
			container.InterceptWith<PerformanceInterceptor>(type => type == typeof(IDataResponseProvider));
			container.InterceptWith<PerformanceInterceptor>(type => type == typeof(IHealthMonitoringProcessor));
			container.InterceptWith<PerformanceInterceptor>(type => type == typeof(IAddressServiceRequestProcessor));
			container.InterceptWith<PerformanceInterceptor>(type => type == typeof(IGeocodeServiceRequestProcessor));
			container.InterceptWith<PerformanceInterceptor>(type => type == typeof(IMapServiceRequestProcessor));
			container.InterceptWith<PerformanceInterceptor>(type => type == typeof(IApplicationRepository));
			container.InterceptWith<SecurityLogMonitorInterceptor>(type => type == typeof(IHealthMonitoringProcessor));

		}

		private static void RegisterAutomapper(Container container)
		{
			container.Register<ITypeMapFactory, TypeMapFactory>();
			container.RegisterCollection<IObjectMapper>(MapperRegistry.Mappers);
		}

		private static void RegisterOthers(Container container)
		{
			// When the interceptor (and its dependencies) are thread-safe,
			// it can be registered as singleton to prevent a new instance
			// from being created and each call. When the intercepted service
			// and both the interceptor are both singletons, the returned
			// (proxy) instance will be a singleton as well.
			container.RegisterSingleton<PerformanceInterceptor>();
			container.RegisterSingleton<SecurityLogMonitorInterceptor>();
			container.RegisterSingleton<IRequestValidator, RequestValidator>();
			container.RegisterSingleton<IMappingHelper, MappingHelper>();
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
			container.RegisterSingleton<IHealthMonitoringProcessor, HealthMonitoringProcessor>();
			container.RegisterSingleton<IAddressServiceRequestProcessor, AddressServiceRequestProcessor>();
			container.RegisterSingleton<IGeocodeServiceRequestProcessor, GeocodeServiceRequestProcessor>();
			container.RegisterSingleton<IMapServiceRequestProcessor, MapServiceRequestProcessor>();

		}

		private static void RegisterUrlBuilders(Container container)
		{
			container.RegisterCollection<IUrlBuilder>(
				new[] {
					typeof(BingUrlBuilder),
					typeof(GoogleUrlBuilder),
					typeof(MelissaUrlBuilder)
				});
		}

		private static void RegisterParsers(Container container)
		{
			container.RegisterCollection<IResponseParser>(
				new[] {
					typeof(BingResponseParser),
					typeof(GoogleResponseParser),
					typeof(MelissaResponseParser)
				});

			container.RegisterSingleton<IBingAddressParser, BingAddressParser>();
			container.RegisterSingleton<IMelissaAddressParser, MelissaAddressParser>();
			container.RegisterSingleton<IAddressQueryBuilder, AddressQueryBuilder>();
			container.RegisterSingleton<IGoogleAddressParser, GoogleAddressParser>();
		}

		private static void RegisterProviders(Container container)
		{
			container.RegisterSingleton<IAddressVerificationDataProvider, AddressVerificationDataProvider>();
			container.RegisterSingleton<ICountryConfigurationProvider, CountryConfigurationProvider>();
			container.RegisterSingleton<IMappingDataProvider, MappingDataProvider>();
			container.RegisterSingleton<IDataResponseProvider, DataResponseProvider>();
			container.RegisterSingleton<IMelissaLevelOfConfidenceProvider, MelissaLevelOfConfidenceProvider>();
			container.RegisterSingleton<IGoogleLevelOfConfidenceProvider, GoogleLevelOfConfidenceProvider>();
			container.RegisterSingleton<IAddressAutocompleteDataProvider, AddressAutocompleteDataProvider>();
			container.RegisterSingleton<IGeocodeAddressDataProvider, GeocodeAddressDataProvider>();
			container.RegisterSingleton<IBingPushPinParameterProvider, BingPushPinParameterProvider>();
			container.RegisterSingleton<IGooglePushPinParameterProvider, GooglePushPinParameterProvider>();
			container.RegisterSingleton<ICorrectImageSizeProvider, CorrectImageSizeProvider>();
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
			container.RegisterSingleton<ILoggerProxy, Log4NetProxy>();

			//Registers common formatters
			container.RegisterSingleton<IFileSizeFormatter, FileSizeFormatter>();
			container.RegisterSingleton<IDateTimeFormatter, DateTimeFormatter>();
			container.RegisterSingleton<IFullMethodNameFormatter, FullMethodNameFormatter>();
			container.RegisterSingleton<IDefaultValueFormatter, DefaultValueFormatter>();
			container.RegisterSingleton<IJsonDataContainerFormatter, JsonDataContainerFormatter>();

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
			container.RegisterSingleton<ISqlQueryFormatter, SqlQueryFormatter>();

			//Register data containers
			container.RegisterCollection<IDataContainer>(
				new[] {
					typeof(DatabasePerformanceLogDataContainer),
					typeof(PerformanceLogDataContainer),
					typeof(TraceLogDataContainer),
					typeof(ExceptionLogDataContainer),
					typeof(SecurityLogDataContainer)
				});


			container.RegisterSingleton<ILogMonitorFactory>(new LogMonitorFactory
			{
				{"IDatabasePerformanceLogMonitor", container.GetInstance<DatabasePerformanceLogMonitor>},
				{"ITraceLogMonitor", container.GetInstance<TraceLogMonitor>},
				{"IPerformanceLogMonitor", container.GetInstance<PerformanceLogMonitor>},
				{"IExceptionLogMonitor", container.GetInstance<ExceptionLogMonitor>},
				{"ISecurityLogMonitor", container.GetInstance<SecurityLogMonitor>}
			});

			//Configuration related
			container.RegisterSingleton<ILogConfigurationProvider, LogConfigurationProvider>();

			//Security logging related
			container.Register<ISecurityLogMonitor, SecurityLogMonitor>();
		}

		private static void RegisterSecurity(Container container)
		{
			container.RegisterSingleton<IRequestHeaderParametersProviderFactory>(new RequestHeaderParametersProviderFactory
			{
				{"IWcfRequestHeaderParametersProvider", container.GetInstance<WcfRequestHeaderParametersProvider>},
				{"IWebApiRequestHeaderParametersProvider", container.GetInstance<WebApiRequestHeaderParametersProvider>}
			});
		}
	}
}
