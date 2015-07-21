using Microsoft.Practices.EnterpriseLibrary.Data;

namespace CES.CoreApi.Foundation.Data.Models
{
    public class ConnectionConfiguration
    {
        public string GroupName { get; set; }
        public string ServerName { get; set; }
        public string ConnectionString { get; set; }
        public bool IsDefault { get; set; }
        public int DatabaseId { get; set; }
        public Database Database { get; set; }
        public string ProviderName { get; set; }
    }
}