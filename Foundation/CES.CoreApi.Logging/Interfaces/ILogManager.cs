using System;
using System.Collections.Generic;
using System.ServiceModel;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Interfaces
{
    public interface ILogManager
    {
        /// <summary>
        /// Provides new instance of Log monitor
        /// </summary>
        /// <returns></returns>
        T GetMonitorInstance<T>() where T : class;

        /// <summary>
        /// Gets exception log data contianer and populates it by service call details
        /// </summary>
        /// <param name="context">Service operation context</param>
        /// <param name="getClientDetails"></param>
        /// <returns></returns>
        ExceptionLogDataContainer GetExceptionLogDataContainerWithCallDetails(OperationContext context, Func<IDictionary<string, object>> getClientDetails);

        /// <summary>
        /// Logs entry data
        /// </summary>
        /// <param name="dataContainer">Performance log entry data container</param>
        void Publish(IDataContainer dataContainer);

        void Publish(Exception exception, string customMessage = null, ExceptionLogDataContainer dataContainer = null);

        /// <summary>
        /// Provides new instance of data container
        /// </summary>
        /// <returns></returns>
        T GetContainerInstance<T>() where T : class, IDataContainer;
    }
}