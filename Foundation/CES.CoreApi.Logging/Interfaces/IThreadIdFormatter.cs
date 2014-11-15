namespace CES.CoreApi.Logging.Interfaces
{
    public interface IThreadIdFormatter
    {
        /// <summary>
        /// Returns thread identifier formatted
        /// </summary>
        string Format(int threadId);
    }
}