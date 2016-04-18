using CES.CoreApi.Foundation.Models;
using System.Security.Principal;

namespace CES.CoreApi.Security.Interfaces
{
    public interface IApplicationAuthenticator
    {
        IPrincipal Authenticate(ServiceCallHeaderParameters serviceCallHeaderParameters);
    }
}