using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Logging.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CES.CoreApi.Logging.Models
{
	public class DatabasePerformanceLogDataContainer : IDataContainer
	{
		private readonly IJsonDataContainerFormatter _formatter;
		private readonly ISqlQueryFormatter _sqlQueryFormatter;

		public DatabasePerformanceLogDataContainer(
			IJsonDataContainerFormatter formatter,
			ISqlQueryFormatter sqlQueryFormatter)
		{
			
			if (formatter == null)
				throw new ArgumentNullException("formatter");
			if (sqlQueryFormatter == null)
				throw new ArgumentNullException("sqlQueryFormatter");

			_formatter = formatter;
			_sqlQueryFormatter = sqlQueryFormatter;
			StartTime = DateTime.UtcNow;
			ThreadId = Thread.CurrentThread.ManagedThreadId;
		}

		[JsonProperty(PropertyName = "Timestamp")]
		public DateTime StartTime { get; private set; }

		public long ElapsedMilliseconds { get; set; }

		public string CommandText { get; set; }

		public DatabaseConnection Connection { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		public CommandType CommandType { get; set; }

		public int CommandTimeout { get; set; }

		public IEnumerable<DatabaseParameter> Parameters { get; set; }

		public int ThreadId { get; private set; }

		public dynamic ApplicationContext { get; set; }

		public string Query => _sqlQueryFormatter.Format(this);

		public override string ToString()
		{
			return _formatter.Format(this);
		}

		[JsonConverter(typeof(StringEnumConverter))]
		public LogType LogType => LogType.DbPerformanceLog;
	}
}