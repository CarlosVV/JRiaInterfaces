using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Foundation.Providers;
using SimpleInjector;
using SimpleInjector.Integration.Wcf;

namespace CES.CoreApi.Foundation.Service
{
    public abstract class IocBasedServiceHostFactory : SimpleInjectorServiceHostFactory
    {
        protected IocBasedServiceHostFactory(Container container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            new IocContainerProvider().Initialize(container);
            Container = IocContainerProvider.Instance;
        }

        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var host = new IocBasedServiceHost(Container, serviceType, baseAddresses);

            host.Description.Behaviors.Add((IServiceBehavior) Container.GetInstance<IServiceExceptionHandler>());

            ApplyServiceBehaviors(host);
            ApplyContractBehaviors(host);

            //Configure authentication and authorization
            host.Authentication.ServiceAuthenticationManager = (ServiceAuthenticationManager)IocContainerProvider.Instance.GetInstance<IAuthenticationManager>();
            host.Authorization.ServiceAuthorizationManager = (ServiceAuthorizationManager)IocContainerProvider.Instance.GetInstance<IAuthorizationManager>();
            var serviceAuthorizationBehavior = host.Description.Behaviors.Find<ServiceAuthorizationBehavior>();
            serviceAuthorizationBehavior.PrincipalPermissionMode = PrincipalPermissionMode.Custom;

            return host;
        }

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