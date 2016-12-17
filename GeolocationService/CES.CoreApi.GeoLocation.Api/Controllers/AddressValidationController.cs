using CES.CoreApi.GeoLocation.Providers;
using CES.CoreApi.GeoLocation.Providers.Bing;
using CES.CoreApi.GeoLocation.Providers.Google;
using CES.CoreApi.GeoLocation.Providers.MelissaData;
using System.Net;
using System.Web.Http;

namespace CES.CoreApi.GeoLocation.Api.Controllers
{

	[RoutePrefix("geolocation/v2")]
	public class AddressValidationController : ApiController
	{
		[HttpPost]
		[Route("address/validate")]
		public virtual IHttpActionResult ValidateAddress(Models.Requests.AddressRequest request)
		{
			
			var provider = new GeoLocationFactory();
			var result = provider.Validation(request);
			return Content(HttpStatusCode.OK, result);
		}
	}
}
