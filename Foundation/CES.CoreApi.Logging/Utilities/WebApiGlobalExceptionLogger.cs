using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Logging.Utilities
{
    public class WebApiGlobalExceptionLogger : IExceptionLogger
    {
        #region Core

        private readonly ILogMonitorFactory _logMonitorFactory;
        private readonly IWebApiCallInformationProvider _webApiCallInformationProvider;

        public WebApiGlobalExceptionLogger(ILogMonitorFactory logMonitorFactory, IWebApiCallInformationProvider webApiCallInformationProvider)
        {
            if (logMonitorFactory == null) throw new ArgumentNullException("logMonitorFactory");
            if (webApiCallInformationProvider == null) throw new ArgumentNullException("webApiCallInformationProvider");
            _logMonitorFactory = logMonitorFactory;
            _webApiCallInformationProvider = webApiCallInformationProvider;
        }

        #endregion

        #region public methods

        public virtual Task LogAsync(ExceptionLoggerContext context,
                                     CancellationToken cancellationToken)
        {
            return !ShouldLog(context)
                ? Task.FromResult(0)
                : LogAsyncCore(context);
        }
        
        #endregion

        #region Private methods

        private Task LogAsyncCore(ExceptionLoggerContext context)
        {
            LogCore(context);
            return Task.FromResult(0);
        }

        private void LogCore(ExceptionLoggerContext context)
        {
            var exceptionMonitor = _logMonitorFactory.CreateNew<IExceptionLogMonitor>();

            _webApiCallInformationProvider.AddDetails(exceptionMonitor.DataContainer, context);
            
            exceptionMonitor.Publish(context.Exception);
        }

        private static bool ShouldLog(ExceptionLoggerContext context)
        {
            return true;
        }

        #endregion
    }
}
