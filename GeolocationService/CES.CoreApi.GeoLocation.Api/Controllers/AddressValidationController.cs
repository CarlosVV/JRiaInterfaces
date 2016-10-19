using CES.CoreApi.GeoLocation.Providers;
using CES.CoreApi.GeoLocation.Providers.MelissaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CES.CoreApi.GeoLocation.Api.Controllers
{

	[RoutePrefix("geolocation/v2")]
	public class AddressValidationController : ApiController
    {
		[HttpPost]
		[Route("address/validate")]
		public virtual IHttpActionResult DoValidateAddress(Models.Requests.AddressRequest request)
		{


		//	GoogleProvider p = new GoogleProvider();
			MelissaDataProvider p = new MelissaDataProvider();
			var x = p.DoValidation(request);
			return Content(HttpStatusCode.OK, x);
		}
	}
}
