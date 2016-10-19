using CES.CoreApi.Compliance.Screening.Models;
using CES.CoreApi.Compliance.Screening.Providers;
using CES.CoreApi.Compliance.Screening.Repositories;
using CES.CoreApi.Compliance.Screening.Services;
using CES.CoreApi.Compliance.Screening.ViewModels;
using System;
using System.Net;
using System.Web.Http;
using CES.CoreApi.Compliance.Screening.Utilities;

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

        //[HttpGet]
        //[Route("Rules")]
        //public IHttpActionResult Rules(DateTime transDateTime
        //, int runtimeID
        //, ServiceIdType serviceId
        //, int productId
        //, int countryFromId
        //, int countryToId
        //, int receivingAgentID
        //, int receivingAgentLocID
        //, int payAgentID
        //, int payAgentLocID
        //, DeliveryMethod deliveryMethod
        //, string entryType
        //, string sendCurrency
        //, double sendAmount
        //, double sendTotalAmount
        //, PartyType partyType)
        //{
        //    var rulesRequest = new GetRulesRequest()
        //    {
        //        TransDateTime = transDateTime,
        //        RuntimeID = runtimeID,
        //        ServiceId = serviceId,
        //        ProductId = productId,
        //        CountryFromId = countryFromId,
        //        CountryToId = countryToId,
        //        ReceivingAgentID = receivingAgentID,
        //        ReceivingAgentLocID = receivingAgentLocID,
        //        PayAgentID = payAgentID,
        //        PayAgentLocID = payAgentID,
        //        DeliveryMethod = deliveryMethod,
        //        EntryType = entryType,
        //        SendCurrency = sendCurrency,
        //        SendAmount = sendAmount,
        //        SendTotalAmount = sendTotalAmount,
        //        PartyType = partyType
        //    };
        //    var validator = new GetRulesRequestValidator();
        //    var model = validator.Validate(rulesRequest); //FluentValidation validation

        //    if (!model.IsValid)
        //    {
        //        ModelState.AddModelErrors(model);
        //        return BadRequest(ModelState);
        //    }


        //    var request = AutoMapper.Mapper.Map<RulesRequest>(rulesRequest);
        //    var response = _ruleService.GetRules(request);
        //    var rulesResponse = AutoMapper.Mapper.Map<RulesResponse>(response);

        //    return Content(HttpStatusCode.OK, rulesResponse);  // HTTP 200
        //}

        [HttpGet]
        [Route("Rules")]
        public IHttpActionResult GetRules(DateTime transDateTime = default(DateTime)
            , int runtimeID = 0
            , ServiceIdType serviceId = ServiceIdType.Undefined
            , int productId = 0
            , int countryFromId = 0
            , int countryToId = 0
            , int receivingAgentID = 0
            , int receivingAgentLocID = 0
            , int payAgentID = 0
            , int payAgentLocID = 0
            , DeliveryMethod deliveryMethod = DeliveryMethod.Undefined
            , string entryType = ""
            , string sendCurrency = ""
            , double sendAmount = 0
            , double sendTotalAmount = 0
            , PartyType partyType = PartyType.Undefined)
        {
            var rulesRequest = new GetRulesRequest()
            {
                TransDateTime = transDateTime,
                RuntimeID = runtimeID,
                ServiceId = serviceId,                
                ProductId = productId,
                CountryFromId = countryFromId,
                CountryToId = countryToId,
                ReceivingAgentID = receivingAgentID,
                ReceivingAgentLocID = receivingAgentLocID,
                PayAgentID = payAgentID,
                PayAgentLocID = payAgentLocID,
                DeliveryMethod = deliveryMethod,
                EntryType = entryType,
                SendCurrency = sendCurrency,
                SendAmount = sendAmount,
                SendTotalAmount = sendTotalAmount,
                PartyType = partyType,

            };

            var validator = new GetRulesRequestValidator();
            var model = validator.Validate(rulesRequest); //FluentValidation validation

            if (!model.IsValid)
            {
                ModelState.AddModelErrors(model);
                return BadRequest(ModelState);
            }


            var request = AutoMapper.Mapper.Map<RulesRequest>(rulesRequest);
            var response = _ruleService.GetRules(request);
            var rulesResponse = AutoMapper.Mapper.Map<RulesResponse>(response);

            return Content(HttpStatusCode.OK, rulesResponse);  // HTTP 200
        }

        [HttpPost]
        [Route("Rules")]
        private IHttpActionResult Rules()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Content(HttpStatusCode.NotImplemented, new object());  // HTTP 200
        }
    }
}
