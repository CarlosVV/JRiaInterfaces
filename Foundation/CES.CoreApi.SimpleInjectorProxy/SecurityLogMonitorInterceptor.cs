using System;
//using Castle.DynamicProxy;
//using CES.CoreApi.Security.Interfaces;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Models;
//using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.Security.Interfaces;

namespace CES.CoreApi.SimpleInjectorProxy
{
	public class SecurityLogMonitorInterceptor : IInterceptor
	{
		private readonly ISecurityLogMonitor _securityLogMonitor;
		private readonly IIdentityProvider _identityProvider;


		public SecurityLogMonitorInterceptor(ISecurityLogMonitor securityLogMonitor, IIdentityProvider identityManager)
		{
			if (securityLogMonitor == null)
				throw new ArgumentNullException("securityLogMonitor");
			if (identityManager == null) throw new ArgumentNullException("identityManager");
			_securityLogMonitor = securityLogMonitor;
			_identityProvider = identityManager;

		}

		public void Intercept(IInvocation invocation)
		{
			invocation.Proceed();

			var clientApplicationIdentity = _identityProvider.GetClientApplicationIdentity();

			var securityAuditParameters = new SecurityAuditParameters
			{
				ClientApplicationId = clientApplicationIdentity.ApplicationId,
				Operation = clientApplicationIdentity.OperationName,
				ServiceApplicationId = 8000//TODO Get using simple app config  manager 
			};

			_securityLogMonitor.DataContainer.ApplicationContext = clientApplicationIdentity;
			_securityLogMonitor.LogSuccess(securityAuditParameters);
		}
	}
}
