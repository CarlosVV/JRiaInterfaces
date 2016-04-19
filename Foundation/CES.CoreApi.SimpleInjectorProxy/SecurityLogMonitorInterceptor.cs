using System;
using Castle.DynamicProxy;
using CES.CoreApi.Security.Interfaces;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Models;
using CES.CoreApi.Foundation.Contract.Interfaces;

namespace CES.CoreApi.SimpleInjectorProxy
{
	public class SecurityLogMonitorInterceptor : IInterceptor
	{
		private readonly ILogMonitorFactory _logMonitorFactory;
		private readonly IIdentityManager _identityManager;
	

		public SecurityLogMonitorInterceptor(ILogMonitorFactory logMonitorFactory, IIdentityManager identityManager)
		{
			if (logMonitorFactory == null)
				throw new ArgumentNullException("logMonitorFactory");
			if (identityManager == null) throw new ArgumentNullException("identityManager");
			_logMonitorFactory = logMonitorFactory;
			_identityManager = identityManager;
			
		}

		public void Intercept(IInvocation invocation)
		{
			invocation.Proceed();
			
			var clientApplicationIdentity = _identityManager.GetClientApplicationIdentity();
			
			
			var securityAuditParameters = new SecurityAuditParameters
			{
				ClientApplicationId = clientApplicationIdentity.ApplicationId,
				Operation = clientApplicationIdentity.OperationName,
				ServiceApplicationId = 8000//TODO Get using simple app config  manager 
			};

			var securityLogMonitor = _logMonitorFactory.CreateNew<ISecurityLogMonitor>();
			securityLogMonitor.DataContainer.ApplicationContext = clientApplicationIdentity;
			securityLogMonitor.LogSuccess(securityAuditParameters);
		}
	}
}
