using System;
using Castle.DynamicProxy;
using CES.CoreApi.Common.Interfaces;
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
		private readonly IServiceCallHeaderParametersProvider _parametersProvider;

		public SecurityLogMonitorInterceptor(ILogMonitorFactory logMonitorFactory, IIdentityManager identityManager, IHostApplicationProvider hostApplicationProvider, IServiceCallHeaderParametersProvider parametersProvider)
		{
			if (logMonitorFactory == null)
				throw new ArgumentNullException("logMonitorFactory");
			if (identityManager == null) throw new ArgumentNullException("identityManager");
			_logMonitorFactory = logMonitorFactory;
			_identityManager = identityManager;
			_hostApplicationProvider = hostApplicationProvider;
			_parametersProvider = parametersProvider;
		}

		public void Intercept(IInvocation invocation)
		{
			invocation.Proceed();
			
			var clientApplicationIdentity = _identityManager.GetClientApplicationIdentity();
			var hostApplication = _hostApplicationProvider.GetApplication().Result;
			
			var securityAuditParameters = new SecurityAuditParameters
			{
				ClientApplicationId = clientApplicationIdentity.ApplicationId,
				Operation = _parametersProvider.GetParameters().OperationName,
				ServiceApplicationId = hostApplication.Id
			};

			var securityLogMonitor = _logMonitorFactory.CreateNew<ISecurityLogMonitor>();
			securityLogMonitor.DataContainer.ApplicationContext = clientApplicationIdentity;
			securityLogMonitor.LogSuccess(securityAuditParameters);
		}
	}
}
