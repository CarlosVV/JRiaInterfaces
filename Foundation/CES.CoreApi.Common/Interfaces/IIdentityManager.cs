using System.Security.Principal;
using CES.CoreApi.Common.Models;

namespace CES.CoreApi.Common.Interfaces
{
    public interface IIdentityManager
    {
        ClientApplicationIdentity GetClientApplicationIdentity();
        void SetCurrentPrincipal(IPrincipal principal);
        IPrincipal GetCurrentPrincipal();
    }
}