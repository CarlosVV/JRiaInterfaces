using System;
using Castle.DynamicProxy;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.SimpleInjectorProxy
{
    public class PerformanceInterceptor : IInterceptor
    {
        private readonly ILogMonitorFactory _logMonitorFactory;

        public PerformanceInterceptor(ILogMonitorFactory logMonitorFactory)
        {
            if (logMonitorFactory == null) 
                throw new ArgumentNullException("logMonitorFactory");
            _logMonitorFactory = logMonitorFactory;
        }

        public void Intercept(IInvocation invocation)
        {
            var performanceMonitor = _logMonitorFactory.CreateNew<IPerformanceLogMonitor>();
            performanceMonitor.Start(invocation.Method);

            // Calls the decorated instance.
            invocation.Proceed();

            performanceMonitor.DataContainer.Arguments = invocation.Arguments;
            performanceMonitor.DataContainer.ReturnValue = invocation.ReturnValue;
            performanceMonitor.Stop();
        }
    }
}
