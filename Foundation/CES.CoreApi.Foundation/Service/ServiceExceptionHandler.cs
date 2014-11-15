using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Foundation.Contract.Exceptions;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Foundation.Contract.Models;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Models;
using ServiceEndpoint = System.ServiceModel.Description.ServiceEndpoint;

namespace CES.CoreApi.Foundation.Service
{
    /// <summary>
    /// This handler processes all unhandled exceptions occurred in WCF application
    /// Two step exception logging used because OperationContext is available only in ProvideFault method.
    /// This method always called first. HandleError called second, standard .Net exception handled there and published.
    /// </summary>
    public class ServiceExceptionHandler : IErrorHandler, IServiceBehavior, IServiceExceptionHandler
    {
        #region Core

        private readonly ILogManager _logManager;
        private readonly IClientSecurityContextProvider _clientDetailsProvider;
        private readonly ICurrentDateTimeProvider _currentDateTimeProvider;
        private ExceptionLogDataContainer _dataContainer;
        private const string ErrorMessageTemplate = "Server error encountered. All details have been logged.";

        public ServiceExceptionHandler(ILogManager logManager, IClientSecurityContextProvider clientDetailsProvider, 
            ICurrentDateTimeProvider currentDateTimeProvider)
        {
            if (logManager == null) throw new ArgumentNullException("logManager");
            if (clientDetailsProvider == null) throw new ArgumentNullException("clientDetailsProvider");
            if (currentDateTimeProvider == null) throw new ArgumentNullException("currentDateTimeProvider");
            _logManager = logManager;
            _clientDetailsProvider = clientDetailsProvider;
            _currentDateTimeProvider = currentDateTimeProvider;
        }

        #endregion

        #region IErrorHandler implementation

        /// <summary>
        /// Provides a fault. The MessageFault fault parameter can be replaced or set to null to suppress reporting a fault. 
        /// </summary>
        /// <param name="error">The Exception object thrown in the course of the service operation.</param>
        /// <param name="version">The SOAP version of the message</param>
        /// <param name="fault">The System.ServiceModel.Channels.Message object that is returned to the client, or service, in the duplex case.</param>
        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            _dataContainer = _logManager.GetExceptionLogDataContainerWithCallDetails(OperationContext.Current,
                () => _clientDetailsProvider.GetDetails(OperationContext.Current));

            var faultException = BuildFaultException(error);
            var messageFault = faultException.CreateMessageFault();
            fault = Message.CreateMessage(version, messageFault, faultException.Action);
        }

        /// <summary>
        /// Logs an error, then allow the error to be handled as usual.
        /// </summary>
        /// <param name="exception">The exception thrown during processing</param>
        /// <returns></returns>
        public bool HandleError(Exception exception)
        {
            // Publish exception
            _logManager.Publish(exception, dataContainer: _dataContainer);

            // Return true to indicate the Exception has been handled
            return true;
        }

        #endregion

        #region IServiceBehavior implementation

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
            Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (var dispatcher in serviceHostBase.ChannelDispatchers.OfType<ChannelDispatcher>())
            {
                dispatcher.ErrorHandlers.Add(this);
            }
        }

        #endregion

        #region Private methods

        private FaultException<CoreApiServiceFault> BuildFaultException(Exception error)
        {
            var coreApiException = error as CoreApiException ?? new CoreApiException(error);

            var coreApiServiceFault = new CoreApiServiceFault
            {
                ErrorCode = coreApiException.ErrorCode,
                ErrorMessage = coreApiException.ClientMessage,
                ErrorTime = _currentDateTimeProvider.GetCurrentUtc(),
                ErrorIdentifier = coreApiException.ErrorId
            };

            var faultException = new FaultException<CoreApiServiceFault>(coreApiServiceFault, ErrorMessageTemplate,
                new FaultCode("Server Error"));
            return faultException;
        }

        #endregion
    }
}