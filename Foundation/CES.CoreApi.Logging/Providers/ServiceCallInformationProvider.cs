using System;
using System.Collections.Generic;
using System.ServiceModel;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Providers
{
    public class ServiceCallInformationProvider : IServiceCallInformationProvider
    {
        #region Core

        private readonly IRemoteClientInformationProvider _remoteClientInformationProvider;
        private readonly IHtppRequestInformationProvider _htppRequestInformationProvider;
        private readonly IServerInformationProvider _serverInformationProvider;

        /// <summary>
        /// Initializes ServiceCallInformationProvider instance
        /// </summary>
        /// <param name="remoteClientInformationProvider">Remote Client Details Provider instance</param>
        /// <param name="htppRequestInformationProvider">Htpp Request Details Provider instance</param>
        /// <param name="serverInformationProvider">Server details information provider instance </param>
        public ServiceCallInformationProvider(IRemoteClientInformationProvider remoteClientInformationProvider,
            IHtppRequestInformationProvider htppRequestInformationProvider, IServerInformationProvider serverInformationProvider)
        {
            if (remoteClientInformationProvider == null) 
                throw new ArgumentNullException("remoteClientInformationProvider");
            if (htppRequestInformationProvider == null) 
                throw new ArgumentNullException("htppRequestInformationProvider");
            if (serverInformationProvider == null) 
                throw new ArgumentNullException("serverInformationProvider");

            _remoteClientInformationProvider = remoteClientInformationProvider;
            _htppRequestInformationProvider = htppRequestInformationProvider;
            _serverInformationProvider = serverInformationProvider;
        }

        #endregion //Core

        #region Public methods

        /// <summary>
        /// Adds incoming message details to exception log data container
        /// </summary>
        /// <param name="exceptionLogDataContainer">Exception log data container</param>
        /// <param name="context">Service operation context</param>
        /// <param name="getClientDetails"></param>
        public void AddDetails(ExceptionLogDataContainer exceptionLogDataContainer, OperationContext context, Func<IDictionary<string, object>> getClientDetails)
        {
            if (context == null) return;

            //Add remote client details to the data container
            _remoteClientInformationProvider.AddDetails(exceptionLogDataContainer, context, getClientDetails);

            //Add http request details to the data container
            _htppRequestInformationProvider.AddDetails(exceptionLogDataContainer, context);

            //Add server details to the data container
            _serverInformationProvider.AddDetails(exceptionLogDataContainer, context);
        }

        #endregion //Public methods
    }
}
