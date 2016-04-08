using System.Net;

namespace CES.CoreApi.Logging.Models
{
	public class RequestLogInfo
	{
		public WebHeaderCollection Headers { get; set; }
		public string Method { get; set; }
		public string QueryString { get; set; }
		public object Envelope { get; set; }
		public object RequestMessage { get; set; }
	}
}
