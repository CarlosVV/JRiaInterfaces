using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CES.CoreApi.Logging.Models
{
	public class DatabaseParameter
	{

		public string Name { get; set; }


		public object Value { get; set; }


		[JsonConverter(typeof(StringEnumConverter))]
		public ParameterDirection Direction { get; set; }


		[JsonConverter(typeof(StringEnumConverter))]
		public DbType DataType { get; set; }
	}
}
