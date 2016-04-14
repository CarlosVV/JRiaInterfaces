using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Providers
{
	public class ServerInformationProvider : IServerInformationProvider
	{
		public void AddDetails(ExceptionLogDataContainer exceptionLogDataContainer, ServerLogInfo serverLogInfo)
		{
			var group = exceptionLogDataContainer.GetGroupByTitle("Server Details");
			group.AddItem("Host", serverLogInfo.Host);
			group.AddItem("Port", serverLogInfo.Port);
			group.AddItem("Scheme", serverLogInfo.Scheme);
			group.AddItem("URL", serverLogInfo.OriginalString);
			group.AddItem("Query", serverLogInfo.Query);
		}
	}
}
