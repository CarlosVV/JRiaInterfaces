using System;
using System.Collections.Generic;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Monitors
{
	public class ExceptionLogMonitor : BaseLogMonitor, IExceptionLogMonitor
	{
		private readonly IServiceCallInformationProvider _serviceCallDetailsProvider;

		public ExceptionLogMonitor(ExceptionLogDataContainer dataContainer, IServiceCallInformationProvider serviceCallDetailsProvider,
			ILoggerProxy logProxy, ILogConfigurationProvider configuration)
			: base(logProxy, configuration)
		{
			if (dataContainer == null) throw new ArgumentNullException("dataContainer");
			if (serviceCallDetailsProvider == null) throw new ArgumentNullException("serviceCallDetailsProvider");

			_serviceCallDetailsProvider = serviceCallDetailsProvider;
			DataContainer = dataContainer;
		}

		public ExceptionLogDataContainer DataContainer { get; private set; }

		public void AddServiceCallDetails(RemoteClientLogInfo remoteClientLogInfo, RequestLogInfo requestLogInfo, ServerLogInfo serverLogInfo, Func<IDictionary<string, object>> getClientDetails)
		{
			try
			{
				_serviceCallDetailsProvider.AddDetails(DataContainer, remoteClientLogInfo, requestLogInfo, serverLogInfo, getClientDetails);

			}
			// ReSharper disable EmptyGeneralCatchClause
			catch (Exception)
			// ReSharper restore EmptyGeneralCatchClause
			{
			}
		}

		public void Publish(Exception exception, string customMessage = null)
		{
			try
			{
				customMessage = string.IsNullOrEmpty(customMessage) ? string.Empty : customMessage;

				DataContainer.SetException(exception);
				DataContainer.CustomMessage = customMessage;

				Publish(DataContainer);
			}
			// ReSharper disable EmptyGeneralCatchClause
			catch (Exception)
			// ReSharper restore EmptyGeneralCatchClause
			{
			}
		}
	}
}
