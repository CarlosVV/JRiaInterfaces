using System.ServiceModel;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Security.Interfaces;
using System.Security.Principal;
using CES.CoreApi.Security.Wcf.Interfaces;

namespace CES.CoreApi.Security.Wcf
{
	public class AuthorizationManager : ServiceAuthorizationManager, IAuthorizationManager
	{
		private readonly IApplicationAuthorizator _authorizationAdministrator;

		public AuthorizationManager(IApplicationAuthorizator authorizationAdministrator)
		{
			if (authorizationAdministrator == null)
				throw new CoreApiException(TechnicalSubSystem.Authorization, SubSystemError.GeneralRequiredParameterIsUndefined, "authorizationAdministrator");

			_authorizationAdministrator = authorizationAdministrator;
		}

		public override bool CheckAccess(OperationContext operationContext)
		{
			if (operationContext.EndpointDispatcher.IsSystemEndpoint)
				return operationContext.EndpointDispatcher.IsSystemEndpoint;

			var principal = operationContext.IncomingMessageProperties["Principal"] as IPrincipal;
			operationContext.ServiceSecurityContext.AuthorizationContext.Properties["Principal"] = _authorizationAdministrator.ValidateAccess(principal);
			return operationContext.ServiceSecurityContext.AuthorizationContext.Properties["Principal"] != null;
		}
	}
}
