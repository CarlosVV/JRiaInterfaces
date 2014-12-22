using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace CES.CoreApi.Logging.Models
{
    [DataContract]
    public class DatabaseConnection
    {
        /// <summary>
        /// Gets or sets database name
        /// </summary>
        [DataMember]
        public string DatabaseName { get; set; }

        /// <summary>
        /// Gets or sets database server name
        /// </summary>
        [DataMember]
        public string ServerName { get; set; }

        /// <summary>
        /// Gets or sets database connection string
        /// </summary>
        [DataMember]
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets database connection timeout
        /// </summary>
        [DataMember]
        public int ConnectionTimeout { get; set; }

        /// <summary>
        /// Gets or sets database server version
        /// </summary>
        [DataMember]
        public string ServerVersion { get; set; }
    }
}
