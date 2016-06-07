
using CES.CoreApi.Security.Models;

namespace CES.CoreApi.Security.WebApi.Interfaces
{
    public interface IWebApiRequestHeaderParametersService
    {
		ServiceCallHeaderParameters GetParameters(string operationName);
    }
}