
using CES.CoreApi.Security.Models;

namespace CES.CoreApi.Foundation.Security.Interfaces
{
    public interface IServiceCallHeaderParametersProvider
    {
        ServiceCallHeaderParameters GetParameters();
    }
}