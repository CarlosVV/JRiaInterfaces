using System.Globalization;
using CES.CoreApi.Logging.Interfaces;

namespace CES.CoreApi.Logging.Formatters
{
    public class ThreadIdFormatter : IThreadIdFormatter
    {
        private const string ThreadId = "0{0}";

        #region Public methods

        /// <summary>
        /// Returns thread identifier formatted
        /// </summary>
        public string Format(int threadId)
        {
            var threadIdString = threadId.ToString(CultureInfo.InvariantCulture);
            return threadIdString.Length == 1
                       ? string.Format(ThreadId, threadIdString)
                       : threadIdString;
        }

        #endregion //Public methods
    }
}