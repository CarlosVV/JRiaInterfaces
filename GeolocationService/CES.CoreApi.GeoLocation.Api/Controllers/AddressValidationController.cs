using CES.CoreApi.GeoLocation.Providers;
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
			//MelissaData = 1,     
			IGeoLocationProvider provider = null;
			if (request.ProviderId == 1)
			{
				provider = new MelissaDataProvider();
			}	
			else if (request.ProviderId == 2)
			{//   Bing = 2,

			}
			else
			{
				provider = new GoogleProvider();
			}

			var result = provider.Validation(request);
			return Content(HttpStatusCode.OK, result);
		}
	}
}
