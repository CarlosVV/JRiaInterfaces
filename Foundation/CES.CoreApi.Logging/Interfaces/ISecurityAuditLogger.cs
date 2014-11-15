using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Interfaces
{
    public interface ISecurityAuditLogger
    {
        void LogSuccess(SecurityAuditParameters parameters);
        void LogFailure(SecurityAuditParameters parameters, string details);
    }
}