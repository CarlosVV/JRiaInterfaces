using System.Collections.ObjectModel;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Interfaces
{
    public interface IExceptionLogItemGroupListFormatter
    {
        /// <summary>
        /// Formats exception log item group list as a string
        /// </summary>
        /// <param name="groups">Exception log item group list</param>
        /// <returns></returns>
        string Format( Collection<ExceptionLogItemGroup> groups);
    }
}