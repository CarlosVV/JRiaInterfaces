using System;
using Castle.DynamicProxy;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.SimpleInjectorProxy
{
    public class ExceptionInterceptor : IInterceptor
    {
        private readonly ILogMonitorFactory _logMonitorFactory;

        public ExceptionInterceptor(ILogMonitorFactory logMonitorFactory)
        {
            if (logMonitorFactory == null) 
                throw new ArgumentNullException("logMonitorFactory");
            _logMonitorFactory = logMonitorFactory;
        }

        public void Intercept(IInvocation invocation)
        {
            try
            {
                // Calls the decorated instance.
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                var exceptionLogMonitor = _logMonitorFactory.CreateNew<IExceptionLogMonitor>();    
                exceptionLogMonitor.Publish(ex);
                throw;
            }
        }
    }
}
