using System.ServiceModel;

namespace CES.CoreApi.Foundation.Security.Interfaces
{
    public interface IAuthorizationManager
    {
        bool CheckAccess(OperationContext operationContext);
    }
}