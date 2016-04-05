using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Logging.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CES.CoreApi.Logging.Models
{
	public class SecurityLogDataContainer : IDataContainer
	{
		#region Core

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

		#endregion

		#region Public properties

		public int ServiceApplicationId { get; set; }

		public int ClientApplicationId { get; set; }

		public string Operation { get; set; }

		[JsonConverter(typeof(StringEnumConverter))]
		public SecurityAuditResult AuditResult { get; set; }
		[DataMember(Name = "timestamp")]
		public DateTime AuditTime { get; private set; }

		public string Details { get; set; }

		public Guid MessageId { get; private set; }


		[JsonConverter(typeof(StringEnumConverter))]
		public LogType LogType
		{
			get { return LogType.SecurityAudit; }
		}

		/// <summary>
		/// Gets or sets application context information
		/// </summary>

		public dynamic ApplicationContext
		{
			get;
			set;
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
