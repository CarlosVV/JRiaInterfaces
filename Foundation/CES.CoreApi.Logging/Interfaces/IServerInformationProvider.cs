using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Interfaces
{
	public interface IServerInformationProvider
	{
		void AddDetails(ExceptionLogDataContainer exceptionLogDataContainer, ServerLogInfo serverLogInfo);
	}
}