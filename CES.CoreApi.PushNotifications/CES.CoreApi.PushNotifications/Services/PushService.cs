using CES.CoreApi.PushNotifications.Models;
using CES.CoreApi.PushNotifications.Providers;
using CES.CoreApi.PushNotifications.ViewModels;

namespace CES.CoreApi.PushNotifications.Services
{

	public class PushService
	{
		private  ApnsProvider _provider;		


		public PushResponse Push(ApnsMessageRequest  request, object requestId)
		{
			_provider = new ApnsProvider();
			var result = _provider.Push(new Notification
			{
				DeviceToken = request.DeviceToken,
				
				PayLoad = new PayLoad { Alert = request.Message, Badge = 10, Sound = "bingbong.aiff" }

			});
			result.RequestId = request.RequestId;
			result.ResponseId = requestId;
			return result;
		}
	}
}