using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.ServiceModel.Channels;

namespace CES.CoreApi.Foundation.Security.Interfaces
{
    public interface IApplicationAuthenticator
    {
        ReadOnlyCollection<IAuthorizationPolicy> Authenticate(ReadOnlyCollection<IAuthorizationPolicy> authPolicy, 
            Uri listenUri, ref Message message);
    }
}