using CES.CoreApi.Common.Models;
using CES.CoreApi.Foundation.Contract.Models;

namespace CES.CoreApi.Foundation.Contract.Interfaces
{
    public interface IRequestHeaderParametersProvider
    {
        ServiceCallHeaderParameters GetParameters();
    }
}