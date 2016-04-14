namespace CES.CoreApi.Logging.Interfaces
{
	public interface IDatabasePerformanceLogConfiguration
	{
		int Threshold { get; }

		bool IsEnabled { get; }

		bool IsAsynchronous { get; }
	}
}