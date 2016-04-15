
using CES.CoreApi.Foundation.Models;

namespace CES.CoreApi.Security.Interfaces
{
    public interface IRequestHeaderParametersProvider
    {
		ServiceCallHeaderParameters GetParameters();
    }
}