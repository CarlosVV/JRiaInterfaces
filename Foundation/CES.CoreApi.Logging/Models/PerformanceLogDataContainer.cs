using System;
using System.Collections;
using System.Threading;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Logging.Models
{
    public class PerformanceLogDataContainer : IDataContainer
    {
        #region Core

        private readonly IPerformanceLogFormatter _performanceLogFormatter;

        public PerformanceLogDataContainer(IPerformanceLogFormatter performanceLogFormatter, ICurrentDateTimeProvider currentDateTimeProvider)
        {
            if (performanceLogFormatter == null) throw new ArgumentNullException("performanceLogFormatter");
            if (currentDateTimeProvider == null) throw new ArgumentNullException("currentDateTimeProvider");

            _performanceLogFormatter = performanceLogFormatter;
            StartTime = currentDateTimeProvider.GetCurrentLocal();
            ThreadId = Thread.CurrentThread.ManagedThreadId;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Returns time when method was executed
        /// </summary>
        public DateTime StartTime
        {
            get;
            private set;
        }

        /// <summary>
        /// Returns elapsed time in milliseconds
        /// </summary>
        public long ElapsedMilliseconds
        {
            get; 
            set;
        }

        /// <summary>
        /// Gets or sets thred identifier
        /// </summary>
        public int ThreadId
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets executed method name
        /// </summary>
        public string MethodName
        {
            get; 
            set;
        }

        /// <summary>
        /// Gets or sets generic argument list
        /// </summary>
        public Type[] GenericArguments
        {
            get; 
            set;
        }

        /// <summary>
        /// Gets or sets list of method parameters
        /// </summary>
        public IEnumerable Arguments
        {
            get; 
            set;
        }

        /// <summary>
        /// Gets or sets method return value
        /// </summary>
        public object ReturnValue
        {
            get; 
            set;
        }

        #endregion //Public properties

        #region Overriding

        /// <summary>
        /// Returns string representation of the log entry
        /// </summary>
        /// <returns>String representation of the log entry</returns>
        public override string ToString()
        {
            return _performanceLogFormatter.Format(this);
        }

        /// <summary>
        /// Gets log type
        /// </summary>
        public LogType LogType
        {
            get { return LogType.PerformanceLog; }
        }

        #endregion //Overriding
    }
}
