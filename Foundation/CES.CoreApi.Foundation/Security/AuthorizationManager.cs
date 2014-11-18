using System;
using System.ServiceModel;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.Foundation.Contract.Exceptions;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Foundation.Security
{
    public class AuthorizationManager : ServiceAuthorizationManager, IAuthorizationManager
    {
        #region Core

        private readonly IAuthorizationAdministrator _authorizationAdministrator;
        private readonly ILogManager _logManager;

        public AuthorizationManager(IAuthorizationAdministrator authorizationAdministrator, ILogManager logManager)
        {
            if (authorizationAdministrator == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "authorizationAdministrator");
            if (logManager == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                  SubSystemError.GeneralRequiredParameterIsUndefined, "logManager");
            _authorizationAdministrator = authorizationAdministrator;
            _logManager = logManager;
        }

        #endregion

        #region Public methods

        public override bool CheckAccess(OperationContext operationContext)
        {
            //Any exception happened in WCF authentication-authorization chain
            //converted to "The caller was not authenticated by the service."
            //So we are loosing details and need to catch exception here also
            try
            {
                return operationContext.EndpointDispatcher.IsSystemEndpoint ||
                   _authorizationAdministrator.ValidateAccess(operationContext);
            }
            catch (Exception ex)
            {
                _logManager.Publish(ex);
                throw;
            }
        }

        #endregion
    }
}
