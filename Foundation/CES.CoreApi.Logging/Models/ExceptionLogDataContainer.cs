using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Logging.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CES.CoreApi.Logging.Models
{
	public class ExceptionLogDataContainer : IDataContainer
	{
		private readonly IJsonDataContainerFormatter _formatter;

		public ExceptionLogDataContainer(IJsonDataContainerFormatter formatter, ICurrentDateTimeProvider currentDateTimeProvider)
		{
			if (formatter == null)
				throw new ArgumentNullException("formatter");
			if (currentDateTimeProvider == null)
				throw new ArgumentNullException("currentDateTimeProvider");

			_formatter = formatter;
			Items = new Collection<ExceptionLogItemGroup>();
			Timestamp = currentDateTimeProvider.GetCurrentUtc();
			ThreadId = Thread.CurrentThread.ManagedThreadId;
		}

		[JsonProperty(PropertyName = "Timestamp")]
		public DateTime Timestamp { get; private set; }

		public Collection<ExceptionLogItemGroup> Items { get; private set; }

		public CoreApiException Exception { get; private set; }

		public int ThreadId { get; private set; }

		public string Message => Exception == null ? string.Empty : GetExceptionMessage();

		public string CustomMessage { get; set; }

		public dynamic ApplicationContext { get; set; }

		public ExceptionLogItemGroup GetGroupByTitle(string groupTitle)
		{
			if (string.IsNullOrEmpty(groupTitle))
				throw new ArgumentNullException("groupTitle");

			var group = Items.FirstOrDefault(p => p.Title.Equals(groupTitle, StringComparison.OrdinalIgnoreCase));
			if (group == null)
			{
				group = new ExceptionLogItemGroup { Title = groupTitle };
				Items.Add(group);
			}
			return group;
		}

		public void SetException(Exception exception)
		{
			if (exception == null)
				throw new ArgumentNullException("exception");

			var coreApiException = exception as CoreApiException ?? new CoreApiException(exception);
			Exception = coreApiException;
		}

		public override string ToString()
		{
			return _formatter.Format(this);
		}

		[JsonConverter(typeof(StringEnumConverter))]
		public LogType LogType => LogType.ExceptionLog;

		private string GetExceptionMessage()
		{
			var message = Exception.Message;
			var innerException = Exception.InnerException;

			while (innerException != null)
			{
				message = innerException.Message;
				innerException = innerException.InnerException;
			}

			return message;
		}
	}
}