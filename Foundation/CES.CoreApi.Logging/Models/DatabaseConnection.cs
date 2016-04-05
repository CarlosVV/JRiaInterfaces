using System.Runtime.Serialization;

namespace CES.CoreApi.Logging.Models
{
	public class DatabaseConnection
	{
		public string DatabaseName { get; set; }

		public string ServerName { get; set; }

		public string ConnectionString { get; set; }

		public int ConnectionTimeout { get; set; }

		public string ServerVersion { get; set; }

		public long OpenConnectionTime { get; set; }
	}
}
