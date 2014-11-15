using System;
using System.Globalization;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Formatters
{
    public class ExceptionLogFormatter : IExceptionLogFormatter
    {
        private readonly IExceptionFormatter _exceptionFormatter;
        private readonly IExceptionLogItemGroupListFormatter _exceptionLogItemGroupListFormatter;

        #region Core

        internal const string WholeException = @"
{0}
{1}
--------------------------------------------------------------------------------";

        /// <summary>
        /// Initializes ExceptionLogFormatter instance
        /// </summary>
        /// <param name="exceptionFormatter">Exception formatter instance</param>
        /// <param name="exceptionLogItemGroupListFormatter">Log group list formatter</param>
        public ExceptionLogFormatter(IExceptionFormatter exceptionFormatter, IExceptionLogItemGroupListFormatter exceptionLogItemGroupListFormatter)
        {
            if (exceptionFormatter == null) 
                throw new ArgumentNullException("exceptionFormatter");
            if (exceptionLogItemGroupListFormatter == null)
                throw new ArgumentNullException("exceptionLogItemGroupListFormatter");

            _exceptionFormatter = exceptionFormatter;
            _exceptionLogItemGroupListFormatter = exceptionLogItemGroupListFormatter;
        }

        #endregion //Core

        #region Public methods

        /// <summary>
        /// Gets log entry formatted
        /// </summary>
        /// <param name="dataContainer">Log entry data</param>
        /// <returns></returns>
        public string Format(ExceptionLogDataContainer dataContainer)
        {
            if (dataContainer == null)
                throw new ArgumentNullException("dataContainer");

            return string.Format(CultureInfo.InvariantCulture,
                                 WholeException,
                                 _exceptionFormatter.Format(dataContainer.Exception, dataContainer.Timestamp),
                                 _exceptionLogItemGroupListFormatter.Format(dataContainer.Items));
        }

        #endregion //Public methods
    }
}