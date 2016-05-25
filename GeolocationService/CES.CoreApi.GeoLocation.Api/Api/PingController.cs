using System.Web.Http;

namespace CES.CoreApi.GeoLocation.Api.Api
{
	[RoutePrefix("geolocation")]
	public class PingController : ApiController
    {
		[HttpGet]
		[Route("ping")]
		public string Ping()
		{
			return "OK";
		}
	}
}
