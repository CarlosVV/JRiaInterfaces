using System;
using System.ServiceModel;
using System.ServiceModel.Description;
//using CES.CoreApi.Foundation.Contract.Interfaces;
using SimpleInjector;
using SimpleInjector.Integration.Wcf;
//using CES.CoreApi.Foundation.Providers;
//using CES.CoreApi.Security.Wcf.Interfaces;

namespace CES.CoreApi.Foundation.Service
{
	public class IocContainerProvider
	{
		private static Container _instance;
		public void Initialize(Container container)
		{
			if (container == null)
				throw new ArgumentNullException("container");
			Instance = container;
		}

		public static Container Instance
		{
			get
			{
				if (_instance == null)
					throw new Exception(
						"Organization.Ria						TechnicalSystem.CoreApi						TechnicalSubSystem.CoreApi						SubSystemError.ServiceIntializationIoCContainerIsNotInitialized");

				return _instance;
			}
			private set { _instance = value; }
		}

	}
	public abstract class IocBasedServiceHostFactory: SimpleInjectorServiceHostFactory
    {
        protected IocBasedServiceHostFactory(Container container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            new IocContainerProvider().Initialize(container);
            Container = IocContainerProvider.Instance;
        }

        //protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        //{
        //    //var host = new IocBasedServiceHost(Container, serviceType, baseAddresses);

        //    //host.Description.Behaviors.Add((IServiceBehavior) Container.GetInstance<IServiceExceptionHandler>());

        //    ////ApplyServiceBehaviors(host);
        //    ////ApplyContractBehaviors(host);
			
        //    //host.Authentication.ServiceAuthenticationManager = (ServiceAuthenticationManager)IocContainerProvider.Instance.GetInstance<IAuthenticationManager>();
        //    //host.Authorization.ServiceAuthorizationManager = (ServiceAuthorizationManager)IocContainerProvider.Instance.GetInstance<IAuthorizationManager>();
        //    //var serviceAuthorizationBehavior = host.Description.Behaviors.Find<ServiceAuthorizationBehavior>();
        //    //serviceAuthorizationBehavior.PrincipalPermissionMode = PrincipalPermissionMode.Custom;

        //    //return host;

        //}

        private static void ApplyServiceBehaviors(ServiceHostBase host)
        {
            foreach (var behavior in Container.GetAllInstances<IServiceBehavior>())
            {
                host.Description.Behaviors.Add(behavior);
            }
        }

        private static void ApplyContractBehaviors(SimpleInjectorServiceHost host)
        {
            foreach (var behavior in Container.GetAllInstances<IContractBehavior>())
            {
                foreach (var contract in host.GetImplementedContracts())
                {
                    contract.Behaviors.Add(behavior);
                }
            }
        }
        
        protected static Container Container { get; private set; }
    }
}