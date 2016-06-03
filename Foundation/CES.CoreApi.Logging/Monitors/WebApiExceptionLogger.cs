﻿using CES.CoreApi.Logging.Log4Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.ExceptionHandling;

namespace CES.CoreApi.Logging.Monitors
{
	public class WebApiExceptionLogger : ExceptionLogger
	{
		public override void Log(ExceptionLoggerContext context)
		{
			var log = new Log4NetProxy();
			log.Publish(Enumerations.LogEntryType.Error, RequestToString(context.Request), context.Exception);
		}

		private static string RequestToString(HttpRequestMessage request)
		{
			var message = new StringBuilder();
			if (request.Method != null)
				message.Append(request.Method);
			if (request.RequestUri != null)
				message.Append(" ").Append(request.RequestUri);
			if (request.Headers != null)
			{
				foreach (var item in request.Headers)
				{

					message.AppendLine(" ").Append(item.Key).Append(":");
					foreach (var str in item.Value)
					{
						message.Append(str).Append(" ");
					}
				}
			}
			

			return message.ToString();
		}
	}
}
