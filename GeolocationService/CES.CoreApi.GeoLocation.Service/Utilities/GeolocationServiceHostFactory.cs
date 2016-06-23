using CES.CoreApi.Caching.Providers;

using CES.CoreApi.GeoLocation.Service.Configuration;
using CES.CoreApi.Security.Interfaces;
using CES.CoreApi.Security.Managers;
using CES.CoreApi.Security.Providers;
using CES.CoreApi.Security.Wcf.Interfaces;
using CES.CoreApi.Security.Wcf.Managers;
using SimpleInjector;
using SimpleInjector.Integration.Wcf;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace CES.CoreApi.GeoLocation.Service.Factory
{
	public static class Bootstrapper
	{
		public static readonly Container Container;

		public  static Container GetBootstrapper()
		{
			var container = new Container();
			container.Options.DefaultScopedLifestyle = new WcfOperationLifestyle();
			CompositionRoot.RegisterDependencies(Container);
			//// register all your components with the container here:
			//// container.Register<IService1, Service1>()
			//// container.Register<IDataContext, DataContext>(Lifestyle.Scoped);
			////CompositionRoot.RegisterDependencies(container);

			//container.Register<IAuthenticationManager, AuthenticationManager>();
			//container.Register<IApplicationAuthenticator, ApplicationAuthenticator>();
			//container.Register<Foundation.Contract.Interfaces.IApplicationRepository, Foundation.Repositories.ApplicationRepository>();
			//container.Register<IAuthorizationManager, AuthorizationManager>();
			//container.Register<IApplicationAuthorizator, ApplicationAuthorizator>();
			//container.Register<Caching.Interfaces.ICacheProvider>(() => new RedisCacheProvider());
			////container.Register<Foundation.Contract.Interfaces.IServiceExceptionHandler, Foundation.Service.ServiceExceptionHandler>();        
			////container.Register<IConfigurationProvider, ConfigurationProvider>();
			//container.Register<IIdentityProvider, IdentityProvider>();
			//container.Verify();

			//Container = container;
			return container;
		}
	}
	public class GeolocationServiceHostFactory : SimpleInjectorServiceHostFactory
	{
		//protected  Container container { get; private set; }
		Container x = Bootstrapper.GetBootstrapper();
		protected override ServiceHost CreateServiceHost(Type serviceType,
			Uri[] baseAddresses)
		{
			var host = new SimpleInjectorServiceHost(
				x,
				serviceType,
				baseAddresses);

			//// This is all optional
			//this.ApplyServiceBehaviors(host);
			//this.ApplyContractBehaviors(host);

			return host;
		}

		private void ApplyServiceBehaviors(ServiceHost host)
		{

			foreach (var behavior in Bootstrapper.Container.GetAllInstances<IServiceBehavior>())
			{
				host.Description.Behaviors.Add(behavior);
			}
		}

		private void ApplyContractBehaviors(SimpleInjectorServiceHost host)
		{
			foreach (var behavior in Bootstrapper.Container.GetAllInstances<IContractBehavior>())
			{
				foreach (var contract in host.GetImplementedContracts())
				{
					contract.Behaviors.Add(behavior);
				}
			}
		}
	}
	//public sealed class GeolocationServiceHostFactory : IocBasedServiceHostFactory
 //   {
 //       public GeolocationServiceHostFactory()
 //           : base(new Container())
 //       {
 //           CompositionRoot.RegisterDependencies(Container);
	//		//MapperConfiguratorX.Configure(Container);
 //       }
 //   }
}