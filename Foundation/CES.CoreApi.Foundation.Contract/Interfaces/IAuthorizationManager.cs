using System.ServiceModel;

namespace CES.CoreApi.Foundation.Contract.Interfaces
{
    public interface IAuthorizationManager
    {
        bool CheckAccess(OperationContext operationContext);
    }
}