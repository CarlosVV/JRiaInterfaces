using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CES.CoreApi.Logging.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class DatabaseParameter
    {
         [JsonProperty]
        public string Name { get; set; }

         [JsonProperty]
        public object Value { get; set; }

         [JsonProperty]
         [JsonConverter(typeof(StringEnumConverter))]
        public ParameterDirection Direction { get; set; }

         [JsonProperty]
         [JsonConverter(typeof(StringEnumConverter))]
        public DbType DataType { get; set; }
    }
}
