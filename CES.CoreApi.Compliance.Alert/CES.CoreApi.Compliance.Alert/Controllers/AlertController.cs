using CES.CoreApi.Compliance.Alert.Business.Interfaces;
using CES.CoreApi.Compliance.Alert.Business.Models;
using System.Net;
using System.Web.Http;

namespace CES.CoreApi.Compliance.Alert.Controllers
{
	[RoutePrefix("compliance")]
	public class AlertsController : ApiController
	{
		private IAlertsService _alertsServices;

		public AlertsController(IAlertsService alertsService)
		{
			_alertsServices = alertsService;
		}

		[HttpPut]
		[Route("Alerts")]
		public IHttpActionResult ReviewAlert([FromBody] ReviewAlertRequest request)
		{
			return  Content(HttpStatusCode.OK, _alertsServices.ReviewIssueClear(request));
		}
	}
}
