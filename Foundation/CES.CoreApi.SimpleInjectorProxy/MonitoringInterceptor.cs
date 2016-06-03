using CES.CoreApi.Logging.Interfaces;
using System;
using System.Diagnostics;

namespace CES.CoreApi.SimpleInjectorProxy
{
	public class MonitoringInterceptor : IInterceptor
	{
		private readonly IExceptionLogMonitor logger;

		public MonitoringInterceptor(IExceptionLogMonitor logger)
		{
			this.logger = logger;
		}

		public void Intercept(IInvocation invocation)
		{
			var watch = Stopwatch.StartNew();

			// Calls the decorated instance.
			invocation.Proceed();

			var decoratedType = invocation.InvocationTarget.GetType();

			this.logger.Publish(new Exception("ex test"), "Test12321");
		}
	}
}
