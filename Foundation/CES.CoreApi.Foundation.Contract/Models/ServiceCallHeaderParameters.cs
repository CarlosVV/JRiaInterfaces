using System;

namespace CES.CoreApi.Foundation.Contract.Models
{
    public class ServiceCallHeaderParameters
    {
        #region Core

        public ServiceCallHeaderParameters(int applicationId, string operationName, DateTime timestamp, 
            string applicationSessionId, string referenceNumber, string referenceNumberType)
        {
            ApplicationId = applicationId;
            OperationName = operationName;
            Timestamp = timestamp;
            ApplicationSessionId = applicationSessionId;
            ReferenceNumber = referenceNumber;
            ReferenceNumberType = referenceNumberType;
        }

        #endregion

        #region Public properties

        public int ApplicationId { get; private set; }
        public string OperationName { get; private set; }
        public DateTime Timestamp { get; private set; }
        public string ApplicationSessionId { get; private set; }
        public string ReferenceNumber { get; private set; }
        public string ReferenceNumberType { get; private set; }

        #endregion
    }
}
