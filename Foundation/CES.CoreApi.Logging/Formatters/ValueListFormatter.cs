using System.Collections.Generic;
using System.Globalization;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Utilities;

namespace CES.CoreApi.Logging.Formatters
{
    public class ValueListFormatter : IValueListFormatter
    {
        private const string ArgumentList = "{0}";

        #region Public methods

        /// <summary>
        /// Gets log entry formatted
        /// </summary>
        /// <param name="argumentList">Type argument list</param>
        /// <returns></returns>
        public string Format(IEnumerable<object> argumentList)
        {
            return argumentList != null
                       ? string.Format(CultureInfo.InvariantCulture,
                                       ArgumentList,
                                       argumentList.ToStringList())
                       : string.Empty;
        }

        #endregion //Public methods
    }
}
