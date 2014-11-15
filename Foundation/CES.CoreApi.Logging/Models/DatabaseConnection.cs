using Newtonsoft.Json;

namespace CES.CoreApi.Logging.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class DatabaseConnection
    {
        /// <summary>
        /// Gets or sets database name
        /// </summary>
        [JsonProperty]
        public string DatabaseName { get; set; }

        /// <summary>
        /// Gets or sets database server name
        /// </summary>
        [JsonProperty]
        public string ServerName { get; set; }

        /// <summary>
        /// Gets or sets database connection string
        /// </summary>
        [JsonProperty]
        public string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets database connection timeout
        /// </summary>
        [JsonProperty]
        public int ConnectionTimeout { get; set; }

        /// <summary>
        /// Gets or sets database server version
        /// </summary>
        [JsonProperty]
        public string ServerVersion { get; set; }
    }
}
