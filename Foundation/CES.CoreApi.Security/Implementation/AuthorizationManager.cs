using System.ServiceModel;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Security.Interfaces;

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
			return operationContext.EndpointDispatcher.IsSystemEndpoint || _authorizationAdministrator.ValidateAccess(operationContext);
		}
	}
}
