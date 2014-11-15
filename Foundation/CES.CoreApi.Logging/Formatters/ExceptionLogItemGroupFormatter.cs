using System;
using System.Globalization;
using System.Linq;
using System.Text;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Formatters
{
    public class ExceptionLogItemGroupFormatter : IExceptionLogItemGroupFormatter
    {
        #region Core

        private readonly IExceptionLogItemGroupTitleFormatter _exceptionLogItemGroupTitleFormatter;

        private const string Item = "{0}{1}";
        private const string ChildItem = "{0}{1}={2}";
        private const string ItemValue = "{0}{1}";
        private const string NewLine = "\r\n";

        /// <summary>
        /// Initializes ExceptionLogItemGroupFormatter instance
        /// </summary>
        /// <param name="exceptionLogItemGroupTitleFormatter">Log group title formatter instance</param>
        public ExceptionLogItemGroupFormatter(IExceptionLogItemGroupTitleFormatter exceptionLogItemGroupTitleFormatter)
        {
            if (exceptionLogItemGroupTitleFormatter == null)
                throw new ArgumentNullException("exceptionLogItemGroupTitleFormatter");
            _exceptionLogItemGroupTitleFormatter = exceptionLogItemGroupTitleFormatter;
        }

        #endregion //Core
        
        #region Public methods

        /// <summary>
        /// Formats log item group as a string
        /// </summary>
        /// <param name="group">Log item group</param>
        /// <returns></returns>
        public string Format(ExceptionLogItemGroup group)
        {
            if (group == null)
                throw new ArgumentNullException("group");

            var title = _exceptionLogItemGroupTitleFormatter.Format(group.Title);

            var builder = new StringBuilder();
            builder.Append(title);
            builder.AppendLine();
            builder.AppendLine();

            foreach (var item in group.Items.OrderBy(p => p.ItemName))
            {
                builder.AppendFormat(CultureInfo.InvariantCulture,
                                     Item,
                                     item.ItemName.PadRight(50, ' '),
                                     FormatItemValue(item.ItemValue));
                if (item.ChildItems.Count > 0)
                {
                    builder.AppendLine();
                    FormatChildItemValue(item, builder);
                }
                builder.AppendLine();
            }

            return builder.ToString();
        }

        #endregion //Public methods

        #region Private methods

        /// <summary>
        /// Formats child log item as a string
        /// </summary>
        /// <param name="item">Parent log item</param>
        /// <param name="builder">String builder instance</param>
        private static void FormatChildItemValue(ExceptionLogItem item, StringBuilder builder)
        {
            var prefix = new string(' ', 50);
            builder.AppendLine();
            foreach (var childItem in item.ChildItems)
            {
                builder.AppendFormat(CultureInfo.InvariantCulture,
                                     ChildItem,
                                     prefix,
                                     childItem.ItemName,
                                     FormatItemValue(childItem.ItemValue));
                builder.AppendLine();
            }
        }

        /// <summary>
        /// Adds padding to every line in multi line value
        /// </summary>
        /// <param name="rawValue">Raw value</param>
        /// <returns></returns>
        private static string FormatItemValue(string rawValue)
        {
            if (!IsMultiLineValue(rawValue)) return rawValue;

            var lines = rawValue.Split(NewLine.ToCharArray());
            var prefix = new string(' ', 48);

            lines = (from line in lines.Where(p => p.Length > 0)
                     select string.Format(CultureInfo.InvariantCulture, ItemValue, prefix, line))
                .ToArray();

            return string.Format(CultureInfo.InvariantCulture, ItemValue, NewLine, string.Join(NewLine, lines));
        }

        /// <summary>
        /// Defines whether string value is multi line or not
        /// </summary>
        /// <param name="rawValue">Raw value</param>
        /// <returns></returns>
        private static bool IsMultiLineValue(string rawValue)
        {
            return rawValue.Contains(NewLine);
        }

        #endregion //Private methods
    }
}