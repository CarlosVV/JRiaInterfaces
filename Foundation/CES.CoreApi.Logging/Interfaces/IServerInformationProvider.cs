﻿using System.ServiceModel;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Interfaces
{
    public interface IServerInformationProvider
    {
        /// <summary>
        /// Adds server details to exception log data container
        /// </summary>
        /// <param name="exceptionLogDataContainer">Exception log data container</param>
        /// <param name="context"></param>
        void AddDetails(ExceptionLogDataContainer exceptionLogDataContainer, OperationContext context);
    }
}