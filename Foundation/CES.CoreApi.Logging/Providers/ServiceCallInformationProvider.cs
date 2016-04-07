using System;
using System.Collections.Generic;
using System.ServiceModel;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Models;
using System.ServiceModel.Channels;

namespace CES.CoreApi.Logging.Providers
{
	public class ServiceCallInformationProvider : IServiceCallInformationProvider
	{

		private readonly IRemoteClientInformationProvider _remoteClientInformationProvider;
		private readonly IHttpRequestInformationProvider _httpRequestInformationProvider;
		private readonly IServerInformationProvider _serverInformationProvider;

		/// <summary>
		/// Initializes ServiceCallInformationProvider instance
		/// </summary>
		/// <param name="remoteClientInformationProvider">Remote Client Details Provider instance</param>
		/// <param name="httpRequestInformationProvider">Htpp Request Details Provider instance</param>
		/// <param name="serverInformationProvider">Server details information provider instance </param>
		public ServiceCallInformationProvider(IRemoteClientInformationProvider remoteClientInformationProvider,
			IHttpRequestInformationProvider httpRequestInformationProvider, IServerInformationProvider serverInformationProvider)
		{
			if (remoteClientInformationProvider == null)
				throw new ArgumentNullException("remoteClientInformationProvider");
			if (httpRequestInformationProvider == null)
				throw new ArgumentNullException("httpRequestInformationProvider");
			if (serverInformationProvider == null)
				throw new ArgumentNullException("serverInformationProvider");

			_remoteClientInformationProvider = remoteClientInformationProvider;
			_httpRequestInformationProvider = httpRequestInformationProvider;
			_serverInformationProvider = serverInformationProvider;
		}

		public void AddDetails(ExceptionLogDataContainer exceptionLogDataContainer, int port, string address, Func<IDictionary<string, object>> getClientDetails)
		{

			

			_remoteClientInformationProvider.AddDetails(exceptionLogDataContainer, port, address, getClientDetails);




			//Add http request details to the data container
			_httpRequestInformationProvider.AddDetails(exceptionLogDataContainer, context);

			//Add server details to the data container
			_serverInformationProvider.AddDetails(exceptionLogDataContainer, context);
		}

		public void AddDetails(ExceptionLogDataContainer exceptionLogDataContainer, OperationContext context, Func<IDictionary<string, object>> getClientDetails)
		{
			if (context == null)
				return;

			//Add remote client details to the data container
			//_remoteClientInformationProvider.AddDetails(exceptionLogDataContainer, context, getClientDetails);

			var incomingMessageProperties = context.IncomingMessageProperties;
			if (!incomingMessageProperties.ContainsKey(RemoteEndpointMessageProperty.Name))
				return;

			var endpointMessageProperty = incomingMessageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
			if (endpointMessageProperty == null)
				return;

			_remoteClientInformationProvider.AddDetails(exceptionLogDataContainer, endpointMessageProperty.Port, endpointMessageProperty.Address, getClientDetails);




			//Add http request details to the data container
			_httpRequestInformationProvider.AddDetails(exceptionLogDataContainer, context);

			//Add server details to the data container
			_serverInformationProvider.AddDetails(exceptionLogDataContainer, context);
		}

	}
}
