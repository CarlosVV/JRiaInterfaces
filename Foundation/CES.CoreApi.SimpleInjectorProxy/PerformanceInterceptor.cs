using System;
using Castle.DynamicProxy;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.SimpleInjectorProxy
{
    public class PerformanceInterceptor : IInterceptor
    {
        private readonly ILogMonitorFactory _logMonitorFactory;
        private readonly IIdentityManager _identityManager;

        public PerformanceInterceptor(ILogMonitorFactory logMonitorFactory, IIdentityManager identityManager)
        {
            if (logMonitorFactory == null)
                throw new ArgumentNullException("logMonitorFactory");
            if (identityManager == null) throw new ArgumentNullException("identityManager");
            _logMonitorFactory = logMonitorFactory;
            _identityManager = identityManager;
        }

        public void Intercept(IInvocation invocation)
        {
            var performanceMonitor = _logMonitorFactory.CreateNew<IPerformanceLogMonitor>();
            performanceMonitor.DataContainer.ApplicationContext = _identityManager.GetClientApplicationIdentity();
            performanceMonitor.Start(invocation.Method);

            // Calls the decorated instance.
            invocation.Proceed();

            performanceMonitor.DataContainer.Arguments = invocation.Arguments;
            performanceMonitor.DataContainer.ReturnValue = invocation.ReturnValue;
            performanceMonitor.Stop();
        }
    }
}
