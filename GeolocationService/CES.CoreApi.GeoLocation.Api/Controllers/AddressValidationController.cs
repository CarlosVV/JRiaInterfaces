using CES.CoreApi.GeoLocation.Providers;
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
			IGeoLocationProvider provider = new GoogleProvider();		
			var result = provider.Validation(request);
			return Content(HttpStatusCode.OK, result);
		}
	}
}
