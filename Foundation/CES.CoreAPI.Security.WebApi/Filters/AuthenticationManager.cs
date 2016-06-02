using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Security.Interfaces;
using CES.CoreApi.Security.WebApi.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace CES.CoreApi.Security.WebAPI.Filters
{
	public class AuthenticationManager : IAuthenticationFilter
	{
		private readonly IApplicationAuthenticator _authenticator;
		private readonly IWebApiRequestHeaderParametersService _parametersProvider;

		public bool AllowMultiple { get; set; }

		public AuthenticationManager(IApplicationAuthenticator authenticator, IWebApiRequestHeaderParametersService parametersProvider)
		{
			if (authenticator == null)
				throw new CoreApiException(TechnicalSubSystem.Authentication, SubSystemError.GeneralRequiredParameterIsUndefined, "authenticator");

			if (parametersProvider == null)
				throw new CoreApiException(TechnicalSubSystem.Authentication, SubSystemError.GeneralRequiredParameterIsUndefined, "requestHeaderParametersProviderFactory");

			_authenticator = authenticator;
			_parametersProvider = parametersProvider;
		}


		public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
		{
			
			context.Principal =  await _authenticator.AuthenticateAsync(
										_parametersProvider.GetParameters(context.ActionContext.ActionDescriptor.ActionName));
		}

		public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}
