using System.Configuration;
using CES.CoreApi.Logging.Configuration;

namespace CES.CoreApi.Logging.Interfaces
{
	public interface ITraceLogConfiguration
	{
		bool IsEnabled { get; }

		bool IsAsynchronous { get; }

		bool IsRequestLoggingEnabled { get; }

		bool IsResponseLoggingEnabled { get; }
	}
}