using System.Data;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CES.CoreApi.Logging.Models
{
    [DataContract]
    public class DatabaseParameter
    {
         [DataMember]
        public string Name { get; set; }

         [DataMember]
        public object Value { get; set; }

         [DataMember]
         [JsonConverter(typeof(StringEnumConverter))]
        public ParameterDirection Direction { get; set; }

         [DataMember]
         [JsonConverter(typeof(StringEnumConverter))]
        public DbType DataType { get; set; }
    }
}
