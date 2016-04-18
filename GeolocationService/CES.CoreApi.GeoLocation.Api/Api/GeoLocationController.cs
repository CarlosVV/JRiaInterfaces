using CES.CoreApi.GeoLocation.Api.Models;
using CES.CoreApi.GeoLocation.Service.Contract.Models;
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

		[HttpGet]
		public string Ping()
		{
			return "OK";
		}

		
		[HttpPost]
		public ValidateAddressResponse ValidateAddress(ValidateAddressRequest request)
		{
			return null;
		}

	}
}
