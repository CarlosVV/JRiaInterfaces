using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Security.Interfaces;
using CES.CoreApi.Security.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace CES.CoreApi.Security.Factories
{
	[Serializable]
	public class RequestHeaderParametersProviderFactory : Dictionary<string, Func<BaseRequestHeaderParameters>>, IRequestHeaderParametersProviderFactory
	{
		private const string requestHeaderParameter = "I{0}RequestHeaderParametersProvider";

		public T GetInstance<T>(string hostServiceType) where T : class
		{
			var name = GetRegistrationName(hostServiceType);
			return this[name]() as T;
		}

		private static string GetRegistrationName(string hostServiceType)
		{
			if (string.IsNullOrWhiteSpace(hostServiceType))
				throw new CoreApiException(TechnicalSubSystem.GeoLocationService, SubSystemError.GeneralInvalidParameterValue, "hostServiceType", hostServiceType);

			return string.Format(CultureInfo.InvariantCulture, requestHeaderParameter, hostServiceType);
		}
	}
}
