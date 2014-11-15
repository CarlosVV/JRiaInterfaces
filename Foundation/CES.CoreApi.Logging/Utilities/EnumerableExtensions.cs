using System.Collections;
using System.Text;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Logging.Utilities
{
    internal static class EnumerableExtensions
    {
        #region Public methods

        /// <summary>
        /// Formats enumerablle list as comma delimited string list
        /// </summary>
        /// <param name="list">Enumerable list</param>
        /// <param name="defaultValueFormatter">Default value formatter instance</param>
        /// <param name="quoteValue">Defines whether value should be quoted by single quotes</param>
        /// <returns></returns>
        public static string ToStringList(this IEnumerable list, IDefaultValueFormatter defaultValueFormatter = null, bool quoteValue = false)
        {
            if (list == null)
                return string.Empty;

            var sb = new StringBuilder();

            foreach (var item in list)
            {
                sb.AppendFormat(quoteValue ? "'{0}', " : "{0}, ", FormatValue(defaultValueFormatter, item));
            }
            if (sb.Length > 0)
                sb.Remove(sb.Length - 2, 2);

            return sb.ToString();
        }

        #endregion //Public methods

        #region Private methods

        private static string FormatValue(IDefaultValueFormatter defaultValueFormatter, object item)
        {
            return defaultValueFormatter == null
                       ? item.ToString()
                       : defaultValueFormatter.Format(item);
        }

        #endregion //Private methods
    }
}