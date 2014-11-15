using System.ServiceModel;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Providers
{
    public class ServerInformationProvider : IServerInformationProvider
    {
        #region Public methods

        /// <summary>
        /// Adds server details to exception log data container
        /// </summary>
        /// <param name="exceptionLogDataContainer">Exception log data container</param>
        /// <param name="context">Service operation context</param>
        public void AddDetails(ExceptionLogDataContainer exceptionLogDataContainer, OperationContext context)
        {
            if (context == null) return;

            var incomingMessageProperties = context.IncomingMessageProperties;
            var group = exceptionLogDataContainer.GetGroupByTitle("Server Details");
            group.AddItem("Host", incomingMessageProperties.Via.Host);
            group.AddItem("Port", incomingMessageProperties.Via.Port);
            group.AddItem("Scheme", incomingMessageProperties.Via.Scheme);
            group.AddItem("URL", incomingMessageProperties.Via.OriginalString);
            group.AddItem("Query", incomingMessageProperties.Via.Query);
        }

        #endregion //Public methods
    }
}
