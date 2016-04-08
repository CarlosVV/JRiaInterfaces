using System;
using System.Collections.Generic;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Models;

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
			if (remoteClientInformationProvider == null) throw new ArgumentNullException("remoteClientInformationProvider");
			if (httpRequestInformationProvider == null) throw new ArgumentNullException("httpRequestInformationProvider");
			if (serverInformationProvider == null) throw new ArgumentNullException("serverInformationProvider");

			_remoteClientInformationProvider = remoteClientInformationProvider;
			_httpRequestInformationProvider = httpRequestInformationProvider;
			_serverInformationProvider = serverInformationProvider;
		}

		public void AddDetails(ExceptionLogDataContainer exceptionLogDataContainer
			, RemoteClientLogInfo remoteClientLogInfo, RequestLogInfo requestLogInfo, ServerLogInfo serverLogInfo
			, Func<IDictionary<string, object>> getClientDetails)
		{
			_remoteClientInformationProvider.AddDetails(exceptionLogDataContainer, remoteClientLogInfo, getClientDetails);
			
			_httpRequestInformationProvider.AddDetails(exceptionLogDataContainer, requestLogInfo);
			
			_serverInformationProvider.AddDetails(exceptionLogDataContainer, serverLogInfo);
		}
	}
}
