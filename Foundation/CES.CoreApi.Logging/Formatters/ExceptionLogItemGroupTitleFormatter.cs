using System.Globalization;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Logging.Formatters
{
    public class ExceptionLogItemGroupTitleFormatter : IExceptionLogItemGroupTitleFormatter
    {
        #region Core

        private const string Title = "********************{0}";

        #endregion

        #region Public methods

        /// <summary>
        /// Formats group title
        /// </summary>
        /// <param name="groupTitle">Group title</param>
        /// <returns></returns>
        public string Format(string groupTitle)
        {
            return string.Format(CultureInfo.InvariantCulture,
                                 Title,
                                 groupTitle.ToUpperInvariant())
                .PadRight(80, '*');
        }

        #endregion //Public methods
    }
}