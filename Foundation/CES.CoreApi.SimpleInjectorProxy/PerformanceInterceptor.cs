using System;
using Castle.DynamicProxy;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Security.Interfaces;

namespace CES.CoreApi.SimpleInjectorProxy
{
	public class PerformanceInterceptor : IInterceptor
	{
		private readonly IPerformanceLogMonitor _performanceLogMonitor;
		private readonly IIdentityManager _identityManager;

		public PerformanceInterceptor(IPerformanceLogMonitor performanceLogMonitor, IIdentityManager identityManager)
		{
			if (performanceLogMonitor == null)
				throw new ArgumentNullException("performanceLogMonitor");
			if (identityManager == null)
				throw new ArgumentNullException("identityManager");

			_performanceLogMonitor = performanceLogMonitor;
			_identityManager = identityManager;
		}

		public void Intercept(IInvocation invocation)
		{
			_performanceLogMonitor.DataContainer.ApplicationContext = _identityManager.GetClientApplicationIdentity();
			_performanceLogMonitor.Start(invocation.Method);

			// Calls the decorated instance.
			invocation.Proceed();

			_performanceLogMonitor.DataContainer.Arguments = invocation.Arguments;
			_performanceLogMonitor.DataContainer.ReturnValue = invocation.ReturnValue;
			_performanceLogMonitor.Stop();
		}
	}
}
