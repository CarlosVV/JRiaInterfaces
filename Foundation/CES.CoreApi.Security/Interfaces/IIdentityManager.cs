using CES.CoreApi.Security.Models;
using System.Security.Principal;


namespace CES.CoreApi.Security.Interfaces
{
    public interface IIdentityManager
    {
        ClientApplicationIdentity GetClientApplicationIdentity();
        void SetCurrentPrincipal(IPrincipal principal);
        IPrincipal GetCurrentPrincipal();
    }
}