using System;
using System.Collections.Generic;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Interfaces
{
	public interface IServiceCallInformationProvider
	{
		void AddDetails(ExceptionLogDataContainer exceptionLogDataContainer, RemoteClientLogInfo remoteClientLogInfo, RequestLogInfo requestLogInfo, ServerLogInfo serverLogInfo, Func<IDictionary<string, object>> getClientDetails);

	}
}