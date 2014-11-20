using System;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Foundation.Contract.Enumerations;
using CES.CoreApi.Foundation.Contract.Exceptions;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Foundation.Security
{
    public class SecurityAuditLogger : ISecurityAuditLogger
    {
        private readonly ILogManager _logManager;

        public SecurityAuditLogger(ILogManager logManager)
        {
            if (logManager == null)
                throw new CoreApiException(TechnicalSubSystem.GeoLocationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "logManager");
            _logManager = logManager;
        }

        public void LogSuccess(SecurityAuditParameters parameters)
        {
            if (parameters == null) throw new ArgumentNullException("parameters");

            var dataContainer = _logManager.GetContainerInstance<SecurityLogDataContainer>();
            dataContainer.ServiceApplicationId = parameters.ServiceApplicationId;
            dataContainer.ClientApplicationId = parameters.ClientApplicationId;
            dataContainer.Operation = parameters.Operation;
            dataContainer.AuditResult = SecurityAuditResult.AccessGranted;

            _logManager.Publish(dataContainer);
        }

        public void LogFailure(SecurityAuditParameters parameters, string details)
        {
            if (parameters == null) throw new ArgumentNullException("parameters");

            var dataContainer = _logManager.GetContainerInstance<SecurityLogDataContainer>();
            dataContainer.ServiceApplicationId = parameters.ServiceApplicationId;
            dataContainer.ClientApplicationId = parameters.ClientApplicationId;
            dataContainer.Operation = parameters.Operation;
            dataContainer.AuditResult = SecurityAuditResult.AccessDenied;
            dataContainer.Details = details;

            _logManager.Publish(dataContainer);
        }
    }
}
