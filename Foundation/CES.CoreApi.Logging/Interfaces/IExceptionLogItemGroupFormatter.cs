using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Interfaces
{
    public interface IExceptionLogItemGroupFormatter
    {
        /// <summary>
        /// Formats log item group as a string
        /// </summary>
        /// <param name="group">Log item group</param>
        /// <returns></returns>
        string Format(ExceptionLogItemGroup group);
    }
}