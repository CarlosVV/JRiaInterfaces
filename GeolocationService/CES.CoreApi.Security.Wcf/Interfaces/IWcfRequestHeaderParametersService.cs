
using CES.CoreApi.Foundation.Models;

namespace CES.CoreApi.Security.Wcf.Interfaces
{
    public interface IWcfRequestHeaderParametersService
    {
		ServiceCallHeaderParameters GetParameters();
    }
}