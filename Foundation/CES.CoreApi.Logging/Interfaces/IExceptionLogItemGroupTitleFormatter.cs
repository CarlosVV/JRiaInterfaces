namespace CES.CoreApi.Logging.Interfaces
{
    public interface IExceptionLogItemGroupTitleFormatter
    {
        /// <summary>
        /// Formats group title
        /// </summary>
        /// <param name="groupTitle">Group title</param>
        /// <returns></returns>
        string Format(string groupTitle);
    }
}