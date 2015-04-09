using System;
using System.Security.Principal;
using System.Threading;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Foundation.Contract.Models;

namespace CES.CoreApi.Foundation.Security
{
    public class IdentityManager : IIdentityManager
    {
        public ClientApplicationIdentity GetClientApplicationIdentity()
        {
            return Thread.CurrentPrincipal.Identity != null
                ? Thread.CurrentPrincipal.Identity as ClientApplicationIdentity
                : null;
        }

        public void SetCurrentPrincipal(IPrincipal principal)
        {
            if (principal == null) throw new ArgumentNullException("principal");
            Thread.CurrentPrincipal = principal;
        }

        public IPrincipal GetCurrentPrincipal()
        {
            return Thread.CurrentPrincipal;
        }
    }
}
