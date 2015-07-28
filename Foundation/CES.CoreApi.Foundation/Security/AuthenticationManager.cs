using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.ServiceModel;
using System.ServiceModel.Channels;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Foundation.Security
{
    public class AuthenticationManager : ServiceAuthenticationManager, IAuthenticationManager
    {
        #region Core

        private readonly IApplicationAuthenticator _authenticator;
        private readonly IExceptionLogMonitor _exceptionMonitor;

        public AuthenticationManager(IApplicationAuthenticator authenticator, IExceptionLogMonitor exceptionMonitor)
        {
            if (authenticator == null)
                throw new CoreApiException(TechnicalSubSystem.Authentication,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "authenticator");
            if (exceptionMonitor == null)
                throw new CoreApiException(TechnicalSubSystem.Authentication,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "exceptionMonitor");
            _authenticator = authenticator;
            _exceptionMonitor = exceptionMonitor;
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
                _exceptionMonitor.Publish(ex);
                throw;
            }
        }

        #endregion
    }
}
