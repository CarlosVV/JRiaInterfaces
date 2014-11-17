using System;
using System.Collections.Generic;
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
        [JsonProperty]
        DateTime RequestTime { get; }

        /// <summary>
        /// Returns time when response was received
        /// </summary>
        [JsonProperty]
        DateTime ResponseTime { get; }

        /// <summary>
        /// Gets or sets request headers
        /// </summary>
        [JsonProperty]
        IDictionary<string, object> Headers { get; set; }

        /// <summary>
        /// Gets or sets request message
        /// </summary>
        [JsonProperty]
        string RequestMessage { get; set; }

        /// <summary>
        /// Gets or sets log record creation time
        /// </summary>
        [JsonProperty]
        DateTime LogTime { get; }

        /// <summary>
        /// Gets or sets request message length
        /// </summary>
        [JsonProperty]
        long RequestMessageLength { get; }

        /// <summary>
        /// Gets or sets response message
        /// </summary>
        [JsonProperty]
        string ResponseMessage { get; set; }

        /// <summary>
        /// Gets or sets response message length
        /// </summary>
        [JsonProperty]
        long ResponseMessageLength { get; }

        /// <summary>
        /// Gets or sets binary response message length
        /// </summary>
        [JsonProperty]
        long BinaryResponseMessageLength { get; set; }

        /// <summary>
        /// Gets or sets data provider type
        /// </summary>
        [JsonProperty]
        string ProviderType { get; set; }

        [JsonProperty]
        string ClientSideMessage { get; set; }

        /// <summary>
        /// Gets log type
        /// </summary>
        [JsonProperty]
        [JsonConverter(typeof (StringEnumConverter))]
        LogType LogType { get; }

        /// <summary>
        /// Returns string representation of the log entry
        /// </summary>
        /// <returns>String representation of the log entry</returns>
        string ToString();
    }
}