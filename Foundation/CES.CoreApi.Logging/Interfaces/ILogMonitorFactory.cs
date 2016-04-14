namespace CES.CoreApi.Logging.Interfaces
{
	public interface ILogMonitorFactory
	{
		T CreateNew<T>() where T : class;
	}
}