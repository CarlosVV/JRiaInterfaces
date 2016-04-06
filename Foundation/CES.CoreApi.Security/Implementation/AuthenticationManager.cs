using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.ServiceModel;
using System.ServiceModel.Channels;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Security.Interfaces;

namespace CES.CoreApi.Security
{
	public class AuthenticationManager : ServiceAuthenticationManager, IAuthenticationManager
	{
		private readonly IApplicationAuthenticator _authenticator;

		public AuthenticationManager(IApplicationAuthenticator authenticator)
		{
			if (authenticator == null)
				throw new CoreApiException(TechnicalSubSystem.Authentication,
					SubSystemError.GeneralRequiredParameterIsUndefined, "authenticator");

			_authenticator = authenticator;
		}

		public override ReadOnlyCollection<IAuthorizationPolicy> Authenticate(ReadOnlyCollection<IAuthorizationPolicy> authPolicy, Uri listenUri, ref Message message)
		{
			if (OperationContext.Current.EndpointDispatcher.IsSystemEndpoint)
				return authPolicy;

			return _authenticator.Authenticate(authPolicy, listenUri, ref message);
		}
	}
}
