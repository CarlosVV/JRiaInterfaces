
using CES.CoreApi.Foundation.Models;

namespace CES.CoreApi.Security.Interfaces
{
    public interface IWebApiRequestHeaderParametersProvider
    {
		ServiceCallHeaderParameters GetParameters(string operationName);
    }
}