using System;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Models;
using CES.CoreApi.Logging.Enumerations;

namespace CES.CoreApi.Logging.Monitors
{
	public class SecurityLogMonitor: BaseLogMonitor, ISecurityLogMonitor
    {
        public SecurityLogMonitor(SecurityLogDataContainer dataContainer, ILoggerProxy logProxy, ILogConfigurationProvider configuration) 
            : base(logProxy, configuration)
        {
            if (dataContainer == null) throw new ArgumentNullException("dataContainer");
            DataContainer = dataContainer;
        }

        /// <summary>
        /// Gets or sets data container instance
        /// </summary>
        public SecurityLogDataContainer DataContainer { get; private set; }

        public void LogSuccess(SecurityAuditParameters parameters)
        {
            if (parameters == null) throw new ArgumentNullException("parameters");

            DataContainer.ServiceApplicationId = parameters.ServiceApplicationId;
            DataContainer.ClientApplicationId = parameters.ClientApplicationId;
            DataContainer.Operation = parameters.Operation;
            DataContainer.AuditResult = SecurityAuditResult.AccessGranted;

            Publish(DataContainer);
        }

        public void LogFailure(SecurityAuditParameters parameters, string details)
        {
            if (parameters == null) throw new ArgumentNullException("parameters");

            DataContainer.ServiceApplicationId = parameters.ServiceApplicationId;
            DataContainer.ClientApplicationId = parameters.ClientApplicationId;
            DataContainer.Operation = parameters.Operation;
            DataContainer.AuditResult = SecurityAuditResult.AccessDenied;
            DataContainer.Details = details;

            Publish(DataContainer);
        }
    }
}
