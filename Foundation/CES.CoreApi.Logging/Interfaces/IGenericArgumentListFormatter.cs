using System;

namespace CES.CoreApi.Logging.Interfaces
{
    public interface IGenericArgumentListFormatter
    {
        /// <summary>
        /// Gets log entry formatted
        /// </summary>
        /// <param name="argumentList">Type argument list</param>
        /// <returns></returns>
        string Format(Type[] argumentList);
    }
}