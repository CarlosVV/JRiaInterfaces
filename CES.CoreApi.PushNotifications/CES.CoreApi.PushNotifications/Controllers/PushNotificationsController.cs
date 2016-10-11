using CES.CoreApi.PushNotifications.Services;
using CES.CoreApi.PushNotifications.ViewModels;
using System;
using System.Net;
using System.Web.Http;

namespace CES.CoreApi.PushNotifications.Controllers
{
	/// <summary>
	/// This is a sample api Controller  class need to be removed or replaced.   
	/// </summary>
	[RoutePrefix("PushNotifications")]
	public class PushNotificationsController : ApiController
	{
		private PushService _service;
		public PushNotificationsController()
		{
			_service = new PushService();
		}
		[HttpGet]
		[Route("Ping")]
		public IHttpActionResult PingServer()
		{	
			return  Content(HttpStatusCode.OK, $"Hello PushNotifications Service! {DateTime.UtcNow}");
		}

		[HttpPost]
		[Route("Apple")]
		public IHttpActionResult ApnsPushMessage(ApnsMessageRequest request)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var response = _service.Push(request, Request.Properties["MS_RequestId"]);
			return Content(HttpStatusCode.OK, response);
		}
	}
}
