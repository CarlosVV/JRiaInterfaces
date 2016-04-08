using System;
using System.Collections.Generic;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Interfaces
{
	public interface IRemoteClientInformationProvider
	{
		void AddDetails(ExceptionLogDataContainer exceptionLogDataContainer, RemoteClientLogInfo remoteClientLogInfo, Func<IDictionary<string, object>> getClientDetails);

	}
}