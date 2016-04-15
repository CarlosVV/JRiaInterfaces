using System;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Security;

namespace CES.CoreApi.Foundation.Providers
{
    public class WebAPIRequestHeaderParametersProvider : BaseRequestHeaderParameters, IRequestHeaderParametersProvider
	{
        public ServiceCallHeaderParameters GetParameters()
        {
			throw new NotImplementedException();
        }
	}
}
