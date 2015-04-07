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
    [DataContract]
    public class DatabasePerformanceLogDataContainer : IDataContainer
    {
        #region Core

        private readonly IJsonDataContainerFormatter _formatter;
        private readonly ISqlQueryFormatter _sqlQueryFormatter;

        public DatabasePerformanceLogDataContainer(ICurrentDateTimeProvider currentDateTimeProvider, IJsonDataContainerFormatter formatter, 
            ISqlQueryFormatter sqlQueryFormatter)
        {
            if (currentDateTimeProvider == null) throw new ArgumentNullException("currentDateTimeProvider");
            if (formatter == null) throw new ArgumentNullException("formatter");
            if (sqlQueryFormatter == null) throw new ArgumentNullException("sqlQueryFormatter");

            _formatter = formatter;
            _sqlQueryFormatter = sqlQueryFormatter;
            StartTime = currentDateTimeProvider.GetCurrentUtc();
            ThreadId = Thread.CurrentThread.ManagedThreadId;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Returns time when method was executed
        /// </summary>
        [DataMember(Name = "timestamp")]
        public DateTime StartTime
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns elapsed time in milliseconds
        /// </summary>
        [DataMember]
        [DefaultValue(-1)]
        public long ElapsedMilliseconds
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets database command text
        /// </summary>
        [DataMember]
        public string CommandText
        {
            get;
            set;
        }

        [DataMember]
        public DatabaseConnection Connection { get; set; }

        /// <summary>
        /// Gets or sets database command type
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(StringEnumConverter))]
        public CommandType CommandType
        {
            get;
            set;
        }
       
        /// <summary>
        /// Gets or sets database command timeout
        /// </summary>
        [DataMember]
        public int CommandTimeout
        {
            get;
            set;
        }

        [DataMember]
        public IEnumerable<DatabaseParameter> Parameters { get; set; }

        /// <summary>
        /// Gets or sets thred identifier
        /// </summary>
        [DataMember]
        public int ThreadId
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets correlation information instance
        /// </summary>
        [DataMember]
        public ApplicationContext ApplicationContext
        {
            get;
            set;
        }

        /// <summary>
        /// Gets SQL query formatted
        /// </summary>
        [DataMember]
        public string Query
        {
            get { return _sqlQueryFormatter.Format(this); }
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
        [DataMember]
        [JsonConverter(typeof(StringEnumConverter))]
        public LogType LogType
        {
            get { return LogType.DbPerformanceLog; }
        }

        #endregion //Overriding
    }
}
