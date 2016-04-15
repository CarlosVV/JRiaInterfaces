using System.Security.Principal;
using CES.CoreApi.Security.Models;

namespace CES.CoreApi.Security.Interfaces
{
    public interface IIdentityManager
    {
        ClientApplicationIdentity GetClientApplicationIdentity();
        void SetCurrentPrincipal(IPrincipal principal);
        IPrincipal GetCurrentPrincipal();
    }
}