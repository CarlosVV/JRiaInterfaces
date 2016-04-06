using System.ServiceModel;

namespace CES.CoreApi.Security.Interfaces
{
    public interface IApplicationAuthorizator
    {
        bool ValidateAccess(OperationContext operationContext);
    }
}