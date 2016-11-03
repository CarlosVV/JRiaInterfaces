using CES.CoreApi.Payout.Service.Business.Logic.Services;
using System.Net;
using System.Web.Http;

namespace CES.CoreApi.Payout.Api.Controllers
{
	[RoutePrefix("moneytransfer")]
	public class PinTrafficController : ApiController
	{
		[HttpGet]
		[Route("payout/v1/Config/Settings")]
		public IHttpActionResult GetSettings()
		{
			var service = new ConfigurationService();

			return Content(HttpStatusCode.OK, service.GetSettings());
		}
		[HttpGet]
		[Route("payout/v1/pin/provider")]
		public IHttpActionResult GetPinProvider(string pin)
		{
			var service = new ConfigurationService();

			return Content(HttpStatusCode.OK, service.GetProvider(pin));
		}
	}
}
