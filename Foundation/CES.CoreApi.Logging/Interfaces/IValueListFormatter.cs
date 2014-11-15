using System.Collections.Generic;

namespace CES.CoreApi.Logging.Interfaces
{
    public interface IValueListFormatter
    {
        /// <summary>
        /// Gets log entry formatted
        /// </summary>
        /// <param name="argumentList">Type argument list</param>
        /// <returns></returns>
        string Format(IEnumerable<object> argumentList);
    }
}