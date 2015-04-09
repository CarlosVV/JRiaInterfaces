﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Logging.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CES.CoreApi.Logging.Models
{
    [DataContract]
    public class TraceLogDataContainer : IDataContainer, ITraceLogDataContainer
    {
        #region Core

        private readonly IJsonDataContainerFormatter _jsonDataContainerFormatter;
        private readonly ILogConfigurationProvider _configuration;
        private readonly ICurrentDateTimeProvider _currentDateTimeProvider;
        private string _requestMessage;
        private string _responseMessage;

        public TraceLogDataContainer(IJsonDataContainerFormatter jsonDataContainerFormatter, ILogConfigurationProvider configuration, 
            ICurrentDateTimeProvider currentDateTimeProvider)
        {
            if (jsonDataContainerFormatter == null) throw new ArgumentNullException("jsonDataContainerFormatter");
            if (configuration == null) throw new ArgumentNullException("configuration");
            if (currentDateTimeProvider == null) throw new ArgumentNullException("currentDateTimeProvider");

            _jsonDataContainerFormatter = jsonDataContainerFormatter;
            _configuration = configuration;
            _currentDateTimeProvider = currentDateTimeProvider;
            _requestMessage = string.Empty;
            _responseMessage = string.Empty;
            MessageId = Guid.NewGuid();
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets time when request was sent
        /// </summary>
        [DataMember]
        public DateTime RequestTime { get; private set; }

        /// <summary>
        /// Returns time when response was received
        /// </summary>
        [DataMember]
        public DateTime ResponseTime { get; private set; }

        /// <summary>
        /// Gets or sets request headers
        /// </summary>
        [DataMember]
        public IDictionary<string, object> Headers { get; set; }

        /// <summary>
        /// Gets or sets request message
        /// </summary>
        [DataMember]
        [DefaultValue("")]
        public string RequestMessage
        {
            get
            {
                return _requestMessage;
            }
            set
            {
                RequestTime = _currentDateTimeProvider.GetCurrentUtc();
                value = string.IsNullOrEmpty(value) ? string.Empty : value;
                RequestMessageLength = value.Length;
                
                if (!_configuration.TraceLogConfiguration.IsRequestLoggingEnabled)
                    return;
                
                _requestMessage = value;
            }
        }

        /// <summary>
        /// Gets or sets log record creation time
        /// </summary>
        [DataMember(Name = "timestamp")]
        public DateTime LogTime
        {
            get
            {
                return _currentDateTimeProvider.GetCurrentUtc();
            }
        }

        /// <summary>
        /// Gets or sets request message length
        /// </summary>
        [DataMember]
        public long RequestMessageLength
        {
            get; private set; 
        }

        /// <summary>
        /// Gets or sets response message
        /// </summary>
        [DataMember]
        [DefaultValue("")]
        public string ResponseMessage
        {
            get
            {
                return _responseMessage;
            }
            set
            {
                ResponseTime = _currentDateTimeProvider.GetCurrentUtc();
                value = string.IsNullOrEmpty(value) ? string.Empty : value;
                ResponseMessageLength = value.Length;

                if (!_configuration.TraceLogConfiguration.IsResponseLoggingEnabled)
                    return;

                _responseMessage = value;
            }
        }

        /// <summary>
        /// Gets or sets response message length
        /// </summary>
        [DataMember]
        public long ResponseMessageLength
        {
            get; private set; 
        }

        /// <summary>
        /// Gets or sets binary response message length
        /// </summary>
        [DataMember]
        public long BinaryResponseMessageLength { get; set; }

        /// <summary>
        /// Gets or sets data provider type
        /// </summary>
        [DataMember]
        public string ProviderType { get; set; }

        [DataMember]
        public string ClientSideMessage { get; set; }

        [DataMember]
        public Guid MessageId { get; private set; }

        #endregion //Public properties

        #region Overriding

        /// <summary>
        /// Returns string representation of the log entry
        /// </summary>
        /// <returns>String representation of the log entry</returns>
        public override string ToString()
        {
            return _jsonDataContainerFormatter.Format(this);
        }

        /// <summary>
        /// Gets log type
        /// </summary>
        [DataMember]
        [JsonConverter(typeof(StringEnumConverter))]
        public LogType LogType
        {
            get { return LogType.TraceLog; }
        }

        #endregion //Overriding
    }
}
