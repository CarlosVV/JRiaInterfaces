namespace CES.CoreApi.Logging.Interfaces
{
    public interface IDatabasePerformanceLogConfiguration
    {
        /// <summary>
        /// Returns threshold in milliseconds, after which system logs call, 0 means log all calls
        /// </summary>
        int Threshold { get; }

        /// <summary>
        /// Returns whether db performance log enabled
        /// </summary>
        bool IsEnabled { get; }

        /// <summary>
        /// Returns whether db performance log asynchronous
        /// </summary>
        bool IsAsynchronous { get; }
    }
}