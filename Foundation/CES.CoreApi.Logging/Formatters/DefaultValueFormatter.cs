using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Logging.Formatters
{
    public class DefaultValueFormatter : IDefaultValueFormatter
    {
        #region Implementation of IValueFormatter

        /// <summary>
        /// Formats value as a string
        /// </summary>
        /// <param name="value">value to format</param>
        /// <returns></returns>
        public string Format(object value)
        {
            return value?.ToString() ?? string.Empty;
        }

        #endregion
    }
}