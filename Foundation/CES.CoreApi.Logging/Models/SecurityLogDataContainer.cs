using System;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Logging.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CES.CoreApi.Logging.Models
{
	public class SecurityLogDataContainer : IDataContainer
	{
		private readonly IJsonDataContainerFormatter _jsonDataContainerFormatter;

		public SecurityLogDataContainer(IJsonDataContainerFormatter jsonDataContainerFormatter, ICurrentDateTimeProvider currentDateTimeProvider)
		{
			if (jsonDataContainerFormatter == null)
				throw new ArgumentNullException("jsonDataContainerFormatter");
			if (currentDateTimeProvider == null)
				throw new ArgumentNullException("currentDateTimeProvider");

			_jsonDataContainerFormatter = jsonDataContainerFormatter;
			AuditTime = currentDateTimeProvider.GetCurrentUtc();
			MessageId = Guid.NewGuid();
		}
		
		public int ServiceApplicationId { get; set; }

		public int ClientApplicationId { get; set; }

		public string Operation { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		public SecurityAuditResult AuditResult { get; set; }

		[JsonProperty(PropertyName = "Timestamp")]
		public DateTime AuditTime { get; private set; }

		public string Details { get; set; }

		public Guid MessageId { get; private set; }


		[JsonConverter(typeof(StringEnumConverter))]
		public LogType LogType => LogType.SecurityAudit;
		public dynamic ApplicationContext { get; set; }
		public override string ToString()
		{
			return _jsonDataContainerFormatter.Format(this);
		}
	}
}
