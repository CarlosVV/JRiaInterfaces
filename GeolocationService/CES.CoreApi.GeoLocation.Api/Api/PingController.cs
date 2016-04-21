using System.Web.Http;

namespace CES.CoreApi.GeoLocation.Api.Api
{
	public class PingController : ApiController
    {
		[Route("Ping")]
		[HttpGet]
		public string Ping()
		{
			return "OK";
		}
	}
}
