using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Interfaces
{
	public interface IHttpRequestInformationProvider
	{
		void AddDetails(ExceptionLogDataContainer exceptionLogDataContainer, RequestLogInfo requestLogInfo);
	}
}