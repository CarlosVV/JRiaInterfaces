using CES.CoreApi.WebApi.Models;
using CES.CoreApi.WebApi.Services;
using CES.CoreApi.WebApi.ViewModels;
using System.Net;
using System.Web.Http;

namespace CES.CoreApi.WebApi.Controllers
{
	/// <summary>
	/// This is a sample api Controller  class need to be removed or replaced.   
	/// </summary>
	[RoutePrefix("Service")]
	public class SampleController : ApiController
	{
		private SampleService _service;
		public SampleController()
		{
			_service = new SampleService();
		}
		[HttpGet]
		[Route("Ping")]
		public IHttpActionResult PingServer()
		{			
			return Content(HttpStatusCode.OK, $"Hello CES.CoreApi.WebApi Service! {System.DateTime.UtcNow}");
		}
		[HttpGet]
		[Route("OfferCurrency")]
		public IHttpActionResult GetCurrenyCountry([FromUri] ServiceOfferCurrencyRequest request)
		{
			if (!ModelState.IsValid) {			
				return BadRequest( ModelState);
			}

			var countryCurrencyRequest
					= AutoMapper.Mapper.Map<CountryCurrencyRequest>(request);

			var response = _service.GetServiceOfferdCurrencies(countryCurrencyRequest);
			return Content(HttpStatusCode.OK, response);
		}
		
	}
}
