using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Security.Enums;
using CES.CoreApi.Security.Interfaces;
using CES.CoreApi.Security.Wcf.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace CES.CoreApi.Security.Wcf
{
	public class AuthenticationManager : ServiceAuthenticationManager, IAuthenticationManager
	{
		private readonly IApplicationAuthenticator _authenticator;
		private readonly IWcfRequestHeaderParametersProvider _parametersProvider;

		public AuthenticationManager(IApplicationAuthenticator authenticator, IWcfRequestHeaderParametersProvider parametersProvider)
		{
			if (authenticator == null)
				throw new CoreApiException(TechnicalSubSystem.Authentication, SubSystemError.GeneralRequiredParameterIsUndefined, "authenticator");

			if (parametersProvider == null)
				throw new CoreApiException(TechnicalSubSystem.Authentication, SubSystemError.GeneralRequiredParameterIsUndefined, "requestHeaderParametersProviderFactory");

			_authenticator = authenticator;
			_parametersProvider = parametersProvider;
		}

		public override ReadOnlyCollection<IAuthorizationPolicy> Authenticate(ReadOnlyCollection<IAuthorizationPolicy> authPolicy, Uri listenUri, ref Message message)
		{
			if (OperationContext.Current.EndpointDispatcher.IsSystemEndpoint)
				return authPolicy;
			
			message.Properties["Principal"] = _authenticator.Authenticate(_parametersProvider.GetParameters());
			return authPolicy;
		}
	}
}
