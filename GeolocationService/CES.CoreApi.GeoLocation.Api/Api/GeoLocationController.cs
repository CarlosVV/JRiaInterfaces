using CES.CoreApi.GeoLocation.Api.Models;
using System.Web.Http;

namespace CES.CoreApi.GeoLocation.Api.Api
{
	public class GeoLocationController : ApiController
    {
		
		private readonly IUserRepository repository;
		public GeoLocationController(IUserRepository repository)
		{
			this.repository = repository;
		}

		[Route("Ping")]
		[HttpGet]
		public string Ping()
		{
			return "OK";
		}


	}
}
