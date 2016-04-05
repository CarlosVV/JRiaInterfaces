using System;
using System.ServiceModel;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Security.Interfaces;

namespace CES.CoreApi.Security
{
	public class AuthorizationManager : ServiceAuthorizationManager, IAuthorizationManager
	{
		private readonly IAuthorizationAdministrator _authorizationAdministrator;

		public AuthorizationManager(IAuthorizationAdministrator authorizationAdministrator)
		{
			if (authorizationAdministrator == null)
				throw new CoreApiException(TechnicalSubSystem.Authorization,
				   SubSystemError.GeneralRequiredParameterIsUndefined, "authorizationAdministrator");

			_authorizationAdministrator = authorizationAdministrator;
		}

		public override bool CheckAccess(OperationContext operationContext)
		{
			return operationContext.EndpointDispatcher.IsSystemEndpoint || _authorizationAdministrator.ValidateAccess(operationContext);
		}
	}
}
