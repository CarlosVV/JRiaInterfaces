using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using CES.CoreApi.Foundation.Contract.Exceptions;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Logging.Formatters
{
    public class ExceptionFormatter : IExceptionFormatter
    {
        #region Core

        private readonly IExceptionLogItemGroupTitleFormatter _exceptionLogItemGroupTitleFormatter;
        private readonly IDateTimeFormatter _dateTimeFormatter;

        private const string Item = "{0}				{1}";
        private const string StackTraceItemDelimiter = "\r\n";

        /// <summary>
        /// Initializes ExceptionFormatter instance
        /// </summary>
        /// <param name="exceptionLogItemGroupTitleFormatter">Log group title formatter instance</param>
        /// <param name="dateTimeFormatter">DateTime formatter instance </param>
        public ExceptionFormatter(IExceptionLogItemGroupTitleFormatter exceptionLogItemGroupTitleFormatter, IDateTimeFormatter dateTimeFormatter)
        {
            if (exceptionLogItemGroupTitleFormatter == null)
                throw new ArgumentNullException("exceptionLogItemGroupTitleFormatter");
            if (dateTimeFormatter == null) 
                throw new ArgumentNullException("dateTimeFormatter");

            _exceptionLogItemGroupTitleFormatter = exceptionLogItemGroupTitleFormatter;
            _dateTimeFormatter = dateTimeFormatter;
        }

        #endregion //Core

        #region Public methods

        /// <summary>
        /// Gets exception formatted as a string
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <param name="timestamp">Exception timestamp </param>
        /// <returns></returns>
        public string Format(Exception exception, DateTime timestamp)
        {
            if (exception == null)
                throw new ArgumentNullException("exception");

            var title = _exceptionLogItemGroupTitleFormatter.Format("EXCEPTION DETAILS");

            var builder = new StringBuilder();
            builder.Append(title);
            builder.AppendLine();
            builder.AppendLine();

            ProcessException(exception, timestamp, builder);

            return builder.ToString();
        }

        #endregion //Public methods

        #region Private methods

        /// <summary>
        /// Processes exception including inner exception, puts information to string builder
        /// </summary>
        /// <param name="exception">Exception instance</param>
        /// <param name="timestamp">Time when exception happened</param>
        /// <param name="builder">Exception log string builder instance</param>
        private void ProcessException(Exception exception, DateTime timestamp, StringBuilder builder)
        {
            var counter = 0;

            foreach (var exceptionDetails in ParseException(exception, timestamp))
            {
                if (counter > 0)
                {
                    builder.AppendLine(_exceptionLogItemGroupTitleFormatter.Format("INNER EXCEPTION DETAILS"));
                    builder.AppendLine();
                }

                ProcessExceptionDetails(exceptionDetails, builder);

                counter++;
            }
        }

        /// <summary>
        /// Processes exception details, puts information to string builder
        /// </summary>
        /// <param name="exceptionDetails">Exception details</param>
        /// <param name="builder">Exception log string builder instance</param>
        private static void ProcessExceptionDetails(KeyValuePair<int, Dictionary<string, object>> exceptionDetails, StringBuilder builder)
        {
            foreach (var item in exceptionDetails.Value)
            {
                builder.AppendFormat(CultureInfo.InvariantCulture,
                                     Item,
                                     item.Key.PadRight(17, ' '),
                                     item.Value);
                builder.AppendLine();
            }
        }

        /// <summary>
        /// Parses exception details including inner exceptions
        /// </summary>
        /// <param name="exception">Exception instance</param>
        /// <param name="timestamp">Exception timestamp</param>
        /// <returns>Full exception details</returns>
        private Dictionary<int, Dictionary<string, object>> ParseException(Exception exception, DateTime timestamp)
        {
            var exceptionDetails = new Dictionary<int, Dictionary<string, object>>();
            var counter = 1;

            exceptionDetails.Add(counter, GetExceptionDetails(exception, timestamp));

            var innerException = exception.InnerException;
            while (innerException != null)
            {
                counter++;
                exceptionDetails.Add(counter, GetExceptionDetails(innerException));
                innerException = innerException.InnerException;
            }

            return exceptionDetails;
        }

        /// <summary>
        /// Gets exception details
        /// </summary>
        /// <param name="exception">Exception instance</param>
        /// <param name="timestamp">Exception timestamp </param>
        /// <returns></returns>
        private Dictionary<string, object> GetExceptionDetails(Exception exception, DateTime timestamp = default(DateTime))
        {
            var exceptionDetails = new Dictionary<string, object>();
            if (timestamp != default(DateTime))
                exceptionDetails.Add("Timestamp", _dateTimeFormatter.Format(timestamp));

            var coreApiException = exception as CoreApiException;

            if (coreApiException != null)
            {
                exceptionDetails.Add("Error Code", coreApiException.ErrorCode);
                exceptionDetails.Add("Message", coreApiException.ClientMessage);
                exceptionDetails.Add("Error ID", coreApiException.ErrorId);
            }
            else
            {
                exceptionDetails.Add("Message", exception.Message);
            }

            AddStackTrace(exception, coreApiException, exceptionDetails);

            var source = !string.IsNullOrEmpty(exception.Source)
                ? exception.Source
                : coreApiException != null
                    ? coreApiException.Source
                    : string.Empty;
            exceptionDetails.Add("Source", source);

            var targetSite = exception.TargetSite ?? (coreApiException != null ? coreApiException.TargetSite : null);
            exceptionDetails.Add("TargetSite", targetSite);

            exceptionDetails.Add("ExceptionType", exception.GetType());
            exceptionDetails.Add("Data", exception.Data);
         
            GetExceptionData(exception.Data, exceptionDetails);

            return exceptionDetails;
        }

        private static void AddStackTrace(Exception exception, CoreApiException coreApiException, Dictionary<string, object> exceptionDetails)
        {
            var fromCoreApiException = coreApiException != null
                ? FormatStackTrace(coreApiException.CallStack)
                : string.Empty;

            var fromException = FormatStackTrace(exception.StackTrace);
            var stackTraceFormatted = string.Empty;

            if (!string.IsNullOrEmpty(fromException))
                stackTraceFormatted = fromException;

            if (!string.IsNullOrEmpty(fromCoreApiException))
                stackTraceFormatted = fromCoreApiException;

            if (!string.IsNullOrEmpty(stackTraceFormatted))
                exceptionDetails.Add("StackTrace", stackTraceFormatted);
        }

        /// <summary>
        /// Formats stack trace
        /// </summary>
        /// <param name="stackTrace">Raw stack trace</param>
        /// <returns></returns>
        private static string FormatStackTrace(string stackTrace)
        {
            if (string.IsNullOrEmpty(stackTrace))
                return string.Empty;

            var builder = new StringBuilder();
            builder.AppendLine();

            var space = new string(' ', 48);

            foreach (var item in stackTrace.Split(StackTraceItemDelimiter.ToCharArray()).Where(p => p.Length > 0))
            {
                builder.Append(space);
                builder.Append(item.Trim());
                builder.AppendLine();
            }

            return builder.ToString();
        }

        /// <summary>
        /// Gets a collection of key/value pairs that provide additional user-defined information about the exception.
        /// </summary>
        /// <param name="exceptionData">Exception data collection</param>
        /// <param name="exceptionDetails">Exception details</param>
        private static void GetExceptionData(ICollection exceptionData, IDictionary<string, object> exceptionDetails)
        {
            if (exceptionData == null || exceptionData.Count == 0) return;

            foreach (DictionaryEntry keyValuePair in exceptionData)
            {
                exceptionDetails.Add(keyValuePair.Key.ToString(), keyValuePair.Value);
            }
        }

        #endregion //Private methods
    }
}