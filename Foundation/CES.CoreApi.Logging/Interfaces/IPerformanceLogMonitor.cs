using System.Reflection;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Interfaces
{
	public interface IPerformanceLogMonitor
	{
		void Start(MethodBase method);

		void Start(string methodName);

		void Stop();

		PerformanceLogDataContainer DataContainer { get; }
	}
}