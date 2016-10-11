using System.Web.Http;

namespace CES.CoreApi.GeoLocation.Api
{
	[RoutePrefix("geolocation")]
	public class PingController : ApiController
    {
		[HttpGet]
		[Route("Ping")]
		public string Ping()
		{
			return "OK";
		}

		[HttpGet]
		[Route("ClearCache")]
		public IHttpActionResult ResetCache([FromUri] string key)
		{
			//Cache
			Caching.Cache.Remove(key);
			return Ok();
		}
	}

}
