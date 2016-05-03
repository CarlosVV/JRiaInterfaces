﻿using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.ServiceModel.Channels;

namespace CES.CoreApi.Security.Wcf.Interfaces
{
    public interface IAuthenticationManager
    {
        ReadOnlyCollection<IAuthorizationPolicy> Authenticate(ReadOnlyCollection<IAuthorizationPolicy> authPolicy, Uri listenUri, ref Message message);
    }
}