using System.Web.Http;

namespace CES.CoreApi.GeoLocation.Api.Api
{
	[RoutePrefix("geolocation")]
	public class PingController : ApiController
    {
		[HttpGet]
		[Route("Ping")]
		public string Ping()
		{
			return "OK 2" + CES.CoreApi.Configuration.Provider.GeoLocationConfigurationSection.Instance.Google.Key;;
		}
	}
}
