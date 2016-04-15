using CES.CoreApi.Common.Models;
using CES.CoreApi.Foundation.Models;

namespace CES.CoreApi.Security.Interfaces
{
    public interface IWCFRequestHeaderParametersProvider
	{
		ServiceCallHeaderParameters GetParameters();
	}
}