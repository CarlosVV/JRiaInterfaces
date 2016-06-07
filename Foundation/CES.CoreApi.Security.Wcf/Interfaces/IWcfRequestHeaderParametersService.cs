

using CES.CoreApi.Security.Models;

namespace CES.CoreApi.Security.Wcf.Interfaces
{
    public interface IWcfRequestHeaderParametersService
    {
		ServiceCallHeaderParameters GetParameters();
    }
}