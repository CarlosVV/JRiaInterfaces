using System;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Logging.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CES.CoreApi.Logging.Models
{
    [JsonObject(MemberSerialization.OptIn)]
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

        [JsonProperty]
        public int ServiceApplicationId { get; set; }
        [JsonProperty]
        public int ClientApplicationId { get; set; }
        [JsonProperty]
        public int ServerId { get; set; }
        [JsonProperty]
        public string Operation { get; set; }
        [JsonProperty]
        [JsonConverter(typeof(StringEnumConverter))]
        public SecurityAuditResult AuditResult { get; set; }
        [JsonProperty]
        public DateTime AuditTime { get; private set; }
        [JsonProperty]
        public string Details { get; set; }
        [JsonProperty]
        public Guid MessageId { get; private set; }
        
        [JsonProperty]
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
