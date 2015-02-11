using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.ServiceModel;
using System.ServiceModel.Channels;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Foundation.Security
{
    public class AuthenticationManager : ServiceAuthenticationManager, IAuthenticationManager
    {
        #region Core

        private readonly IApplicationAuthenticator _authenticator;
        private readonly ILogManager _logManager;

        public AuthenticationManager(IApplicationAuthenticator authenticator, ILogManager logManager)
        {
            if (authenticator == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "authenticator");
            if (logManager == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "logManager");
            _authenticator = authenticator;
            _logManager = logManager;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Authenticates all incoming calls
        /// </summary>
        /// <param name="authPolicy"></param>
        /// <param name="listenUri"></param>
        /// <param name="message">Incoming message</param>
        /// <returns></returns>
        public override ReadOnlyCollection<IAuthorizationPolicy> Authenticate(
            ReadOnlyCollection<IAuthorizationPolicy> authPolicy, Uri listenUri, ref Message message)
        {
            //Any exception happened in WCF authentication-authorization chain
            //converted to "The caller was not authenticated by the service."
            //So we are loosing details and need to catch exception here also
            try
            {
                //If this is MEX contract call
                if (OperationContext.Current.EndpointDispatcher.IsSystemEndpoint)
                    return authPolicy;

                //return OperationContext.Current.ServiceSecurityContext.IsAnonymous
                //    ? _authenticator.Authenticate(authPolicy, listenUri, ref message)
                //    : authPolicy;
                return _authenticator.Authenticate(authPolicy, listenUri, ref message);
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
