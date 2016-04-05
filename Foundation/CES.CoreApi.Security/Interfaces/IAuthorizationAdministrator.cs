using System.ServiceModel;

namespace CES.CoreApi.Security.Interfaces
{
    public interface IAuthorizationAdministrator
    {
        bool ValidateAccess(OperationContext operationContext);
    }
}