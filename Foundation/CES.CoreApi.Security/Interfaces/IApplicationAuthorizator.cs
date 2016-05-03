using System.Security.Principal;

namespace CES.CoreApi.Security.Interfaces
{
    public interface IApplicationAuthorizator
    {
        IPrincipal ValidateAccess(IPrincipal principal);
    }
}