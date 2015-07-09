using System;
using System.Configuration;
using System.Linq;
using AutoMapper;
using AutoMapper.Mappers;
using CES.CoreApi.Caching.Providers;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Managers;
using CES.CoreApi.Common.Providers;
using CES.CoreApi.Common.Proxies;
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
using CES.CoreApi.OrderValidation.Service.Business.Contract.Interfaces;
using CES.CoreApi.OrderValidation.Service.Business.Logic.Processors;
using CES.CoreApi.OrderValidation.Service.Business.Logic.Validators;
using CES.CoreApi.OrderValidation.Service.Contract.Models;
using CES.CoreApi.OrderValidation.Service.Data;
using CES.CoreApi.OrderValidation.Service.Interfaces;
using CES.CoreApi.OrderValidation.Service.Utilities;
using CES.CoreApi.SimpleInjectorProxy;
using FluentValidation;
using SimpleInjector;
using SimpleInjector.Extensions;
using IConfigurationProvider = CES.CoreApi.Foundation.Contract.Interfaces.IConfigurationProvider;

namespace CES.CoreApi.OrderValidation.Service.Configuration
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
            //RegisterFactories(container);
            RegisterProcessors(container);
            //RegisterUrlBuilders(container);
            //RegisterParsers(container);
            RegisterProviders(container);
            RegisterValidators(container);

            container.Verify();
        }

        private static void RegisterProviders(Container container)
        {
            container.RegisterSingle<ICurrentDateTimeProvider, CurrentDateTimeProvider>();
        }

        private static void RegisterProcessors(Container container)
        {
            container.RegisterSingle<IOrderValidateRequestProcessor, OrderValidateRequestProcessor>();
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
            container.RegisterSingle<ICacheProvider>(
                () => new AppFabricCacheProvider(container.GetInstance<ILogMonitorFactory>(),
                    container.GetInstance<IIdentityManager>(), cacheName));
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

            container.RegisterSingle<IMappingHelper, MappingHelper>();
            container.RegisterSingle<IExceptionHelper, ExceptionHelper>();
            container.RegisterSingle<IServiceHelper, ServiceHelper>();
            container.RegisterSingle<IValidationReadOnlyRepository, ValidationReadOnlyRepository>();
            container.RegisterSingle<IValidationMainRepository, ValidationMainRepository>();
        }

        private static void RegisterValidators(Container container)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            container.RegisterManyForOpenGeneric(typeof (IValidator<>), assemblies);

            container.RegisterSingle<ICountryCodeValidator, CountryCodeValidator>();
            container.RegisterSingle<IRequestValidator, RequestValidator>();
        }

        private static void RegisterInterceptions(Container container)
        {
            container.InterceptWith<PerformanceInterceptor>(type => type == typeof(IApplicationRepository));
            container.InterceptWith<PerformanceInterceptor>(type => type == typeof(IValidationReadOnlyRepository));
            container.InterceptWith<PerformanceInterceptor>(type => type == typeof(IValidationMainRepository));
            container.InterceptWith<PerformanceInterceptor>(type => type == typeof(IOrderValidateRequestProcessor));
        }

        private static void RegisterResponses(Container container)
        {
            container.Register<OrderValidateResponse, OrderValidateResponse>();
        }
    }
}
