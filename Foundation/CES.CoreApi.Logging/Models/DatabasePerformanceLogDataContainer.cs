using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;
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
		#region Core

		private readonly IJsonDataContainerFormatter _formatter;
		private readonly ISqlQueryFormatter _sqlQueryFormatter;

		public DatabasePerformanceLogDataContainer(ICurrentDateTimeProvider currentDateTimeProvider,
			IJsonDataContainerFormatter formatter,
			ISqlQueryFormatter sqlQueryFormatter)
		{
			if (currentDateTimeProvider == null)
				throw new ArgumentNullException("currentDateTimeProvider");
			if (formatter == null)
				throw new ArgumentNullException("formatter");
			if (sqlQueryFormatter == null)
				throw new ArgumentNullException("sqlQueryFormatter");

			_formatter = formatter;
			_sqlQueryFormatter = sqlQueryFormatter;
			StartTime = currentDateTimeProvider.GetCurrentUtc();
			ThreadId = Thread.CurrentThread.ManagedThreadId;
		}

		#endregion

		#region Public properties

		public DateTime StartTime { get; private set; }

		public long ElapsedMilliseconds { get; set; }

		public string CommandText { get; set; }


		public DatabaseConnection Connection { get; set; }

		[JsonConverter(typeof (StringEnumConverter))]
		public CommandType CommandType { get; set; }

		/// <summary>
		/// Gets or sets database command timeout
		/// </summary>
		public int CommandTimeout { get; set; }


		public IEnumerable<DatabaseParameter> Parameters { get; set; }

		/// <summary>
		/// Gets or sets thred identifier
		/// </summary>
		public int ThreadId { get; private set; }

		/// <summary>
		/// Gets or sets application context information
		/// </summary>
		public dynamic ApplicationContext { get; set; }

		/// <summary>
		/// Gets SQL query formatted
		/// </summary>
		public string Query => _sqlQueryFormatter.Format(this);

		#endregion //Public properties

		#region Overriding

		/// <summary>
		/// Returns string representation of the log entry
		/// </summary>
		/// <returns>String representation of the log entry</returns>
		public override string ToString()
		{
			return _formatter.Format(this);
		}

		/// <summary>
		/// Gets log type
		/// </summary>
		[JsonConverter(typeof (StringEnumConverter))]
		public LogType LogType
		{
			get { return LogType.DbPerformanceLog; }
		}

		#endregion //Overriding
	}
}