using System;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Logging.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CES.CoreApi.Logging.Models
{
    [DataContract]
    public class SecurityLogDataContainer : IDataContainer
    {
        #region Core

        private readonly IJsonDataContainerFormatter _jsonDataContainerFormatter;

        public SecurityLogDataContainer(IJsonDataContainerFormatter jsonDataContainerFormatter, ICurrentDateTimeProvider currentDateTimeProvider)
        {
            if (jsonDataContainerFormatter == null) throw new ArgumentNullException("jsonDataContainerFormatter");
            if (currentDateTimeProvider == null) throw new ArgumentNullException("currentDateTimeProvider");

            _jsonDataContainerFormatter = jsonDataContainerFormatter;
            AuditTime = currentDateTimeProvider.GetCurrentUtc();
            MessageId = Guid.NewGuid();
        }

        #endregion

        #region Public properties

        [DataMember]
        public int ServiceApplicationId { get; set; }
        [DataMember]
        public int ClientApplicationId { get; set; }
        [DataMember]
        public string Operation { get; set; }
        [DataMember]
        [JsonConverter(typeof(StringEnumConverter))]
        public SecurityAuditResult AuditResult { get; set; }
        [DataMember]
        public DateTime AuditTime { get; private set; }
        [DataMember]
        public string Details { get; set; }
        [DataMember]
        public Guid MessageId { get; private set; }
        
        [DataMember]
        [JsonConverter(typeof(StringEnumConverter))]
        public LogType LogType
        {
            get { return LogType.SecurityAudit; }
        }
        
        #endregion

        #region Public methods

        public override string ToString()
        {
            return _jsonDataContainerFormatter.Format(this);
        }

        #endregion
    }
}
