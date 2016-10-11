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
    public class RulesController : ApiController
    {
        private RulesService _ruleService = null;

        public RulesController()
        {
            _ruleService = new RulesService();
        }

        [HttpGet]
        [Route("rules")]
        public IHttpActionResult GetRules()
        {
            return Content(HttpStatusCode.OK,
        $"Hello Compliance.Screening Service! {System.DateTime.UtcNow}");
        }

        [HttpPost]
        private IHttpActionResult GetRules(GetRulesRequest rulesRequest)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var request = new RulesRequest();
           // var request = AutoMapper.Mapper.Map<Request>(screeningRequest);
            var response = _ruleService.GetRules(request);
            //var screeningResponse = AutoMapper.Mapper.Map<ScreeningResponse>(response);

            var rulesResponse = new GetRulesResponse();
            return Content(HttpStatusCode.OK, rulesResponse);  // HTTP 200
        }
    }
}
