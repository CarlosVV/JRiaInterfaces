
using System.Security.Principal;

namespace CES.CoreApi.Security.Interfaces
{
    public interface IIdentityProvider
    {
        IClientApplicationIdentity GetClientApplicationIdentity();
        void SetCurrentPrincipal(IPrincipal principal);
        IPrincipal GetCurrentPrincipal();
    }
}