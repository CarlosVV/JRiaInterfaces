using System.Security.Principal;
using CES.CoreApi.Foundation.Contract.Models;

namespace CES.CoreApi.Foundation.Contract.Interfaces
{
    public interface IIdentityManager
    {
        ClientApplicationIdentity GetClientApplicationIdentity();
        void SetCurrentPrincipal(IPrincipal principal);
        IPrincipal GetCurrentPrincipal();
    }
}