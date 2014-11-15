using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.ServiceModel.Channels;

namespace CES.CoreApi.Foundation.Contract.Interfaces
{
    public interface IAuthenticationManager
    {
        /// <summary>
        /// Authenticates all incoming calls
        /// </summary>
        /// <param name="authPolicy"></param>
        /// <param name="listenUri"></param>
        /// <param name="message">Incoming message</param>
        /// <returns></returns>
        ReadOnlyCollection<IAuthorizationPolicy> Authenticate(
            ReadOnlyCollection<IAuthorizationPolicy> authPolicy, Uri listenUri, ref Message message);
    }
}