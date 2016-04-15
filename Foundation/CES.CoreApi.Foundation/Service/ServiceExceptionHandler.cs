using System;
using System.Net;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Models;
using ServiceEndpoint = System.ServiceModel.Description.ServiceEndpoint;
using CES.CoreApi.Security.Interfaces;

namespace CES.CoreApi.Foundation.Service
{
	/// <summary>
	/// This handler processes all unhandled exceptions occurred in WCF application
	/// Two step exception logging used because OperationContext is available only in ProvideFault method.
	/// This method always called first. HandleError called second, standard .Net exception handled there and published.
	/// </summary>
	public class ServiceExceptionHandler : IErrorHandler, IServiceBehavior, IServiceExceptionHandler
	{
		
		private readonly IExceptionLogMonitor _exceptionLogMonitor;
		private readonly IClientSecurityContextProvider _clientDetailsProvider;
		private readonly IIdentityManager _identityManager;
		private readonly ICurrentDateTimeProvider _currentDateTimeProvider;
		private CoreApiException _coreApiException;
		private const string ErrorMessage = "Server error encountered. All details have been logged.";
		private const string WcfSecurityErrorMessage = "The caller was not authenticated by the service.";
		internal const string HttpRequest = "httpRequest";

		public ServiceExceptionHandler(ILogMonitorFactory logMonitorFactory, IClientSecurityContextProvider clientDetailsProvider,
			IIdentityManager identityManager, ICurrentDateTimeProvider currentDateTimeProvider)
		{
			if (logMonitorFactory == null)
				throw new ArgumentNullException("logMonitorFactory");
			if (clientDetailsProvider == null)
				throw new ArgumentNullException("clientDetailsProvider");
			if (identityManager == null)
				throw new ArgumentNullException("identityManager");
			if (currentDateTimeProvider == null)
				throw new ArgumentNullException("currentDateTimeProvider");

			_clientDetailsProvider = clientDetailsProvider;
			_identityManager = identityManager;
			_currentDateTimeProvider = currentDateTimeProvider;
			_exceptionLogMonitor = logMonitorFactory.CreateNew<IExceptionLogMonitor>();
		}
		
		public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
		{
			var context = OperationContext.Current;
			var incomingMessageProperties = context.IncomingMessageProperties;
			if (!incomingMessageProperties.ContainsKey(RemoteEndpointMessageProperty.Name))
				return;

			var endpointMessageProperty = incomingMessageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
			if (endpointMessageProperty == null)
				return;

			if (context == null)
				return;

			if (!context.IncomingMessageProperties.Keys.Contains(HttpRequest))
				return;

			var item = context.IncomingMessageProperties[HttpRequest] as HttpRequestMessageProperty;
			if (item == null)
				return;

			// this comment came from oleg, i don't know why is not loging when the message is empty
			//Check if message exists - for RESTful calls we don't have message body
			//if (OperationContext.Current.RequestContext.RequestMessage.IsEmpty)
			//return;

			RequestLogInfo requestLogInfo = new RequestLogInfo
			{
				Headers = item.Headers,
				Method = item.Method,
				QueryString = item.QueryString,
				Envelope = context.IncomingMessageVersion.Envelope,
				RequestMessage = context.RequestContext.RequestMessage

			};
			ServerLogInfo serverLogInfo = new ServerLogInfo
			{
				Host = incomingMessageProperties.Via.Host,
				Port = incomingMessageProperties.Via.Port,
				Scheme = incomingMessageProperties.Via.Scheme,
				OriginalString = incomingMessageProperties.Via.OriginalString,
				Query = incomingMessageProperties.Via.Query
			};

			RemoteClientLogInfo remoteClientLogInfo = new RemoteClientLogInfo
			{
				Address = endpointMessageProperty.Address,
				Port = endpointMessageProperty.Port,
				Hostname = Dns.GetHostEntry(endpointMessageProperty.Address).HostName,
				UserName = OperationContext.Current.ServiceSecurityContext?.PrimaryIdentity?.Name
			};

			_exceptionLogMonitor.AddServiceCallDetails(remoteClientLogInfo,requestLogInfo,serverLogInfo, () =>
				_clientDetailsProvider.GetDetails(OperationContext.Current));
			
			_exceptionLogMonitor.DataContainer.ApplicationContext = _identityManager.GetClientApplicationIdentity();

			var faultException = BuildFaultException(error);
			var messageFault = faultException.CreateMessageFault();
			fault = Message.CreateMessage(version, messageFault, faultException.Action);
			//if (fault != null)
			//{
			//    HttpResponseMessageProperty properties = new HttpResponseMessageProperty();
			//    properties.StatusCode = HttpStatusCode.Conflict;
			//    fault.Properties.Add(HttpResponseMessageProperty.Name, properties);
			//}
		}

		public bool HandleError(Exception exception)
		{
			//_coreApiException = ConvertToCoreApiException(exception);

			// Publish exception
			_exceptionLogMonitor.Publish(_coreApiException);

			// Return true to indicate the Exception has been handled
			return true;
		}
		
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

		private FaultException<CoreApiServiceFault> BuildFaultException(Exception exception)
		{
			//Convert non-CoreApi exception to CoreApi exception
			//Also covers special case - Any exception happened in WCF authentication-authorization chain
			//converted to "The caller was not authenticated by the service." automatically by WCF
			//Original exception was logged also to preserve all details
			_coreApiException = ConvertToCoreApiException(exception);

			var coreApiServiceFault = new CoreApiServiceFault
			{
				ErrorCode = _coreApiException.ErrorCode,
				ErrorMessage = _coreApiException.ClientMessage,
				ErrorTime = _currentDateTimeProvider.GetCurrentUtc(),
				ErrorIdentifier = _coreApiException.ErrorId
			};

			return new FaultException<CoreApiServiceFault>(coreApiServiceFault, ErrorMessage, new FaultCode("Server Error"));
		}

		private static CoreApiException ConvertToCoreApiException(Exception exception)
		{
			return exception as CoreApiException ??
				   (exception.Message.Equals(WcfSecurityErrorMessage, StringComparison.OrdinalIgnoreCase)
					   ? new CoreApiException(exception, TechnicalSubSystem.Authentication,
						   SubSystemError.SecurityTheCallerWasNotAuthenticatedByTheService)
					   : new CoreApiException(exception));
		}

		#endregion
	}
}