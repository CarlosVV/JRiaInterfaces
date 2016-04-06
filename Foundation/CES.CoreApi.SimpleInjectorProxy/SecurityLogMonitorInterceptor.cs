using System;
using Castle.DynamicProxy;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Models;

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
		}
	}
}
