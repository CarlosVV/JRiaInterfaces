using System.Collections.Generic;
using System.Security.Principal;
using System.ServiceModel;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Foundation.Contract.Interfaces;

namespace CES.CoreApi.Foundation.Providers
{
    public class ClientDetailsProvider : IClientSecurityContextProvider
    {
        /// <summary>
        /// Provides client applicaiton details as a dictionary
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IDictionary<string, object> GetDetails(OperationContext context)
        {
            var clientDetails = new Dictionary<string, object>();

            if (context == null)
                return clientDetails;

            if (!OperationContext.Current.IncomingMessageProperties.ContainsKey("Principal"))
            {
                clientDetails.Add("Is Authenticated", false);
                return clientDetails;
            }

            var clientPrincipal = OperationContext.Current.IncomingMessageProperties["Principal"] as IPrincipal;
            var clientIdentity = clientPrincipal != null ? clientPrincipal.Identity : null;

            if (clientIdentity == null)
                return clientDetails;

            clientDetails.Add("Application Name", clientIdentity.Name);
            clientDetails.Add("Is Authenticated", clientIdentity.IsAuthenticated);
            clientDetails.Add("Authentication Type", clientIdentity.AuthenticationType);

            var serviceIdentity = clientIdentity as ClientApplicationIdentity;
            if (serviceIdentity != null)
                clientDetails.Add("Application ID", serviceIdentity.ApplicationId);

            return clientDetails;
        }
    }
}
