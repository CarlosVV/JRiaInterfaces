using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Interfaces
{
    public interface ISecurityLogMonitor
    {
        /// <summary>
        /// Gets or sets data container instance
        /// </summary>
        SecurityLogDataContainer DataContainer { get; }

        void LogSuccess(SecurityAuditParameters parameters);
        void LogFailure(SecurityAuditParameters parameters, string details);
    }
}