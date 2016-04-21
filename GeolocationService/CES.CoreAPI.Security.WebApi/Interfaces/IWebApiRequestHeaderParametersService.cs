
using CES.CoreApi.Foundation.Models;

namespace CES.CoreApi.Security.Interfaces
{
    public interface IWebApiRequestHeaderParametersService
    {
		ServiceCallHeaderParameters GetParameters(string operationName);
    }
}