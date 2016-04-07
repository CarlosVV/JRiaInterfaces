using System;
using System.Collections.Generic;
using System.ServiceModel;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Interfaces
{
	public interface IExceptionLogMonitor
	{
		ExceptionLogDataContainer DataContainer { get; }

		void AddServiceCallDetails(OperationContext context, Func<IDictionary<string, object>> getClientDetails);

		void Publish(Exception exception, string customMessage = null);
	}
}