using System;
using System.Globalization;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Models;
using CES.CoreApi.Logging.Utilities;

namespace CES.CoreApi.Logging.Formatters
{
    public class PerformanceLogFormatter : IPerformanceLogFormatter
    {
        #region Core

        private readonly IExecutionTimeFormatter _executionTimeFormatter;
        private readonly IDateTimeFormatter _dateTimeFormatter;
        private readonly IThreadIdFormatter _threadIdFormatter;
        private readonly IGenericArgumentListFormatter _genericArgumentListFormatter;
        private readonly IDefaultValueFormatter _defaultValueFormatter;
        private const string LogEntry = "{0}  [{1}]  {2}   {3}{4}({5}) -> [{6}]";

        /// <summary>
        /// Initializes PerformanceLogFormatter instance
        /// </summary>
        /// <param name="executionTimeFormatter">Excecution time formatter</param>
        /// <param name="dateTimeFormatter">Date time formatter</param>
        /// <param name="threadIdFormatter">Thread ID formatter</param>
        /// <param name="genericArgumentListFormatter">Generic argument list formatter</param>
        /// <param name="defaultValueFormatter">Default value formatter instance</param>
        public PerformanceLogFormatter(IExecutionTimeFormatter executionTimeFormatter, IDateTimeFormatter dateTimeFormatter,
            IThreadIdFormatter threadIdFormatter, IGenericArgumentListFormatter genericArgumentListFormatter,
            IDefaultValueFormatter defaultValueFormatter)
        {
            if (executionTimeFormatter == null) throw new ArgumentNullException("executionTimeFormatter");
            if (dateTimeFormatter == null) throw new ArgumentNullException("dateTimeFormatter");
            if (threadIdFormatter == null) throw new ArgumentNullException("threadIdFormatter");
            if (genericArgumentListFormatter == null) throw new ArgumentNullException("genericArgumentListFormatter");
            if (defaultValueFormatter == null) throw new ArgumentNullException("defaultValueFormatter");
            _executionTimeFormatter = executionTimeFormatter;
            _dateTimeFormatter = dateTimeFormatter;
            _threadIdFormatter = threadIdFormatter;
            _genericArgumentListFormatter = genericArgumentListFormatter;
            _defaultValueFormatter = defaultValueFormatter;
        }

        #endregion //Core

        #region Public methods

        /// <summary>
        /// Gets log entry formatted
        /// </summary>
        /// <param name="dataContainer">Log entry data</param>
        /// <returns></returns>
        public string Format(PerformanceLogDataContainer dataContainer)
        {
            if (dataContainer == null)
                throw new ArgumentNullException("dataContainer");

            return string.Format(CultureInfo.InvariantCulture,
                                 LogEntry,
                                 _dateTimeFormatter.Format(dataContainer.StartTime),
                                 _threadIdFormatter.Format(dataContainer.ThreadId),
                                 _executionTimeFormatter.Format(dataContainer.ElapsedMilliseconds),
                                 dataContainer.MethodName,
                                 _genericArgumentListFormatter.Format(dataContainer.GenericArguments),
                                 dataContainer.Arguments.ToStringList(_defaultValueFormatter, true),
                                 _defaultValueFormatter.Format(dataContainer.ReturnValue));
        }

        #endregion //Public methods
    }
}
