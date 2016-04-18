using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Security.Enums;
using CES.CoreApi.Security.Interfaces;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace CES.CoreApi.Security.Managers.WebAPI
{
	public class ApplicationAuthenticator: Attribute, IAuthenticationFilter
	{
		private readonly IApplicationAuthenticator _authenticator;
		private readonly IRequestHeaderParametersProvider _parametersProvider;

		public bool AllowMultiple { get; set; }

		public ApplicationAuthenticator(IApplicationAuthenticator authenticator, IRequestHeaderParametersProviderFactory requestHeaderParametersProviderFactory)
		{
			if (authenticator == null)
				throw new CoreApiException(TechnicalSubSystem.Authentication, SubSystemError.GeneralRequiredParameterIsUndefined, "authenticator");

			if (requestHeaderParametersProviderFactory == null)
				throw new CoreApiException(TechnicalSubSystem.Authentication, SubSystemError.GeneralRequiredParameterIsUndefined, "requestHeaderParametersProviderFactory");

			_authenticator = authenticator;
			_parametersProvider = requestHeaderParametersProviderFactory.GetInstance<IRequestHeaderParametersProvider>(ServiceType.WebApi.ToString());
		}

		
		public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
		{
			HttpRequestMessage request = context.Request;
			AuthenticationHeaderValue authorization = request.Headers.Authorization;
			
			if (authorization == null || authorization.Scheme != "Basic")
				return;
			
			if (String.IsNullOrEmpty(authorization.Parameter))
			{
				//context.ErrorResult = new AuthenticationFailureResult("Missing credentials", request);
				return;
			}

			context.Principal = _authenticator.Authenticate(_parametersProvider.GetParameters());

		}

		public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
