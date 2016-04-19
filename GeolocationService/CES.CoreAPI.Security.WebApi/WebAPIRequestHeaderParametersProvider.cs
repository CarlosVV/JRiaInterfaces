using System;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Security;
using CES.CoreApi.Foundation.Models;
using CES.CoreApi.Security.Interfaces;
using CES.CoreApi.Security.Models;

namespace CES.CoreApi.Foundation.Providers
{
    public class WebApiRequestHeaderParametersProvider : BaseRequestHeaderParameters, IRequestHeaderParametersProvider
	{
        public ServiceCallHeaderParameters GetParameters()
        {
			throw new NotImplementedException();
        }
	}
}
