using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Interfaces
{
	public interface ISecurityLogMonitor
	{
		SecurityLogDataContainer DataContainer { get; }

		void LogSuccess(SecurityAuditParameters parameters);
		void LogFailure(SecurityAuditParameters parameters, string details);
	}
}