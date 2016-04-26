using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Enumerations;
namespace CES.CoreApi.Logging.Monitors
{
	public abstract class BaseLogMonitor
	{
		private readonly ILoggerProxy _logProxy;

		protected BaseLogMonitor(ILoggerProxy logProxy, ILogConfigurationProvider configuration)
		{
			if (logProxy == null) throw new ArgumentNullException("logProxy");
			if (configuration == null) throw new ArgumentNullException("configuration");
			_logProxy = logProxy;
			Configuration = configuration;
		}

		protected ILogConfigurationProvider Configuration { get; }

		protected void Publish(IDataContainer dataContainer)
		{
			if (!IsLogEnabled(dataContainer.LogType))
				return;

			//Calling application should not fail if logging failed
			try
			{
				PublishDataContainer(dataContainer);
			}
			// ReSharper disable EmptyGeneralCatchClause
			catch (Exception)
			// ReSharper restore EmptyGeneralCatchClause
			{
			}
		}

		private void PublishDataContainer(IDataContainer dataContainer)
		{
			if (Debugger.IsAttached || !IsLogAsynchronous(dataContainer.LogType))
				PublishLogEntry(dataContainer);
			else
				Task.Factory.StartNew(() => PublishLogEntry(dataContainer)); //Save log entry asynchronously
		}

		private void PublishLogEntry(IDataContainer dataContainer)
		{
			switch (dataContainer.LogType)
			{
				case LogType.TraceLog:
				case LogType.SecurityAudit:
					_logProxy.PublishInformation(dataContainer);
					break;
				case LogType.PerformanceLog:
					_logProxy.PublishWarning(dataContainer);
					break;
				case LogType.ExceptionLog:
					_logProxy.PublishError(dataContainer);
					break;
				case LogType.DbPerformanceLog:
					_logProxy.PublishNotice(dataContainer);
					break;
				case LogType.EventLog:
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(dataContainer.LogType), dataContainer.LogType, null);
			}
		}

		private bool IsLogEnabled(LogType logType)
		{
			switch (logType)
			{
				case LogType.DbPerformanceLog:
					return Configuration.DatabasePerformanceLogConfiguration.IsEnabled;

				case LogType.TraceLog:
					return Configuration.TraceLogConfiguration.IsEnabled;

				case LogType.PerformanceLog:
					return Configuration.PerformanceLogConfiguration.IsEnabled;

				case LogType.ExceptionLog:
					return true;

				case LogType.SecurityAudit:
					return true;
				default:
					throw new ArgumentOutOfRangeException(nameof(logType), logType, null);
			}
		}

		private bool IsLogAsynchronous(LogType logType)
		{
			switch (logType)
			{
				case LogType.DbPerformanceLog:
					return Configuration.DatabasePerformanceLogConfiguration.IsAsynchronous;

				case LogType.TraceLog:
					return Configuration.TraceLogConfiguration.IsAsynchronous;

				case LogType.PerformanceLog:
					return Configuration.PerformanceLogConfiguration.IsAsynchronous;
				case LogType.EventLog:
					break;
				case LogType.ExceptionLog:
					break;
				case LogType.SecurityAudit:
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(logType), logType, null);
			}
			return false;
		}
	}
}
