using PushSharp.Apple;
using CES.CoreApi.PushNotifications.Utilities;
using CES.CoreApi.PushNotifications.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace CES.CoreApi.PushNotifications.Providers
{
	public class ApnsProvider
	{

		public PushResponse Push(Notification message)
		{
			var response = new PushResponse { };
			System.AggregateException ex = null;
			StringBuilder error = null;
			var config = new ApnsConfiguration(AppSettings.ApnsEnvironment
				, AppSettings.ApnsCertificatePath, AppSettings.ApnsCertificatePassword);

			var broker = new ApnsServiceBroker(config);
			broker.OnNotificationFailed += (notification, exception) =>
			{
				error = new StringBuilder();
				foreach (var item in exception.InnerExceptions)
				{
					error.Append(item.InnerException.Message);
				}
				ex = exception;							
			};

			broker.OnNotificationSucceeded += (notification) =>
			{
				response.Message = "Message has been pushed to APN";
			};
			broker.Start();
			var payload = JsonConvert.SerializeObject(message);
			broker.QueueNotification(new ApnsNotification
			{
				DeviceToken = message.DeviceToken,			
				Payload = JObject.Parse(payload),
			});
			broker.Stop();
			if (ex != null)
				throw new System.AggregateException(error.ToString(), ex);

			return response;
		}
	}
}