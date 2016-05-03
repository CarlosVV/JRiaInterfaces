using System.ServiceModel;

namespace CES.CoreApi.Security.Wcf.Interfaces
{
    public interface IAuthorizationManager
    {
        bool CheckAccess(OperationContext operationContext);
    }
}