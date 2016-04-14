using System.Data.Common;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Interfaces
{
	public interface IDatabasePerformanceLogMonitor
	{
		DatabasePerformanceLogDataContainer DataContainer { get; }

		void Start(DbCommand command);

		void Stop();

		void UpdateConnectionDetails(DbCommand command);
	}
}