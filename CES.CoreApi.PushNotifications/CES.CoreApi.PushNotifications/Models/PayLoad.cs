using Newtonsoft.Json;

namespace CES.CoreApi.PushNotifications.Models
{
	public class PayLoad
	{
		[JsonProperty("alert")]
		public string Alert { get; set; }
		[JsonProperty("sound")]
		public string Sound { get; set; }
		[JsonProperty("badge")]
		public int Badge { get; set; }
	}
}