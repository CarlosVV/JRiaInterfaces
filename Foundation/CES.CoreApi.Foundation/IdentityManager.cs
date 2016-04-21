using System;
using System.Security.Principal;
using System.Threading;
using CES.CoreApi.Security.Models;
using CES.CoreApi.Foundation.Contract.Interfaces;

namespace CES.CoreApi.Security
{
    public class IdentityManager : IIdentityManager
    {
        public IClientApplicationIdentity GetClientApplicationIdentity()
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
