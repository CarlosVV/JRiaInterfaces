using System;
using System.Collections;
using System.Threading;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Logging.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CES.CoreApi.Logging.Models
{
	public class PerformanceLogDataContainer : IDataContainer
	{
		private readonly IJsonDataContainerFormatter _formatter;

		public PerformanceLogDataContainer(IJsonDataContainerFormatter formatter, ICurrentDateTimeProvider currentDateTimeProvider)
		{
			if (formatter == null)
				throw new ArgumentNullException("formatter");
			if (currentDateTimeProvider == null)
				throw new ArgumentNullException("currentDateTimeProvider");

			_formatter = formatter;
			StartTime = currentDateTimeProvider.GetCurrentUtc();
			ThreadId = Thread.CurrentThread.ManagedThreadId;
		}

		public DateTime StartTime
		{
			get;
			private set;
		}

		public long ElapsedMilliseconds
		{
			get;
			set;
		}

		public int ThreadId
		{
			get;
			private set;
		}

		public string MethodName
		{
			get;
			set;
		}

		public Type[] GenericArguments
		{
			get;
			set;
		}

		public IEnumerable Arguments
		{
			get;
			set;
		}

		public object ReturnValue
		{
			get;
			set;
		}

		public dynamic ApplicationContext
		{
			get;
			set;
		}

		public override string ToString()
		{
			return _formatter.Format(this);
		}

		[JsonConverter(typeof(StringEnumConverter))]
		public LogType LogType => LogType.PerformanceLog;
	}
}
