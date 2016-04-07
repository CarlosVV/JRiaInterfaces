using System;
using System.ServiceModel;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Security.Interfaces;
using System.Security.Principal;

namespace CES.CoreApi.Security
{
	public class AuthorizationManager : ServiceAuthorizationManager, IAuthorizationManager
	{
		private readonly IApplicationAuthorizator _authorizationAdministrator;

		public AuthorizationManager(IApplicationAuthorizator authorizationAdministrator)
		{
			if (authorizationAdministrator == null)
				throw new CoreApiException(TechnicalSubSystem.Authorization,
				   SubSystemError.GeneralRequiredParameterIsUndefined, "authorizationAdministrator");

			_authorizationAdministrator = authorizationAdministrator;
		}

		public override bool CheckAccess(OperationContext operationContext)
		{
			var principal = operationContext.IncomingMessageProperties["Principal"] as IPrincipal;
			operationContext.ServiceSecurityContext.AuthorizationContext.Properties["Principal"] = _authorizationAdministrator.ValidateAccess(principal);
			return operationContext.EndpointDispatcher.IsSystemEndpoint || operationContext.ServiceSecurityContext.AuthorizationContext.Properties["Principal"] != null;
		}
	}
}
