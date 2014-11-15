using System;

namespace CES.CoreApi.Logging.Interfaces
{
    public interface IFileSizeFormatter
    {
        /// <summary>
        /// Formats file size
        /// </summary>
        /// <param name="fileSize">File size in bytes</param>
        /// <returns></returns>
        string Format(long fileSize);

        /// <summary>
        /// Converts the value of a specified object to an equivalent string representation using specified format and culture-specific formatting information.
        /// </summary>
        /// <param name="format"></param>
        /// <param name="arg"></param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        string Format(string format, object arg, IFormatProvider formatProvider);
    }
}