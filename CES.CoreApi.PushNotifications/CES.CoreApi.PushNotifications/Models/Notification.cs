using Newtonsoft.Json;

namespace CES.CoreApi.PushNotifications.Models
{
	public class Notification
	{	
		
		[JsonProperty("aps")]
		public PayLoad PayLoad { get; set; }
		[JsonIgnore]
		public string DeviceToken { get; set; }
	}
}