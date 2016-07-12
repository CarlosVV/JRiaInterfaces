using AutoMapper;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Builders;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Factories;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Parsers;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Processors;
using CES.CoreApi.GeoLocation.Service.Business.Logic.Providers;
//using CES.CoreApi.Logging.Configuration;
//using CES.CoreApi.Logging.Formatters;
//using CES.CoreApi.Logging.Interfaces;
//using CES.CoreApi.Logging.Log4Net;
//using CES.CoreApi.Logging.Models;
//using CES.CoreApi.Logging.Monitors;
//using CES.CoreApi.Logging.Providers;
using SimpleInjector;
using CES.CoreApi.Security.Interfaces;
using CES.CoreApi.Security.Managers;
using CES.CoreApi.Security.Providers;
using CES.CoreApi.Security.WebApi.Interfaces;
using CES.CoreApi.GeoLocation.Service.Business.Contract.Interfaces;

namespace CES.CoreApi.GeoLocation.Api.Configuration
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
            //RegisterLoggging(container);
            RegisterFactories(container);
           // RegisterInterceptions(container);
			RegisterSecurity(container);
            container.Verify();
        }

        private static void RegisterFoundation(Container container)
        {         
            container.Register<IApplicationAuthenticator, ApplicationAuthenticator>();             
            container.Register<IApplicationAuthorizator, ApplicationAuthorizator>();
            container.Register<IIdentityProvider, IdentityProvider>();
        }

  //      private static void RegisterInterceptions(Container container)
  //      {
  //          container.InterceptWith<PerformanceInterceptor>(type => type == typeof(IDataResponseProvider));      
  //          container.InterceptWith<PerformanceInterceptor>(type => type == typeof(IAddressServiceRequestProcessor));
  //          container.InterceptWith<PerformanceInterceptor>(type => type == typeof(IGeocodeServiceRequestProcessor));
  //          container.InterceptWith<PerformanceInterceptor>(type => type == typeof(IMapServiceRequestProcessor));    
			
		//}

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
          //  container.Register<IHealthMonitoringProcessor, HealthMonitoringProcessor>();
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

   //     private static void RegisterLoggging(Container container)
   //     {
   //         //Register common classes
   //         container.Register<ILoggerProxy, Log4NetProxy>();

   //         //Registers common formatters			
   //         container.Register<IDateTimeFormatter, DateTimeFormatter>();
   //         container.Register<IFullMethodNameFormatter, FullMethodNameFormatter>();
   //         container.Register<IDefaultValueFormatter, DefaultValueFormatter>();
   //         container.Register<IJsonDataContainerFormatter, JsonDataContainerFormatter>();

   //         //Exception log related    
			//container.Register<IExceptionLogMonitor, ExceptionLogMonitor>();
			//container.Register<IServiceCallInformationProvider, ServiceCallInformationProvider>();
        
   //         //Performance log related
   //         container.Register<IPerformanceLogMonitor, PerformanceLogMonitor>();

   //         //Trace log related
   //         container.Register<ITraceLogMonitor, TraceLogMonitor>();

   //         //Database performance log related
   //         container.Register<IDatabasePerformanceLogMonitor, DatabasePerformanceLogMonitor>();
   //         container.Register<ISqlQueryFormatter, SqlQueryFormatter>();

			////Register data containers
			//container.RegisterCollection<IDataContainer>(new[] {
			//	typeof(DatabasePerformanceLogDataContainer),
			//	typeof(PerformanceLogDataContainer),
			//	typeof(TraceLogDataContainer),
			//	typeof(ExceptionLogDataContainer),
			//	typeof(SecurityLogDataContainer)});

   //         //Configuration related
   //         container.Register<ILogConfigurationProvider, LogConfigurationProvider>();
   //         //Security logging related
   //         container.Register<ISecurityLogMonitor, SecurityLogMonitor>();
   //     }
		private static void RegisterSecurity(Container container)
		{
			container.Register<IWebApiRequestHeaderParametersService, WebApiRequestHeaderParametersService>();
		}
    }
}
