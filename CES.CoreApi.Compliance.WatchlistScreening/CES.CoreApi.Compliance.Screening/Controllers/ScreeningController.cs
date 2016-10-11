using CES.CoreApi.Compliance.Screening.Models;
using CES.CoreApi.Compliance.Screening.Providers;
using CES.CoreApi.Compliance.Screening.Repositories;
using CES.CoreApi.Compliance.Screening.Services;
using CES.CoreApi.Compliance.Screening.ViewModels;
using System.Net;
using System.Web.Http;

namespace CES.CoreApi.Compliance.Screening.Controllers
{
	[RoutePrefix("Compliance/Screening")]
    public class ScreeningController : ApiController
    {
        private ScreeningService _screeningService = null;
		/// <summary>
		/// Default contructor with optional screening Service to use
		/// 
		/// </summary>
		/// <param name="screeningService"></param>
		public ScreeningController()
		{
			_screeningService = new ScreeningService();

		}
		[HttpGet]
		[Route("Ping")]
		public IHttpActionResult GetPing()
		{
					return Content(HttpStatusCode.OK,
				$"Hello Compliance.Screening Service! {System.DateTime.UtcNow}");
		}
		[HttpGet]
		[Route("Actmize/RealTimeWsProvider/1")]
		public IHttpActionResult PingActmize([FromUri]string originatorName, string originatorCountryCd)
		{
			return Content(HttpStatusCode.OK,
				_screeningService.PingActmize(originatorName, originatorCountryCd));
		}

		[HttpPost]
        [Route("transactions")]
        public IHttpActionResult Transactions(ScreeningRequest screeningRequest)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var request = AutoMapper.Mapper.Map<Request>(screeningRequest);
            var response = _screeningService.ScreeningTransaction(request);
            var screeningResponse = AutoMapper.Mapper.Map<ScreeningResponse>(response);

            if(screeningResponse.Code=="60" )
                return Content(HttpStatusCode.BadGateway, screeningResponse);  // HTTP 502

            if (screeningResponse.Code == "61")
                return Content(HttpStatusCode.InternalServerError, screeningResponse);  // HTTP 500

            return Content(HttpStatusCode.Created, screeningResponse);  // HTTP 201
        }

       
    }
}
