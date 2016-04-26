using System;
using System.Threading;
using System.Threading.Tasks;
using CES.CoreApi.Logging.Interfaces;
using System.Web.Http.ExceptionHandling;

namespace CES.CoreApi.Logging.WebApi
{
	public class WebApiGlobalExceptionLogger : IExceptionLogger
	{
		private readonly IExceptionLogMonitor _exceptionLogMonitor;
		private readonly IWebApiCallInformationProvider _webApiCallInformationProvider;

		public WebApiGlobalExceptionLogger(IExceptionLogMonitor exceptionLogMonitor, IWebApiCallInformationProvider webApiCallInformationProvider)
		{
			if (exceptionLogMonitor == null)
				throw new ArgumentNullException("logMonitorFactory");
			if (webApiCallInformationProvider == null)
				throw new ArgumentNullException("webApiCallInformationProvider");
			_exceptionLogMonitor = exceptionLogMonitor;
			_webApiCallInformationProvider = webApiCallInformationProvider;
		}

		public virtual Task LogAsync(ExceptionLoggerContext context,
									 CancellationToken cancellationToken)
		{
			return !ShouldLog(context)
				? Task.FromResult(0)
				: LogAsyncCore(context);
		}

		private Task LogAsyncCore(ExceptionLoggerContext context)
		{
			LogCore(context);
			return Task.FromResult(0);
		}

		private void LogCore(ExceptionLoggerContext context)
		{
			_webApiCallInformationProvider.AddDetails(_exceptionLogMonitor.DataContainer, context);

			_exceptionLogMonitor.Publish(context.Exception);
		}

		private static bool ShouldLog(ExceptionLoggerContext context)
		{
			return true;
		}
	}
}
