using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using CES.CoreApi.Logging.Interfaces;
using CES.CoreApi.Logging.Models;

namespace CES.CoreApi.Logging.Providers
{
    public class HttpRequestInformationProvider : IHttpRequestInformationProvider
    {
       
        internal const string ContentLength = "Content-Length";
        internal const string HttpRequest = "httpRequest";
        
        private readonly IFileSizeFormatter _fileSizeFormatter;

        /// <summary>
        /// Initializes HtppRequestInformationProvider instance
        /// </summary>
        /// <param name="fileSizeFormatter">File size formatter</param>
        public HttpRequestInformationProvider(IFileSizeFormatter fileSizeFormatter)
        {
            if (fileSizeFormatter == null) throw new ArgumentNullException("fileSizeFormatter");

            _fileSizeFormatter = fileSizeFormatter;
        }

		public void AddDetails(ExceptionLogDataContainer exceptionLogDataContainer,
		{
			if (context == null)
				return;

			if (!context.IncomingMessageProperties.Keys.Contains(HttpRequest))
				return;
			var item = context.IncomingMessageProperties[HttpRequest] as HttpRequestMessageProperty;
			if (item == null)
				return;

			//Check if message exists - for RESTful calls we don't have message body
			if (OperationContext.Current.RequestContext.RequestMessage.IsEmpty)
				return;

			var group = exceptionLogDataContainer.GetGroupByTitle("Incoming Message Details");
			group.AddItem("Version", OperationContext.Current.IncomingMessageVersion.Envelope);
			group.AddItem("Original Message", OperationContext.Current.RequestContext.RequestMessage);
			group.AddItem("Method", item.Method);
			group.AddItem("QueryString", item.QueryString);

			for (var i = 0; i < item.Headers.Count; i++)
			{
				var itemName = item.Headers.GetKey(i);
				group.AddItem(itemName, FormatItemValue(itemName, item.Headers.Get(i)));
			}
		}

		public void AddDetails(ExceptionLogDataContainer exceptionLogDataContainer, OperationContext context)
        {
            if (context == null) return;

            if (!context.IncomingMessageProperties.Keys.Contains(HttpRequest)) return;
            var item = context.IncomingMessageProperties[HttpRequest] as HttpRequestMessageProperty;
            if (item == null) return;

            //Check if message exists - for RESTful calls we don't have message body
            if (OperationContext.Current.RequestContext.RequestMessage.IsEmpty) return;

            var group = exceptionLogDataContainer.GetGroupByTitle("Incoming Message Details");
            group.AddItem("Version", OperationContext.Current.IncomingMessageVersion.Envelope);
            group.AddItem("Original Message", OperationContext.Current.RequestContext.RequestMessage);
            group.AddItem("Method", item.Method);
            group.AddItem("QueryString", item.QueryString);

            for (var i = 0; i < item.Headers.Count; i++)
            {
                var itemName = item.Headers.GetKey(i);
                group.AddItem(itemName, FormatItemValue(itemName, item.Headers.Get(i)));
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
