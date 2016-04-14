using System;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Providers
{
	public class HttpRequestInformationProvider : IHttpRequestInformationProvider
	{
		internal const string ContentLength = "Content-Length";
		internal const string HttpRequest = "httpRequest";

		private readonly IFileSizeFormatter _fileSizeFormatter;

		public HttpRequestInformationProvider(IFileSizeFormatter fileSizeFormatter)
		{
			if (fileSizeFormatter == null)
				throw new ArgumentNullException("fileSizeFormatter");

			_fileSizeFormatter = fileSizeFormatter;
		}

		public void AddDetails(ExceptionLogDataContainer exceptionLogDataContainer, RequestLogInfo requestLogInfo)
		{
			var group = exceptionLogDataContainer.GetGroupByTitle("Incoming Message Details");
			group.AddItem("Envelope", requestLogInfo.Envelope);
			group.AddItem("Original Message", requestLogInfo.RequestMessage);
			group.AddItem("Method", requestLogInfo.Method);
			group.AddItem("QueryString", requestLogInfo.QueryString);

			for (var i = 0; i < requestLogInfo.Headers.Count; i++)
			{
				var itemName = requestLogInfo.Headers.GetKey(i);
				group.AddItem(itemName, FormatItemValue(itemName, requestLogInfo.Headers.Get(i)));
			}
		}

		private string FormatItemValue(string itemName, string itemValueRaw)
		{
			return itemName.Equals(ContentLength, StringComparison.OrdinalIgnoreCase)
				? FormatFileSize(itemValueRaw)
				: itemValueRaw;
		}

		private string FormatFileSize(string itemValueRaw)
		{
			long itemValue;
			return long.TryParse(itemValueRaw, out itemValue)
				? _fileSizeFormatter.Format(itemValue)
				: itemValueRaw;
		}
	}
}
