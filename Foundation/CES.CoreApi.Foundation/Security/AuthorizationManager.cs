using System.ServiceModel;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.Foundation.Contract.Exceptions;
using CES.CoreApi.Foundation.Contract.Interfaces;

namespace CES.CoreApi.Foundation.Security
{
    public class AuthorizationManager : ServiceAuthorizationManager, IAuthorizationManager
    {
        #region Core

        private readonly IAuthorizationAdministrator _authorizationAdministrator;

        public AuthorizationManager(IAuthorizationAdministrator authorizationAdministrator)
        {
            if (authorizationAdministrator == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "authorizationAdministrator");
            _authorizationAdministrator = authorizationAdministrator;
        }

        #endregion

        #region Public methods

        public override bool CheckAccess(OperationContext operationContext)
        {
            return operationContext.EndpointDispatcher.IsSystemEndpoint ||
                   _authorizationAdministrator.ValidateAccess(operationContext);
        }

        #endregion
    }
}
