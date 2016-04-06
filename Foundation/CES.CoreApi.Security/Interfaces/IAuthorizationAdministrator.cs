using System.ServiceModel;

namespace CES.CoreApi.Foundation.Security.Interfaces
{
    public interface IAuthorizationAdministrator
    {
        bool ValidateAccess(OperationContext operationContext);
    }
}