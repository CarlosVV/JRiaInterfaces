using System;
using Castle.DynamicProxy;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Foundation.Contract.Interfaces;

namespace CES.CoreApi.SimpleInjectorProxy
{
    public class PerformanceInterceptor : IInterceptor
    {
		private readonly IPerformanceLogMonitor _performanceLogMonitor;
        private readonly IIdentityProvider _identityProvider;

		public PerformanceInterceptor(IPerformanceLogMonitor performanceLogMonitor, IIdentityProvider identityProvider)
        {
			if (performanceLogMonitor == null)
				throw new ArgumentNullException("performanceLogMonitor");
			if (identityProvider == null)
				throw new ArgumentNullException("identityManager");

			_performanceLogMonitor = performanceLogMonitor;
            _identityProvider = identityProvider;
        }

        public void Intercept(IInvocation invocation)
        {
			_performanceLogMonitor.DataContainer.ApplicationContext = _identityProvider.GetClientApplicationIdentity();
			_performanceLogMonitor.Start(invocation.Method);

            invocation.Proceed();

			_performanceLogMonitor.DataContainer.Arguments = invocation.Arguments;
			_performanceLogMonitor.DataContainer.ReturnValue = invocation.ReturnValue;
			_performanceLogMonitor.Stop();
        }
    }
}
