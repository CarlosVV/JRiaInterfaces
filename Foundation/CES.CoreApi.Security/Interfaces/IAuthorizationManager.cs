using System.ServiceModel;

namespace CES.CoreApi.Security.Interfaces
{
    public interface IAuthorizationManager
    {
        bool CheckAccess(OperationContext operationContext);
    }
}