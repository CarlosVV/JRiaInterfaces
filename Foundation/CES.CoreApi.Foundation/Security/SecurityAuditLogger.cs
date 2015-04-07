using System;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Foundation.Security
{
    public class SecurityAuditLogger : ISecurityAuditLogger
    {
        private readonly ISecurityLogMonitor _securityLogMonitor;

        public SecurityAuditLogger(ISecurityLogMonitor securityLogMonitor)
        {
            if (securityLogMonitor == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "securityLogMonitor");
            _securityLogMonitor = securityLogMonitor;
        }

        public void LogSuccess(SecurityAuditParameters parameters)
        {
            if (parameters == null) throw new ArgumentNullException("parameters");
            _securityLogMonitor.LogSuccess(parameters);
        }

        public void LogFailure(SecurityAuditParameters parameters, string details)
        {
            if (parameters == null) throw new ArgumentNullException("parameters");
            _securityLogMonitor.LogFailure(parameters, details);
        }
    }
}
