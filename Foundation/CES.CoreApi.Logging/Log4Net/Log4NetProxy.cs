using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Logging.Interfaces;
using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Core;

namespace CES.CoreApi.Logging.Log4Net
{
	public class Log4NetProxy : ILoggerProxy
	{
		private static readonly ILog Log;

		static Log4NetProxy()
		{
			XmlConfigurator.Configure();
			Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

			IsDebugEnabled = Log.IsDebugEnabled;
			IsErrorEnabled = Log.IsErrorEnabled;
			IsFatalEnabled = Log.IsFatalEnabled;
			IsInformationEnabled = Log.IsInfoEnabled;
			IsWarningEnabled = Log.IsWarnEnabled;
			IsNoticeEnabled = Log.Logger.IsEnabledFor(Level.Notice);
		}

		public void SetParameter(string parameterName, object parameterValue)
		{
			GlobalContext.Properties[parameterName] = parameterValue;
		}

		public void RemoveParameter(string parameterName)
		{
			GlobalContext.Properties.Remove(parameterName);
		}

		public void ClearParameters()
		{
			GlobalContext.Properties.Clear();
		}

		public void Publish(LogEntryType entryType, string message, Exception exception = null)
		{
			switch (entryType)
			{
				case LogEntryType.Error:
					if (IsErrorEnabled)
					{
						if (!IsErrorAppenderEnabled)
							MakeSureAppenderEnabled(entryType);
						Log.Error(message, exception);
					}
					break;

				case LogEntryType.Fatal:
					if (IsFatalEnabled)
					{
						if (!IsFatalAppenderEnabled)
							MakeSureAppenderEnabled(entryType);
						Log.Fatal(message, exception);
					}
					break;

				case LogEntryType.Information:
					if (IsInformationEnabled)
					{
						if (!IsInformationAppenderEnabled)
							MakeSureAppenderEnabled(entryType);
						Log.Info(message, exception);
					}
					break;

				case LogEntryType.Warning:
					if (IsWarningEnabled)
					{
						if (!IsWarningAppenderEnabled)
							MakeSureAppenderEnabled(entryType);
						Log.Warn(message, exception);
					}
					break;

				case LogEntryType.Debug:
					if (IsDebugEnabled)
					{
						if (!IsDebugAppenderEnabled)
							MakeSureAppenderEnabled(entryType);
						Log.Debug(message, exception);
					}
					break;

				case LogEntryType.Notice:
					if (IsNoticeEnabled)
					{
						if (!IsNoticeAppenderEnabled)
							MakeSureAppenderEnabled(entryType);
						Log.Logger.Log(MethodBase.GetCurrentMethod().DeclaringType, Level.Notice, message,
									   exception);
					}
					break;

				default:
					if (IsInformationEnabled)
					{
						if (!IsInformationAppenderEnabled)
							MakeSureAppenderEnabled(entryType);
						Log.Info(message, exception);
					}
					break;
			}
		}

		public void PublishDebug(string message)
		{
			Publish(LogEntryType.Debug, message);
		}

		public void PublishDebug(string message, params object[] parameters)
		{
			var formattedMessage = string.Format(CultureInfo.InvariantCulture, message, parameters);
			Publish(LogEntryType.Debug, formattedMessage);
		}

		public void PublishWarning(string message)
		{
			Publish(LogEntryType.Warning, message);
		}

		public void PublishWarning(IDataContainer dataContainer)
		{
			Publish(LogEntryType.Warning, dataContainer.ToString());
		}

		public void PublishInformation(string message)
		{
			Publish(LogEntryType.Information, message);
		}

		public void PublishInformation(IDataContainer dataContainer)
		{
			if (dataContainer.LogType != LogType.TraceLog &&
				dataContainer.LogType != LogType.EventLog &&
				dataContainer.LogType != LogType.SecurityAudit)
				return;
			Publish(LogEntryType.Information, dataContainer.ToString());
		}

		public void PublishInformation(string message, params object[] parameters)
		{
			var formattedMessage = string.Format(CultureInfo.InvariantCulture, message, parameters);
			Publish(LogEntryType.Information, formattedMessage);
		}

		public void PublishError(string message)
		{
			Publish(LogEntryType.Error, message);
		}

		public void PublishError(IDataContainer dataContainer)
		{
			if (dataContainer.LogType != LogType.ExceptionLog)
				return;
			Publish(LogEntryType.Error, dataContainer.ToString());
		}

		public void PublishError(string message, Exception exception)
		{
			Publish(LogEntryType.Error, message, exception);
		}

		public void PublishError(string message, params object[] parameters)
		{
			if (!IsErrorEnabled)
				return;
			var formattedMessage = string.Format(CultureInfo.InvariantCulture, message, parameters);
			Publish(LogEntryType.Error, formattedMessage);
		}

		public void PublishFatal(string message)
		{
			Publish(LogEntryType.Fatal, message);
		}

		public void PublishFatal(string message, params object[] parameters)
		{
			var formattedMessage = string.Format(CultureInfo.InvariantCulture, message, parameters);
			Publish(LogEntryType.Fatal, formattedMessage);
		}

		public void PublishFatal(string message, Exception exception)
		{
			Publish(LogEntryType.Fatal, message, exception);
		}

		public void PublishNotice(string message)
		{
			Publish(LogEntryType.Notice, message);
		}

		public void PublishNotice(IDataContainer dataContainer)
		{
			if (dataContainer.LogType != LogType.DbPerformanceLog)
				return;
			Publish(LogEntryType.Notice, dataContainer.ToString());
		}

		public static bool IsDebugEnabled
		{
			get;
			private set;
		}

		public static bool IsInformationEnabled
		{
			get;
			private set;
		}

		public static bool IsWarningEnabled
		{
			get;
			private set;
		}

		public static bool IsErrorEnabled
		{
			get;
			private set;
		}

		public static bool IsFatalEnabled
		{
			get;
			private set;
		}

		public static bool IsNoticeEnabled
		{
			get;
			private set;
		}

		private void MakeSureAppenderEnabled(LogEntryType entryType)
		{
			var appender = LogManager.GetRepository().GetAppenders()
				.OfType<FileAppender>()
				.FirstOrDefault(fileAppender => !string.IsNullOrEmpty(fileAppender.File) &&
												fileAppender.Name.StartsWith(entryType.ToString(),
																			 StringComparison.OrdinalIgnoreCase));

			//Found appender can be null - in this case we suppose it is not a FileAppender 
			//and set appropriate property to TRUE, just not to search it every time

			switch (entryType)
			{
				case LogEntryType.Debug:
					EnableAppender(appender, Level.Debug);
					IsDebugAppenderEnabled = true;
					break;

				case LogEntryType.Warning:
					EnableAppender(appender, Level.Warn);
					IsWarningAppenderEnabled = true;
					break;

				case LogEntryType.Error:
					EnableAppender(appender, Level.Error);
					IsErrorAppenderEnabled = true;
					break;

				case LogEntryType.Fatal:
					EnableAppender(appender, Level.Fatal);
					IsFatalAppenderEnabled = true;
					break;

				case LogEntryType.Information:
					EnableAppender(appender, Level.Info);
					IsInformationAppenderEnabled = true;
					break;

				case LogEntryType.Notice:
					EnableAppender(appender, Level.Notice);
					IsNoticeAppenderEnabled = true;
					break;
			}
		}

		private static void EnableAppender(AppenderSkeleton appender, Level threshold)
		{
			if (appender != null && appender.Threshold == Level.Off)
			{
				appender.Threshold = threshold;
			}
		}

		private bool IsErrorAppenderEnabled { get; set; }
		private bool IsFatalAppenderEnabled { get; set; }
		private bool IsInformationAppenderEnabled { get; set; }
		private bool IsWarningAppenderEnabled { get; set; }
		private bool IsDebugAppenderEnabled { get; set; }
		private bool IsNoticeAppenderEnabled { get; set; }

	}
}