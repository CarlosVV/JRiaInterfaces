using System.ServiceModel;

namespace CES.CoreApi.Foundation.Contract.Interfaces
{
    public interface IAuthorizationAdministrator
    {
        bool ValidateAccess(OperationContext operationContext);
    }
}