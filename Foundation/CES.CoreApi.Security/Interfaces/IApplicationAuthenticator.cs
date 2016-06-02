using CES.CoreApi.Foundation.Models;
using System.Security.Principal;
using System.Threading.Tasks;

namespace CES.CoreApi.Security.Interfaces
{
    public interface IApplicationAuthenticator
    {
		 Task<IPrincipal> AuthenticateAsync(ServiceCallHeaderParameters serviceCallHeaderParameters);
		IPrincipal Authenticate(ServiceCallHeaderParameters serviceCallHeaderParameters);
	}
}