namespace CES.CoreApi.Logging.Interfaces
{
	public interface IPerformanceLogConfiguration
	{
		int Threshold { get; }

		bool IsEnabled { get; }

		bool IsAsynchronous { get; }
	}
}