﻿using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Foundation.Providers;
using CES.CoreApi.Security.Enums;
using CES.CoreApi.Security.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace CES.CoreApi.Security.Managers.WebAPI
{
	public class ApplicationAuthenticator: IAuthenticationFilter
	{
		private readonly IApplicationAuthenticator _authenticator;
		private readonly IWebApiRequestHeaderParametersProvider _parametersProvider;

		public bool AllowMultiple { get; set; }
		
		public ApplicationAuthenticator(IApplicationAuthenticator authenticator, IWebApiRequestHeaderParametersProvider parametersProvider)
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
			var authorization = context.Request.Headers.Authorization;
			context.Principal = _authenticator.Authenticate(_parametersProvider.GetParameters(context.ActionContext.ActionDescriptor.ActionName));
		}

		public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
