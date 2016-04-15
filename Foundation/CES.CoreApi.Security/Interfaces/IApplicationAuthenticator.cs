using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.Security.Principal;
using System.ServiceModel.Channels;

namespace CES.CoreApi.Security.Interfaces
{
    public interface IApplicationAuthenticator
    {
        IPrincipal Authenticate();
    }
}