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
		DateTime RequestTime { get; }

		DateTime ResponseTime { get; }

		IDictionary<string, object> Headers { get; set; }

		string RequestMessage { get; set; }

		DateTime LogTime { get; }

		long RequestMessageLength { get; }

		string ResponseMessage { get; set; }

		long ResponseMessageLength { get; }

		long BinaryResponseMessageLength { get; set; }

		string ProviderType { get; set; }

		string ClientSideMessage { get; set; }

		Guid MessageId { get; }

		[JsonConverter(typeof(StringEnumConverter))]
		LogType LogType { get; }

		string ToString();
	}
}