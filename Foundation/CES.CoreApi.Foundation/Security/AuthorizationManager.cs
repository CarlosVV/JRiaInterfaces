using System;
using System.ServiceModel;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Foundation.Security
{
    public class AuthorizationManager : ServiceAuthorizationManager, IAuthorizationManager
    {
        #region Core

        private readonly IAuthorizationAdministrator _authorizationAdministrator;
        private readonly IExceptionLogMonitor _exceptionMonitor;

        public AuthorizationManager(IAuthorizationAdministrator authorizationAdministrator, IExceptionLogMonitor exceptionMonitor)
        {
            if (authorizationAdministrator == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "authorizationAdministrator");
            if (exceptionMonitor == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                  SubSystemError.GeneralRequiredParameterIsUndefined, "exceptionMonitor");
            _authorizationAdministrator = authorizationAdministrator;
            _exceptionMonitor = exceptionMonitor;
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
                _exceptionMonitor.Publish(ex);
                throw;
            }
        }

        #endregion
    }
}
