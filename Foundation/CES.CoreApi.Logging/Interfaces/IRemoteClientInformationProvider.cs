using System;
using System.Collections.Generic;
using System.ServiceModel;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Interfaces
{
	public interface IRemoteClientInformationProvider
	{
		void AddDetails(ExceptionLogDataContainer exceptionLogDataContainer, OperationContext context, Func<IDictionary<string, object>> getClientDetails);

		void AddDetails(ExceptionLogDataContainer exceptionLogDataContainer, int port, string address, Func<IDictionary<string, object>> getClientDetails);

	}
}