using CES.CoreApi.Foundation.Contract.Interfaces;
using System.Security.Principal;

namespace CES.CoreApi.Foundation.Contract.Interfaces
{
    public interface IIdentityManager
    {
        IClientApplicationIdentity GetClientApplicationIdentity();
        void SetCurrentPrincipal(IPrincipal principal);
        IPrincipal GetCurrentPrincipal();
    }
}