using System;
using System.Collections.ObjectModel;
using System.Text;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Formatters
{
    public class ExceptionLogItemGroupListFormatter : IExceptionLogItemGroupListFormatter
    {
        #region Core

        private readonly IExceptionLogItemGroupFormatter _exceptionLogItemGroupFormatter;

        /// <summary>
        /// Initializes ExceptionLogItemGroupListFormatter instance
        /// </summary>
        /// <param name="exceptionLogItemGroupFormatter">Exception log item group formatter instance</param>
        public ExceptionLogItemGroupListFormatter(IExceptionLogItemGroupFormatter exceptionLogItemGroupFormatter)
        {
            if (exceptionLogItemGroupFormatter == null)
                throw new ArgumentNullException("exceptionLogItemGroupFormatter");

            _exceptionLogItemGroupFormatter = exceptionLogItemGroupFormatter;
        }

        #endregion //Core

        #region Public methods
        
        /// <summary>
        /// Formats exception log item group list as a string
        /// </summary>
        /// <param name="groups">Exception log item group list</param>
        /// <returns></returns>
        public string Format( Collection<ExceptionLogItemGroup> groups)
        {
            var builder = new StringBuilder();

            foreach (var group in groups)
            {
                builder.Append(_exceptionLogItemGroupFormatter.Format(group));
                builder.AppendLine();
            }

            return builder.ToString();
        }

        #endregion //Public methods
    }
}
