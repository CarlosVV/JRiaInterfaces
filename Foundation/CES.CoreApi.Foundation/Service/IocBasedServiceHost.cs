using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Foundation.Contract.Models;
using CES.CoreApi.Foundation.Providers;
using ServiceConfiguration = CES.CoreApi.Foundation.Contract.Models.ServiceConfiguration;
using ServiceEndpoint = System.ServiceModel.Description.ServiceEndpoint;

namespace CES.CoreApi.Foundation.Service
{
    public class IocBasedServiceHost : ServiceHost
    {
        #region Core

        private const string MexAddress = "mex";

        public IocBasedServiceHost(IIocContainer container, Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            foreach (var description in ImplementedContracts.Values)
            {
                description.Behaviors.Add(new IocBasedInstanceProvider(container, serviceType));
            }
        }

        #endregion

        #region Overriding

        protected override void ApplyConfiguration()
        {
            base.ApplyConfiguration();
        
            var interfaces = Description.ServiceType.GetInterfaces();
        
            //Get service configuration
            var serviceConfigurationProvider = IocContainerProvider.Instance.Resolve<IServiceConfigurationProvider>();
            var serviceConfiguration = serviceConfigurationProvider.GetConfiguration();

            //Add endpoints
            foreach (var serviceEndpoint in from serviceInterface in interfaces 
                                            let interfaceName = serviceInterface.ToString() 
                                            let endpoints = GetEndpointsForInterface(serviceConfiguration, interfaceName) 
                                            from endpoint in endpoints 
                                            select AddServiceEndpoint(serviceInterface, endpoint.ConfiguredBinding, ""))
            {
                //Uncomment next line to add message inspector to intercept calls
                //serviceEndpoint.Behaviors.Add(new ServiceEndpointBehavior());

                ConfigureWebHttpBindingBehavior(serviceEndpoint, serviceConfiguration.Behavior);
            }

            //Configure authentication and authorization
            Authentication.ServiceAuthenticationManager = (ServiceAuthenticationManager)IocContainerProvider.Instance.Resolve<IAuthenticationManager>();
            Authorization.ServiceAuthorizationManager = (ServiceAuthorizationManager)IocContainerProvider.Instance.Resolve<IAuthorizationManager>();

            ConfigureServiceBehavior(serviceConfiguration.Behavior);
        }
        
        #endregion

        #region private methods

        /// <summary>
        /// Configures service behavior
        /// </summary>
        /// <param name="behavior">Behavior configuration</param>
        private void ConfigureServiceBehavior(BehaviorConfiguration behavior)
        {
            if (behavior.IsDebugEnabled)
            {
                var debug = (ServiceDebugBehavior)Description.Behaviors[typeof(ServiceDebugBehavior)];
                debug.IncludeExceptionDetailInFaults = true;
                debug.HttpHelpPageEnabled = true;
                debug.HttpsHelpPageEnabled = true;
            }

            if (behavior.IsWsdlEnabled)
            {
                AddMetadataEndpoint();
            }
        }

        /// <summary>
        /// Configures behavior for WebHttp binding [REST]
        /// </summary>
        /// <param name="endpoint">Endpoint instance</param>
        /// <param name="behavior">Behavior configuration</param>
        private static void ConfigureWebHttpBindingBehavior(ServiceEndpoint endpoint, BehaviorConfiguration behavior)
        {
            if (endpoint.Binding.GetType() != typeof(WebHttpBinding))
                return;

            var webHttpBehavior = new WebHttpBehavior
            {
                HelpEnabled = behavior.IsHelpEnabled,
                AutomaticFormatSelectionEnabled = behavior.IsAutomaticFormatSelectionEnabled,
                DefaultOutgoingResponseFormat =
                    behavior.IsJsonResponseEnabled
                        ? System.ServiceModel.Web.WebMessageFormat.Json
                        : System.ServiceModel.Web.WebMessageFormat.Xml,
                DefaultOutgoingRequestFormat =
                    behavior.IsJsonRequestEnabled
                        ? System.ServiceModel.Web.WebMessageFormat.Json
                        : System.ServiceModel.Web.WebMessageFormat.Xml
            };

            endpoint.Behaviors.Add(webHttpBehavior);
        }

        /// <summary>
        /// Gets configured endpoint list for interface
        /// </summary>
        /// <param name="serviceConfiguration">Service configuration</param>
        /// <param name="interfaceName">Interface name</param>
        /// <returns></returns>
        private static IEnumerable<EndpointConfiguration> GetEndpointsForInterface(ServiceConfiguration serviceConfiguration, string interfaceName)
        {
            var endpoints = serviceConfiguration.Endpoints.Where(
                p =>
                    p.Contract.Equals(interfaceName, StringComparison.OrdinalIgnoreCase) &&
                    p.ConfiguredBinding != null)
                .ToList();

            if (!endpoints.Any())
                throw new CoreApiException(TechnicalSubSystem.CoreApi,
                    SubSystemError.ServiceIntializationNoEndpointsFoundForInterface,
                    interfaceName);

            return endpoints;
        }
        
        private void AddMetadataEndpoint()
        {
            var mexBehavior = Description.Behaviors.Find<ServiceMetadataBehavior>();
            if (mexBehavior == null)
            {
                mexBehavior = new ServiceMetadataBehavior();
                Description.Behaviors.Add(mexBehavior);
            }
            else
            {
                //Metadata behavior has already been configured, 
                //so we do not have any work to do.
                return;
            }

            //Add a metadata endpoint at each base address
            //using the "/mex" addressing convention
            foreach (var baseAddress in BaseAddresses)
            {
                if (baseAddress.Scheme == Uri.UriSchemeHttp)
                {
                    mexBehavior.HttpGetEnabled = true;
                    AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexHttpBinding(), MexAddress);
                }
                else if (baseAddress.Scheme == Uri.UriSchemeHttps)
                {
                    mexBehavior.HttpsGetEnabled = true;
                    AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexHttpsBinding(), MexAddress);
                }
                //By some reason multiple MEX endpoints not working, so metadata exposed over http only

                //else if (baseAddress.Scheme == Uri.UriSchemeNetPipe)
                //{
                //    AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexNamedPipeBinding(), MexAddress);
                //}
                //else if (baseAddress.Scheme == Uri.UriSchemeNetTcp)
                //{
                //    AddServiceEndpoint(ServiceMetadataBehavior.MexContractName, MetadataExchangeBindings.CreateMexTcpBinding(), MexAddress);
                //}
            }
        }

        #endregion
    }
}
