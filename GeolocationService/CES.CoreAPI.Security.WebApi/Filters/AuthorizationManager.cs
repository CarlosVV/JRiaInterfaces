using CES.CoreApi.Security.Interfaces;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace CES.CoreAPI.Security.WebApi.Filters
{
	public class AuthorizationManager : IAuthorizationFilter
	{
		public bool AllowMultiple { get; set; }
		public IApplicationAuthorizator _authorizationAdministrator;

		public AuthorizationManager(IApplicationAuthorizator authorizationAdministrator)
		{
			_authorizationAdministrator = authorizationAdministrator;
		}

		public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
		{
			_authorizationAdministrator.ValidateAccess(Thread.CurrentPrincipal);
			return continuation(); 
		}
	}
}
