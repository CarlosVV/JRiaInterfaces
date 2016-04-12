using CES.CoreApi.Foundation.Contract.Interfaces;
using System;
using System.Security.Principal;


namespace CES.CoreApi.Foundation.Contract.Models
{
    public class ClientApplicationIdentity : IIdentity
    {
        #region Core

        public ClientApplicationIdentity(IApplication application, ServiceCallHeaderParameters headerParameters)
        {
            if (application == null) throw new ArgumentNullException("application");
            if (headerParameters == null) throw new ArgumentNullException("headerParameters");
            
            ApplicationId = application.Id;
            Name = application.Name;
            OperationName = headerParameters.OperationName;
            Timestamp = headerParameters.Timestamp;
            ApplicationSessionId = headerParameters.ApplicationSessionId;
            ReferenceNumber = headerParameters.ReferenceNumber;
            ReferenceNumberType = headerParameters.ReferenceNumberType;
            CorrelationId = headerParameters.CorrelationId;
        }

        #endregion

        #region Public properties

        public int ApplicationId { get; private set; }

        public string Name { get; private set; }

        public string AuthenticationType { get { return "Database"; } }

        public bool IsAuthenticated { get { return true; } }

        public string OperationName { get; private set; }

        public DateTime Timestamp { get; private set; }

        public string ApplicationSessionId { get; private set; }

        public string ReferenceNumber { get; private set; }

        public string ReferenceNumberType { get; private set; }

        public string CorrelationId { get; private set; }

        #endregion
    }
}
