namespace CES.CoreApi.Logging.Interfaces
{
	public interface ITraceLogMonitor
	{
		ITraceLogDataContainer DataContainer { get; }

		void Start();

		void Stop();
	}
}