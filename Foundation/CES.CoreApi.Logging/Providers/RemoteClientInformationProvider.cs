using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Providers
{
    public class RemoteClientInformationProvider : IRemoteClientInformationProvider
    {
        #region Public methods

        /// <summary>
        /// Adds remote client details to exception log data container
        /// </summary>
        /// <param name="exceptionLogDataContainer">Exception log data container</param>
        /// <param name="context">Service operation context</param>
        /// <param name="getClientDetails"></param>
        public void AddDetails(ExceptionLogDataContainer exceptionLogDataContainer, OperationContext context, Func<IDictionary<string, object>> getClientDetails)
        {
            if (context == null) return;

            var group = exceptionLogDataContainer.GetGroupByTitle("Remote Client Details");
           
            var incomingMessageProperties = context.IncomingMessageProperties;
            if (!incomingMessageProperties.ContainsKey(RemoteEndpointMessageProperty.Name)) return;

            var endpointMessageProperty = incomingMessageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            if (endpointMessageProperty == null) return;

            //Need to handle possible exceptions happened during host name detection
            try
            {
                group.AddItem("Host Name", Dns.GetHostEntry(endpointMessageProperty.Address).HostName);
            }
            catch(Exception)
            {
                group.AddItem("Host Name", "Cannot be detected");
            }
            group.AddItem("Port", endpointMessageProperty.Port);
            group.AddItem("IP Address", endpointMessageProperty.Address);
            if (OperationContext.Current.ServiceSecurityContext != null)
            {
                group.AddItem("User Name", OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name);                
            }

            //Populate additonal client details - usually security context coming from service
            foreach (var entry in getClientDetails())
            {
                group.AddItem(entry.Key, entry.Value);
            }
        }

        #endregion //Public methods
    }
}
