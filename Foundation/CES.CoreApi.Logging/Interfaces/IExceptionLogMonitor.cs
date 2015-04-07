using System;
using System.Collections.Generic;
using System.ServiceModel;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Interfaces
{
    public interface IExceptionLogMonitor
    {
        /// <summary>
        /// Gets or sets performance log data container instance
        /// </summary>
        ExceptionLogDataContainer DataContainer { get; }

        /// <summary>
        /// Adds service call details to exception log data contianer 
        /// </summary>
        /// <param name="context">Service operation context</param>
        /// <param name="getClientDetails"></param>
        /// <returns></returns>
        void AddServiceCallDetails(OperationContext context,
            Func<IDictionary<string, object>> getClientDetails);

        void Publish(Exception exception, string customMessage = null);
    }
}