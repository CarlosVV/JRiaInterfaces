using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Description;
using CES.CoreApi.Caching.Providers;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Proxies;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Foundation.Data;
using CES.CoreApi.Foundation.Providers;
using CES.CoreApi.Foundation.Security;
using CES.CoreApi.Foundation.Validation;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Foundation.Service
{
    public abstract class IocBasedServiceHostFactory : ServiceHostFactory
    {
        protected IocBasedServiceHostFactory(IIocContainer container)
        {
            if (container == null) 
                throw new ArgumentNullException("container");

            new IocContainerProvider().Initialize(container);
            Container = IocContainerProvider.Instance;
        }

        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var host = new IocBasedServiceHost(Container, serviceType, baseAddresses);
            host.Description.Behaviors.Add((IServiceBehavior) Container.Resolve<IServiceExceptionHandler>());
            return host;
        }

        protected static IIocContainer Container { get; private set; }

        protected virtual void RegisterTypes()
        {
            //Register common foundation classes
            Container
                .RegisterType<IAuthenticationManager, AuthenticationManager>()
                .RegisterType<IApplicationAuthenticator, ApplicationAuthenticator>()
                .RegisterTypeWithInterfaceInterceptor<IApplicationRepository, ApplicationRepository>(LifetimeManagerType.AlwaysNew, InterceptionBehaviorType.Performance)
                .RegisterType<IApplicationValidator, ApplicationValidator>()
                .RegisterType<IRequestHeadersProvider, RequestHeadersProvider>()
                .RegisterType<IServiceCallHeaderParametersProvider, ServiceCallHeaderParametersProvider>()
                .RegisterType<IAuthorizationManager, AuthorizationManager>(LifetimeManagerType.AlwaysNew)
                .RegisterType<IAuthorizationAdministrator, AuthorizationAdministrator>()
                .RegisterType<ICacheProvider, AppFabricCacheProvider>()
                .RegisterType<IHostApplicationProvider, HostApplicationProvider>()
                .RegisterType<IClientSecurityContextProvider, ClientDetailsProvider>()
                .RegisterType<IServiceExceptionHandler, ServiceExceptionHandler>(LifetimeManagerType.AlwaysNew)
                .RegisterType<IAutoMapperProxy, AutoMapperProxy>()
                .RegisterType<IHttpClientProxy, HttpClientProxy>()
                .RegisterType<IConfigurationProvider, ConfigurationProvider>()
                .RegisterType<IServiceConfigurationProvider, ServiceConfigurationProvider>()
                .RegisterType<ISecurityAuditLogger, SecurityAuditLogger>(LifetimeManagerType.AlwaysNew);

        }
    }
}