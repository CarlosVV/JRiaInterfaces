using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.ServiceModel;
using System.ServiceModel.Channels;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.Foundation.Contract.Exceptions;
using CES.CoreApi.Foundation.Contract.Interfaces;

namespace CES.CoreApi.Foundation.Security
{
    public class AuthenticationManager : ServiceAuthenticationManager, IAuthenticationManager
    {
        #region Core

        private readonly IApplicationAuthenticator _authenticator;

        public AuthenticationManager(IApplicationAuthenticator authenticator)
        {
            if (authenticator == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "authenticator");
            _authenticator = authenticator;
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
            //If this is MEX contract call
            if (OperationContext.Current.EndpointDispatcher.IsSystemEndpoint)
                return authPolicy;
            
            return OperationContext.Current.ServiceSecurityContext.IsAnonymous
                ? _authenticator.Authenticate(authPolicy, listenUri, ref message)
                : authPolicy;
        }

        #endregion
    }
}
