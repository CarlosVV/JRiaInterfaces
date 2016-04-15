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
		private readonly IHostApplicationProvider _hostApplicationProvider;

		public SecurityLogMonitorInterceptor(ILogMonitorFactory logMonitorFactory, IIdentityManager identityManager, IHostApplicationProvider hostApplicationProvider)
		{
			if (logMonitorFactory == null)
				throw new ArgumentNullException("logMonitorFactory");
			if (identityManager == null) throw new ArgumentNullException("identityManager");
			_logMonitorFactory = logMonitorFactory;
			_identityManager = identityManager;
			_hostApplicationProvider = hostApplicationProvider;
		}

		public void Intercept(IInvocation invocation)
		{
			invocation.Proceed();
			
			var clientApplicationIdentity = _identityManager.GetClientApplicationIdentity();
			var hostApplication = _hostApplicationProvider.GetApplication().Result;
			
			var securityAuditParameters = new SecurityAuditParameters
			{
				ClientApplicationId = clientApplicationIdentity.ApplicationId,
				Operation = clientApplicationIdentity.OperationName,
				ServiceApplicationId = hostApplication.Id
			};

			var securityLogMonitor = _logMonitorFactory.CreateNew<ISecurityLogMonitor>();
			securityLogMonitor.DataContainer.ApplicationContext = clientApplicationIdentity;
			securityLogMonitor.LogSuccess(securityAuditParameters);
		}
	}
}
