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
    [JsonObject(MemberSerialization.OptIn)]
    public class DatabasePerformanceLogDataContainer : IDataContainer
    {
        #region Core

        private readonly IJsonDataContainerFormatter _formatter;

        public DatabasePerformanceLogDataContainer(ICurrentDateTimeProvider currentDateTimeProvider, IJsonDataContainerFormatter formatter)
        {
            if (currentDateTimeProvider == null) throw new ArgumentNullException("currentDateTimeProvider");
            if (formatter == null) throw new ArgumentNullException("formatter");

            _formatter = formatter;
            StartTime = currentDateTimeProvider.GetCurrentLocal();
            ThreadId = Thread.CurrentThread.ManagedThreadId;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Returns time when method was executed
        /// </summary>
        [JsonProperty]
        public DateTime StartTime
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns elapsed time in milliseconds
        /// </summary>
        [JsonProperty]
        public long ElapsedMilliseconds
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets database command text
        /// </summary>
        [JsonProperty]
        public string CommandText
        {
            get;
            set;
        }

        [JsonProperty]
        public DatabaseConnection Connection { get; set; }

        /// <summary>
        /// Gets or sets database command type
        /// </summary>
        [JsonProperty]
        [JsonConverter(typeof(StringEnumConverter))]
        public CommandType CommandType
        {
            get;
            set;
        }
       
        /// <summary>
        /// Gets or sets database command timeout
        /// </summary>
        [JsonProperty]
        public int CommandTimeout
        {
            get;
            set;
        }

        [JsonProperty]
        public IEnumerable<DatabaseParameter> Parameters { get; set; }

        /// <summary>
        /// Gets or sets thred identifier
        /// </summary>
        [JsonProperty]
        public int ThreadId
        {
            get;
            private set;
        }

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
        [JsonProperty]
        [JsonConverter(typeof(StringEnumConverter))]
        public LogType LogType
        {
            get { return LogType.DbPerformanceLog; }
        }

        #endregion //Overriding
    }
}
