namespace CES.CoreApi.Logging.Models
{
	public class ServerLogInfo
	{
		public string Host { get; set; }
		public string Scheme { get; set; }
		public string OriginalString { get; set; }
		public string Query { get; set; }
		public int Port { get; set; }
	}
}
