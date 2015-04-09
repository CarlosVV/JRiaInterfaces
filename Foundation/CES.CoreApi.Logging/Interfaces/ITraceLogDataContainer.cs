using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Enumerations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CES.CoreApi.Logging.Interfaces
{
    public interface ITraceLogDataContainer
    {
        /// <summary>
        /// Gets or sets time when request was sent
        /// </summary>
        [DataMember]
        DateTime RequestTime { get; }

        /// <summary>
        /// Returns time when response was received
        /// </summary>
        [DataMember]
        DateTime ResponseTime { get; }

        /// <summary>
        /// Gets or sets request headers
        /// </summary>
        [DataMember]
        IDictionary<string, object> Headers { get; set; }

        /// <summary>
        /// Gets or sets request message
        /// </summary>
        [DataMember]
        [DefaultValue("")]
        string RequestMessage { get; set; }

        /// <summary>
        /// Gets or sets log record creation time
        /// </summary>
        [DataMember(Name = "timestamp")]
        DateTime LogTime { get; }

        /// <summary>
        /// Gets or sets request message length
        /// </summary>
        [DataMember]
        long RequestMessageLength { get; }

        /// <summary>
        /// Gets or sets response message
        /// </summary>
        [DataMember]
        [DefaultValue("")]
        string ResponseMessage { get; set; }

        /// <summary>
        /// Gets or sets response message length
        /// </summary>
        [DataMember]
        long ResponseMessageLength { get; }

        /// <summary>
        /// Gets or sets binary response message length
        /// </summary>
        [DataMember]
        long BinaryResponseMessageLength { get; set; }

        /// <summary>
        /// Gets or sets data provider type
        /// </summary>
        [DataMember]
        string ProviderType { get; set; }

        [DataMember]
        string ClientSideMessage { get; set; }

        [DataMember]
        Guid MessageId { get; }

        /// <summary>
        /// Gets log type
        /// </summary>
        [DataMember]
        [JsonConverter(typeof (StringEnumConverter))]
        LogType LogType { get; }

        /// <summary>
        /// Returns string representation of the log entry
        /// </summary>
        /// <returns>String representation of the log entry</returns>
        string ToString();
    }
}