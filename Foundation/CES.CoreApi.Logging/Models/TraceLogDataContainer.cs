using System;
using System.Collections.Generic;
using CES.CoreApi.Logging.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using CES.CoreApi.Logging.Enumerations;

namespace CES.CoreApi.Logging.Models
{
	public class TraceLogDataContainer : IDataContainer, ITraceLogDataContainer
	{
		private readonly IJsonDataContainerFormatter _jsonDataContainerFormatter;
		private readonly ILogConfigurationProvider _configuration;

		private string _requestMessage;
		private string _responseMessage;

		public TraceLogDataContainer(IJsonDataContainerFormatter jsonDataContainerFormatter, ILogConfigurationProvider configuration
			)
		{
			if (jsonDataContainerFormatter == null)
				throw new ArgumentNullException("jsonDataContainerFormatter");
			if (configuration == null)
				throw new ArgumentNullException("configuration");
		

			_jsonDataContainerFormatter = jsonDataContainerFormatter;
			_configuration = configuration;
			
			_requestMessage = string.Empty;
			_responseMessage = string.Empty;
			MessageId = Guid.NewGuid();
		}
		public DateTime RequestTime { get; private set; }

		public DateTime ResponseTime { get; private set; }

		public IDictionary<string, object> Headers { get; set; }

		public string RequestMessage
		{
			get
			{
				return _requestMessage;
			}
			set
			{
				RequestTime = DateTime.UtcNow;
				value = string.IsNullOrEmpty(value) ? string.Empty : value;
				RequestMessageLength = value.Length;

				if (!_configuration.TraceLogConfiguration.IsRequestLoggingEnabled)
					return;

				_requestMessage = value;
			}
		}
		public DateTime LogTime => DateTime.UtcNow;

		public long RequestMessageLength
		{
			get; private set;
		}

		public string ResponseMessage
		{
			get
			{
				return _responseMessage;
			}
			set
			{
				ResponseTime = DateTime.UtcNow;
				value = string.IsNullOrEmpty(value) ? string.Empty : value;
				ResponseMessageLength = value.Length;

				if (!_configuration.TraceLogConfiguration.IsResponseLoggingEnabled)
					return;

				_responseMessage = value;
			}
		}

		public long ResponseMessageLength
		{
			get; private set;
		}

		public long BinaryResponseMessageLength { get; set; }

		public string ProviderType { get; set; }

		public string ClientSideMessage { get; set; }

		public Guid MessageId { get; private set; }

		public dynamic ApplicationContext { get; set; }

		public override string ToString()
		{
			return _jsonDataContainerFormatter.Format(this);
		}

		[JsonConverter(typeof(StringEnumConverter))]
		public LogType LogType => LogType.TraceLog;
	}
}
