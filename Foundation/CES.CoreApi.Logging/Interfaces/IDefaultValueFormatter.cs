namespace CES.CoreApi.Logging.Interfaces
{
    public interface IDefaultValueFormatter
    {
        /// <summary>
        /// Formats value as a string
        /// </summary>
        /// <param name="value">value to format</param>
        /// <returns></returns>
        string Format(object value);
    }
}