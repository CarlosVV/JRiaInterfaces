using CES.CoreApi.Receipt_Main.Models;
using CES.CoreApi.Receipt_Main.Services;
using CES.CoreApi.Receipt_Main.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CES.CoreApi.Receipt_Main.Controllers
{
    
    [RoutePrefix("receipt/mt")]
    public class MTController : ApiController
    {
        private MTService _service;
        public MTController()
        {
            _service = new MTService();
        }
        [HttpGet]
        [Route("Ping")]
        public IHttpActionResult PingServer()
        {
            return Content(HttpStatusCode.OK, $"Hello CES.CoreApi.Receipt_Main MT Service! {System.DateTime.UtcNow}");
        }

        [HttpPost]
        [Route("generate/payout")]
        public IHttpActionResult PostPayout(ServiceMTGeneratePayoutRequestViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var taxGenerateRequest
                    = AutoMapper.Mapper.Map<MTGeneratePayoutRequest>(request);

            var response = _service.GeneratePayout(taxGenerateRequest);
            return Content(HttpStatusCode.OK, response);
        }


        [HttpPost]
        [Route("generate/ordersend")]
        public IHttpActionResult PostOrderSend(ServiceMTGenerateOrderSendRequestViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mgGenerateOrderSendRequest
                    = AutoMapper.Mapper.Map<MTGenerateOrderSendRequest>(request);

            var response = _service.GenerateOrderSend(mgGenerateOrderSendRequest);
            return Content(HttpStatusCode.OK, response);
        }


        [HttpPost]
        [Route("generate/compliance")]
        public IHttpActionResult PostCompliance(ServiceMTGenerateComplianceRequestViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mtGenerateComplianceRequest
                    = AutoMapper.Mapper.Map<MTGenerateComplianceRequest>(request);

            var response = _service.GenerateCompliance(mtGenerateComplianceRequest);
            return Content(HttpStatusCode.OK, response);
        }
    }
}
