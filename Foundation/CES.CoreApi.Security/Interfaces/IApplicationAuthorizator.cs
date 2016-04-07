using System.Security.Principal;
using System.ServiceModel;

namespace CES.CoreApi.Security.Interfaces
{
    public interface IApplicationAuthorizator
    {
        IPrincipal ValidateAccess(IPrincipal principal);
    }
}