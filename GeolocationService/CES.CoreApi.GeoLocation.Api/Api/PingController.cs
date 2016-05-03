using System.Web.Http;

namespace CES.CoreApi.GeoLocation.Api.Api
{
	public class PingController : ApiController
    {
		[HttpGet]
		public string Ping()
		{
			return "OK";
		}
	}
}
