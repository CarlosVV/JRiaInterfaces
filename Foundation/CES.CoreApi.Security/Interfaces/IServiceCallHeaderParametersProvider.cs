using CES.CoreApi.Security.Models;

namespace CES.CoreApi.Security.Interfaces
{
    public interface IServiceCallHeaderParametersProvider
    {
        ServiceCallHeaderParameters GetParameters();
    }
}