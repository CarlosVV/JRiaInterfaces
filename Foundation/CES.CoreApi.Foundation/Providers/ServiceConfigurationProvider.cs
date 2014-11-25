using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Xml;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Foundation.Contract.Constants;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Foundation.Contract.Models;
using ServiceConfiguration = CES.CoreApi.Foundation.Contract.Models.ServiceConfiguration;

namespace CES.CoreApi.Foundation.Providers
{
    public class ServiceConfigurationProvider : IServiceConfigurationProvider
    {
        #region Core

        private readonly IConfigurationProvider _configurationProvider;

        public ServiceConfigurationProvider(IConfigurationProvider configurationProvider)
        {
            if (configurationProvider == null) throw new ArgumentNullException("configurationProvider");
            _configurationProvider = configurationProvider;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ServiceConfiguration GetConfiguration()
        {
            ServiceConfiguration serviceConfig;

            try
            {
                //Get configuration
                serviceConfig = _configurationProvider.ReadFromJson<ServiceConfiguration>(ServiceConfigurationItems.ServiceConfiguration);
            }
            catch (Exception ex)
            {
                throw new CoreApiException(
                    TechnicalSubSystem.CoreApi,
                    SubSystemError.ServiceIntializationConfigurationParseError,
                    ex);
            }
            
            //Validate configuration
            ValidateServiceConfiguration(serviceConfig);

            //Configure endpoints
            ConfigureEndpoints(serviceConfig);

            return serviceConfig;
        }
        
        #endregion

        #region private methods

        /// <summary>
        /// Validates that major service configuration elements populated
        /// </summary>
        /// <param name="serviceConfig">Service configuration</param>
        private static void ValidateServiceConfiguration(ServiceConfiguration serviceConfig)
        {
            if (serviceConfig == null)
                throw new CoreApiException(TechnicalSubSystem.CoreApi,
                    SubSystemError.ServiceIntializationConfigurationNotFound);

            if (!serviceConfig.Endpoints.Any())
                throw new CoreApiException(TechnicalSubSystem.CoreApi,
                    SubSystemError.ServiceIntializationNoEndpointsFound);
        }

        /// <summary>
        /// Configures service endpoints
        /// </summary>
        /// <param name="configuration">Endpoint configuration</param>
        private static void ConfigureEndpoints(ServiceConfiguration configuration)
        {
            foreach (var endpoint in configuration.Endpoints)
            {
                //Configure binding
                ConfigureBinding(configuration, endpoint);

                //Configure endpoint security
                ConfigureEndpointSecurity(endpoint, configuration.Behavior.IsHttpsEnabled);
            }
        }

        /// <summary>
        /// Configures endpoint binding
        /// </summary>
        /// <param name="configuration">Service configuration</param>
        /// <param name="endpoint">Endpoint instance to configure</param>
        private static void ConfigureBinding(ServiceConfiguration configuration, EndpointConfiguration endpoint)
        {
            //Check if same binding already configured for another endpoint
            var configuredBinding = GetBindingAlreadyConfigured(
                configuration.Endpoints,
                endpoint.Binding,
                endpoint.BindingConfigurationName);

            if (configuredBinding == null)
            {
                //Get binding config
                var bindingConfiguration = GetBindingConfiguration(
                    configuration,
                    endpoint.Binding,
                    endpoint.BindingConfigurationName);

                //Configure binding
                endpoint.ConfiguredBinding = ConfigureBinding(endpoint.Binding, bindingConfiguration);
            }
            else
            {
                //Set earlier configured binding
                endpoint.ConfiguredBinding = configuredBinding;
            }
        }

        /// <summary>
        /// Checks if binding with same type and name already configured for another endpoint and gets it
        /// </summary>
        /// <param name="configuration">Service endpoint configuration list</param>
        /// <param name="binding">Binding type</param>
        /// <param name="bindingConfigurationName">Binding configuration name</param>
        /// <returns></returns>
        private static Binding GetBindingAlreadyConfigured(IEnumerable<EndpointConfiguration> configuration, string binding, string bindingConfigurationName)
        {
            var configuredBinding = configuration
                .Where(endpoint => endpoint.ConfiguredBinding != null &&
                                   endpoint.Binding.Equals(binding, StringComparison.OrdinalIgnoreCase) &&
                                   (string.IsNullOrEmpty(bindingConfigurationName) ||
                                   endpoint.ConfiguredBinding.Name.Equals(bindingConfigurationName, StringComparison.OrdinalIgnoreCase)))
                .Select(endpoint => endpoint.ConfiguredBinding)
                .FirstOrDefault();

            return configuredBinding;
        }

        /// <summary>
        /// Gets binding configuration from service configuration according name and type
        /// </summary>
        /// <param name="configuration">Service configuration</param>
        /// <param name="binding">Binding type name</param>
        /// <param name="bindingConfigurationName">Binding configuration name</param>
        /// <returns></returns>
        private static BindingConfiguration GetBindingConfiguration(ServiceConfiguration configuration, string binding, string bindingConfigurationName)
        {
            var bindingConfiguration = configuration.Bindings.FirstOrDefault(
                p =>
                    p.Binding.Equals(binding, StringComparison.OrdinalIgnoreCase) &&
                    p.Name.Equals(bindingConfigurationName, StringComparison.OrdinalIgnoreCase));

            return bindingConfiguration;
        }

        /// <summary>
        /// Configures endpoint security
        /// </summary>
        /// <param name="endpoint">Endpoint instance</param>
        /// <param name="isHttps">Defines whether HTTPS enabled</param>
        private static void ConfigureEndpointSecurity(EndpointConfiguration endpoint, bool isHttps)
        {
            switch (endpoint.Binding.ToUpperInvariant())
            {
                case "WSHTTPBINDING":
                    ConfigureWsHttpEndpointSecurity(endpoint, isHttps);
                    break;

                case "BASICHTTPBINDING":
                    ConfigureBasicHttpEndpointSecurity(endpoint, isHttps);
                    break;

                case "WEBHTTPBINDING":
                    ConfigureWebHttpEndpointSecurity(endpoint, isHttps);
                    break;

                case "NETTCPBINDING":
                    ConfigureNetTcpEndpointSecurity(endpoint, isHttps);
                    break;
            }
        }

        /// <summary>
        /// Configures NetTCP endpoint security
        /// </summary>
        /// <param name="endpoint">Endpoint instance</param>
        /// <param name="isHttps">Defines whether HTTPS enabled</param>
        private static void ConfigureNetTcpEndpointSecurity(EndpointConfiguration endpoint, bool isHttps)
        {
            var netTcpBinding = endpoint.ConfiguredBinding as NetTcpBinding;

            if (netTcpBinding == null)
                throw new CoreApiException(TechnicalSubSystem.CoreApi,
                    SubSystemError.ServiceIntializationBindingNameDoesNotMatch,
                    endpoint.ConfiguredBinding.Name, endpoint.ConfiguredBinding.GetType().Name);

            if (endpoint.SecurityMode == ServiceSecurityMode.None)
            {
                netTcpBinding.Security.Mode = SecurityMode.None;
                return;
            }

            if (isHttps)
                netTcpBinding.Security.Mode = SecurityMode.Transport;

            if (endpoint.SecurityMode != ServiceSecurityMode.Certificate) 
                return;

            netTcpBinding.Security.Mode = isHttps
                ? SecurityMode.TransportWithMessageCredential
                : SecurityMode.Message;
            netTcpBinding.Security.Message.ClientCredentialType = MessageCredentialType.Certificate;
        }

        /// <summary>
        /// Configures WebHttp endpoint security
        /// </summary>
        /// <param name="endpoint">Endpoint instance</param>
        /// <param name="isHttps">Defines whether HTTPS enabled</param>
        private static void ConfigureWebHttpEndpointSecurity(EndpointConfiguration endpoint, bool isHttps)
        {
            var webHttpBinding = endpoint.ConfiguredBinding as WebHttpBinding;

            if (webHttpBinding == null)
                throw new CoreApiException(TechnicalSubSystem.CoreApi,
                    SubSystemError.ServiceIntializationBindingNameDoesNotMatch,
                    endpoint.ConfiguredBinding.Name, endpoint.ConfiguredBinding.GetType().Name);

            if (endpoint.SecurityMode == ServiceSecurityMode.None)
            {
                webHttpBinding.Security.Mode = WebHttpSecurityMode.None;
                return;
            }

            if (isHttps)
                webHttpBinding.Security.Mode = WebHttpSecurityMode.Transport;

            if (endpoint.SecurityMode == ServiceSecurityMode.Certificate)
                webHttpBinding.Security.Transport.ClientCredentialType =
                    HttpClientCredentialType.Certificate;
        }

        /// <summary>
        /// Configures BasicHttp endpoint security
        /// </summary>
        /// <param name="endpoint">Endpoint instance</param>
        /// <param name="isHttps">Defines whether HTTPS enabled</param>
        private static void ConfigureBasicHttpEndpointSecurity(EndpointConfiguration endpoint, bool isHttps)
        {
            var basicBinding = endpoint.ConfiguredBinding as BasicHttpBinding;

            if (basicBinding == null)
                throw new CoreApiException(TechnicalSubSystem.CoreApi,
                    SubSystemError.ServiceIntializationBindingNameDoesNotMatch,
                    endpoint.ConfiguredBinding.Name, endpoint.ConfiguredBinding.GetType().Name);

            if (endpoint.SecurityMode == ServiceSecurityMode.None)
            {
                basicBinding.Security.Mode = BasicHttpSecurityMode.None;
                return;
            }

            if (isHttps)
                basicBinding.Security.Mode = BasicHttpSecurityMode.Transport;

            if (endpoint.SecurityMode != ServiceSecurityMode.Certificate) 
                return;

            basicBinding.Security.Mode = isHttps
                ? BasicHttpSecurityMode.TransportWithMessageCredential
                : BasicHttpSecurityMode.Message;
            basicBinding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.Certificate;
        }

        /// <summary>
        /// Configures WsHttp endpoint security
        /// </summary>
        /// <param name="endpoint">Endpoint instance</param>
        /// <param name="isHttps">Defines whether HTTPS enabled</param>
        private static void ConfigureWsHttpEndpointSecurity(EndpointConfiguration endpoint, bool isHttps)
        {
            var wsHttpBinding = endpoint.ConfiguredBinding as WSHttpBinding;

            if (wsHttpBinding == null)
                throw new CoreApiException(TechnicalSubSystem.CoreApi,
                    SubSystemError.ServiceIntializationBindingNameDoesNotMatch,
                    endpoint.ConfiguredBinding.Name, endpoint.ConfiguredBinding.GetType().Name);

            if (endpoint.SecurityMode == ServiceSecurityMode.None)
            {
                wsHttpBinding.Security.Mode = SecurityMode.None;
                return;
            }

            if (endpoint.SecurityMode != ServiceSecurityMode.Certificate) 
                return;

            if (isHttps)
                wsHttpBinding.Security.Mode = SecurityMode.Transport;

            wsHttpBinding.Security.Mode = isHttps
                ? SecurityMode.TransportWithMessageCredential
                : SecurityMode.Message;
            wsHttpBinding.Security.Message.ClientCredentialType = MessageCredentialType.Certificate;
        }

        /// <summary>
        /// Configures binding according configuration
        /// </summary>
        /// <param name="bindingType">Binding type</param>
        /// <param name="bindingConfiguration">Binding configuration</param>
        /// <returns></returns>
        private static Binding ConfigureBinding(string bindingType, BindingConfiguration bindingConfiguration)
        {
            Binding binding;

            switch (bindingType.ToUpperInvariant())
            {
                case "NETNAMEDPIPEBINDING":
                    binding = ConfigureNetNamedPipeBinding(bindingConfiguration);
                    break;

                case "NETTCPBINDING":
                    binding = ConfigureNetTcpBinding(bindingConfiguration);
                    break;

                case "WSHTTPBINDING":
                    binding = ConfigureWsHttpBinding(bindingConfiguration);
                    break;

                case "BASICHTTPBINDING":
                    binding = ConfigureBasicHttpBinding(bindingConfiguration);
                    break;

                case "WSFEDERATIONHTTPBINDING":
                    binding = ConfigureWsFederationHttpBinding(bindingConfiguration);
                    break;

                case "WS2007FEDERATIONHTTPBINDING":
                    binding = ConfigureWs2007FederationHttpBinding(bindingConfiguration);
                    break;

                case "WSDUALHTTPBINDING":
                    binding = ConfigureWsDualHttpBinding(bindingConfiguration);
                    break;

                case "WEBHTTPBINDING":
                    binding = ConfigureWebHttpBinding(bindingConfiguration);
                    break;

                default:
                    throw new CoreApiException(TechnicalSubSystem.CoreApi,
                        SubSystemError.ServiceIntializationInvalidBinding, 
                        bindingConfiguration.Name);
            }

            ConfigureBindingCommonProperties(bindingConfiguration, binding);

            return binding;
        }

        /// <summary>
        /// Configures common properties for all bindings
        /// </summary>
        /// <param name="bindingConfiguration">Binding configuration</param>
        /// <param name="binding">Binding instance to configure</param>
        private static void ConfigureBindingCommonProperties(BindingConfiguration bindingConfiguration, Binding binding)
        {
            if (bindingConfiguration == null)
                return;

            binding.Name = bindingConfiguration.Name;

            if (!string.IsNullOrEmpty(bindingConfiguration.Namespace))
                binding.Namespace = bindingConfiguration.Namespace;

            if (bindingConfiguration.OpenTimeout != default(TimeSpan))
                binding.OpenTimeout = bindingConfiguration.OpenTimeout;

            if (bindingConfiguration.CloseTimeout != default(TimeSpan))
                binding.CloseTimeout = bindingConfiguration.CloseTimeout;

            if (bindingConfiguration.ReceiveTimeout != default(TimeSpan))
                binding.ReceiveTimeout = bindingConfiguration.ReceiveTimeout;

            if (bindingConfiguration.SendTimeout != default(TimeSpan))
                binding.SendTimeout = bindingConfiguration.SendTimeout;
        }

        /// <summary>
        /// Gets WebHttp Binding configured
        /// </summary>
        /// <param name="bindingConfiguration">Binding configuration</param>
        /// <returns></returns>
        private static Binding ConfigureWebHttpBinding(BindingConfiguration bindingConfiguration)
        {
            var binding = new WebHttpBinding(WebHttpSecurityMode.None);

            if (bindingConfiguration == null)
                return binding;

            binding.TransferMode = bindingConfiguration.TransferMode;

            if (bindingConfiguration.MaxBufferPoolSize != 0)
                binding.MaxBufferPoolSize = bindingConfiguration.MaxBufferPoolSize;

            if (bindingConfiguration.MaxBufferSize != 0)
                binding.MaxBufferSize = bindingConfiguration.MaxBufferSize;

            if (bindingConfiguration.MaxReceivedMessageSize != 0)
                binding.MaxReceivedMessageSize = bindingConfiguration.MaxReceivedMessageSize;

            var readerQuotas = GetReaderQuotasConfigured(bindingConfiguration.ReaderQuotas);
            if (readerQuotas != null)
                binding.ReaderQuotas = readerQuotas;

            return binding;
        }

        /// <summary>
        /// Gets WSDualHttp Binding configured
        /// </summary>
        /// <param name="bindingConfiguration">Binding configuration</param>
        /// <returns></returns>
        private static Binding ConfigureWsDualHttpBinding(BindingConfiguration bindingConfiguration)
        {
            var binding = new WSDualHttpBinding();

            if (bindingConfiguration == null)
                return binding;

            binding.TransactionFlow = bindingConfiguration.TransactionFlow;

            if (bindingConfiguration.MaxBufferPoolSize != 0)
                binding.MaxBufferPoolSize = bindingConfiguration.MaxBufferPoolSize;

            if (bindingConfiguration.MaxReceivedMessageSize != 0)
                binding.MaxReceivedMessageSize = bindingConfiguration.MaxReceivedMessageSize;

            var readerQuotas = GetReaderQuotasConfigured(bindingConfiguration.ReaderQuotas);
            if (readerQuotas != null)
                binding.ReaderQuotas = readerQuotas;

            return binding;
        }

        /// <summary>
        /// Gets WS2007FederationHttp Binding configured
        /// </summary>
        /// <param name="bindingConfiguration">Binding configuration</param>
        /// <returns></returns>
        private static Binding ConfigureWs2007FederationHttpBinding(BindingConfiguration bindingConfiguration)
        {
            var binding = new WS2007FederationHttpBinding();

            if (bindingConfiguration == null)
                return binding;

            binding.TransactionFlow = bindingConfiguration.TransactionFlow;

            if (bindingConfiguration.MaxBufferPoolSize != 0)
                binding.MaxBufferPoolSize = bindingConfiguration.MaxBufferPoolSize;

            if (bindingConfiguration.MaxReceivedMessageSize != 0)
                binding.MaxReceivedMessageSize = bindingConfiguration.MaxReceivedMessageSize;

            var readerQuotas = GetReaderQuotasConfigured(bindingConfiguration.ReaderQuotas);
            if (readerQuotas != null)
                binding.ReaderQuotas = readerQuotas;

            return binding;
        }

        /// <summary>
        /// Gets WSFederationHttp Binding configured
        /// </summary>
        /// <param name="bindingConfiguration">Binding configuration</param>
        /// <returns></returns>
        private static Binding ConfigureWsFederationHttpBinding(BindingConfiguration bindingConfiguration)
        {
            var binding = new WSFederationHttpBinding();

            if (bindingConfiguration == null)
                return binding;

            binding.TransactionFlow = bindingConfiguration.TransactionFlow;

            if (bindingConfiguration.MaxBufferPoolSize != 0)
                binding.MaxBufferPoolSize = bindingConfiguration.MaxBufferPoolSize;

            if (bindingConfiguration.MaxReceivedMessageSize != 0)
                binding.MaxReceivedMessageSize = bindingConfiguration.MaxReceivedMessageSize;

            var readerQuotas = GetReaderQuotasConfigured(bindingConfiguration.ReaderQuotas);
            if (readerQuotas != null)
                binding.ReaderQuotas = readerQuotas;

            return binding;
        }

        /// <summary>
        /// Gets BasicHttp Binding configured
        /// </summary>
        /// <param name="bindingConfiguration">Binding configuration</param>
        /// <returns></returns>
        private static Binding ConfigureBasicHttpBinding(BindingConfiguration bindingConfiguration)
        {
            var binding = new BasicHttpBinding();

            if (bindingConfiguration == null)
                return binding;

            binding.UseDefaultWebProxy = bindingConfiguration.UseDefaultWebProxy;
            binding.MessageEncoding = bindingConfiguration.MessageEncoding;
            binding.TransferMode = bindingConfiguration.TransferMode;

            if (bindingConfiguration.MaxBufferPoolSize != 0)
                binding.MaxBufferPoolSize = bindingConfiguration.MaxBufferPoolSize;

            if (bindingConfiguration.MaxBufferSize != 0)
                binding.MaxBufferSize = bindingConfiguration.MaxBufferSize;

            if (bindingConfiguration.MaxReceivedMessageSize != 0)
                binding.MaxReceivedMessageSize = bindingConfiguration.MaxReceivedMessageSize;

            var readerQuotas = GetReaderQuotasConfigured(bindingConfiguration.ReaderQuotas);
            if (readerQuotas != null)
                binding.ReaderQuotas = readerQuotas;
            
            return binding;
        }

        /// <summary>
        /// Gets WsHttp Binding configured
        /// </summary>
        /// <param name="bindingConfiguration">Binding configuration</param>
        /// <returns></returns>
        private static Binding ConfigureWsHttpBinding(BindingConfiguration bindingConfiguration)
        {
            var binding = new WSHttpBinding
            {
                ReliableSession = {Enabled = false},
                Security =
                {
                    Mode = SecurityMode.None,
                    Transport =
                    {
                        ClientCredentialType = HttpClientCredentialType.None
                    }
                }
            };
            
            if (bindingConfiguration == null)
                return binding;

            binding.MessageEncoding = bindingConfiguration.MessageEncoding;
            binding.TransactionFlow = bindingConfiguration.TransactionFlow;

            if (bindingConfiguration.MaxBufferPoolSize != 0)
                binding.MaxBufferPoolSize = bindingConfiguration.MaxBufferPoolSize;

            if (bindingConfiguration.MaxReceivedMessageSize != 0)
                binding.MaxReceivedMessageSize = bindingConfiguration.MaxReceivedMessageSize;
            
            var readerQuotas = GetReaderQuotasConfigured(bindingConfiguration.ReaderQuotas);
            if (readerQuotas != null)
                binding.ReaderQuotas = readerQuotas;

            var reliableSession = GetReliableSessionConfigured(bindingConfiguration.ReliableSession);
            if (reliableSession != null)
                binding.ReliableSession = reliableSession;
           
            return binding;
        }

        /// <summary>
        /// Gets NetTcp Binding configured
        /// </summary>
        /// <param name="bindingConfiguration">Binding configuration</param>
        /// <returns></returns>
        private static Binding ConfigureNetTcpBinding(BindingConfiguration bindingConfiguration)
        {
            var binding = new NetTcpBinding(SecurityMode.None) {ReliableSession = {Enabled = false}};

            if (bindingConfiguration == null)
                return binding;

            binding.PortSharingEnabled = bindingConfiguration.PortSharingEnabled;
            binding.TransactionFlow = bindingConfiguration.TransactionFlow;
            binding.TransferMode = bindingConfiguration.TransferMode;

            if (bindingConfiguration.ListenBacklog != 0)
                binding.ListenBacklog = bindingConfiguration.ListenBacklog;

            if (bindingConfiguration.MaxBufferPoolSize != 0)
                binding.MaxBufferPoolSize = bindingConfiguration.MaxBufferPoolSize;

            if (bindingConfiguration.MaxBufferSize != 0)
                binding.MaxBufferSize = bindingConfiguration.MaxBufferSize;

            if (bindingConfiguration.MaxReceivedMessageSize != 0)
                binding.MaxReceivedMessageSize = bindingConfiguration.MaxReceivedMessageSize;
            
            if (bindingConfiguration.MaxConnections != 0)
                binding.MaxConnections = bindingConfiguration.MaxConnections;

            var readerQuotas = GetReaderQuotasConfigured(bindingConfiguration.ReaderQuotas);
            if (readerQuotas != null)
                binding.ReaderQuotas = readerQuotas;

            var reliableSession = GetReliableSessionConfigured(bindingConfiguration.ReliableSession);
            if (reliableSession != null)
                binding.ReliableSession = reliableSession;

            return binding;
        }

        /// <summary>
        /// Gets NetNamedPipe Binding configured
        /// </summary>
        /// <param name="bindingConfiguration">Binding configuration</param>
        /// <returns></returns>
        private static Binding ConfigureNetNamedPipeBinding(BindingConfiguration bindingConfiguration)
        {
            var binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);

            if (bindingConfiguration == null) 
                return binding;

            if (bindingConfiguration.MaxBufferPoolSize != 0)
                binding.MaxBufferPoolSize = bindingConfiguration.MaxBufferPoolSize;

            if (bindingConfiguration.MaxBufferSize != 0)
                binding.MaxBufferSize = bindingConfiguration.MaxBufferSize;

            if (bindingConfiguration.MaxReceivedMessageSize != 0)
                binding.MaxReceivedMessageSize = bindingConfiguration.MaxReceivedMessageSize;

            if (bindingConfiguration.TransactionFlow)
                binding.TransactionFlow = bindingConfiguration.TransactionFlow;

            if (bindingConfiguration.MaxConnections != 0)
                binding.MaxConnections = bindingConfiguration.MaxConnections;

            binding.TransferMode = bindingConfiguration.TransferMode;
            
            var readerQuotas = GetReaderQuotasConfigured(bindingConfiguration.ReaderQuotas);
            if (readerQuotas != null)
                binding.ReaderQuotas = readerQuotas;

            return binding;
        }

        /// <summary>
        /// Gets reader quotas configured
        /// </summary>
        /// <param name="configuration">Reader quotas configuration</param>
        /// <returns></returns>
        private static XmlDictionaryReaderQuotas GetReaderQuotasConfigured(ReaderQuotasConfiguration configuration)
        {
            if (configuration == null)
                return null;

            var readerQuotas =   new XmlDictionaryReaderQuotas();

            if (configuration.MaxArrayLength != 0)
                readerQuotas.MaxArrayLength = configuration.MaxArrayLength;

            if (configuration.MaxBytesPerRead != 0)
                readerQuotas.MaxBytesPerRead = configuration.MaxBytesPerRead;

            if (configuration.MaxDepth != 0)
                readerQuotas.MaxDepth = configuration.MaxDepth;

            if (configuration.MaxNameTableCharCount != 0)
                readerQuotas.MaxNameTableCharCount = configuration.MaxNameTableCharCount;

            if (configuration.MaxStringContentLength != 0)
                readerQuotas.MaxStringContentLength = configuration.MaxStringContentLength;

            return readerQuotas;
        }

        /// <summary>
        /// Gets reliable session configured
        /// </summary>
        /// <param name="configuration">Reliable session configuration</param>
        /// <returns></returns>
        private static OptionalReliableSession GetReliableSessionConfigured(ReliableSessionConfiguration configuration)
        {
            if (configuration == null)
                return null;
            
            var reliableSession = new OptionalReliableSession
            {
                Enabled = configuration.Enabled,
                Ordered = configuration.Ordered
            };

            if (configuration.InactivityTimeout != default(TimeSpan))
                reliableSession.InactivityTimeout = configuration.InactivityTimeout;

            return reliableSession;
        }
        
        #endregion
    }
}
