using AutoMapper;
using CES.CoreApi.Payout.Service.Business.Contract.Interfaces;
using CES.CoreApi.Payout.Service.Business.Contract.Models;
using CES.CoreApi.Payout.Models;
using System.Web.Http;
using CES.CoreApi.Payout.Service.Contract.Interfaces;
using System;
using System.Net;

namespace CES.CoreApi.Payout.Controllers
{

	[RoutePrefix("moneytransfer")]
	public class PayoutController : ApiController
	{
		private readonly IRequestValidator _validator;
		private readonly IPayoutProcessor _processor;
		private readonly IMapper _mappingHelper;


		/// <summary>
		/// CONSTRUCTOR:
		/// </summary>
		/// <param name="validator"></param>
		/// <param name="processor"></param>
		/// <param name="mappingHelper"></param>
		public PayoutController(IRequestValidator validator, IPayoutProcessor processor, IMapper mappingHelper)
		{
			//Check incoming parameters:
			if (validator == null) { throw new ArgumentNullException("validator"); }
			if (processor == null) { throw new ArgumentNullException("processor"); }
			if (mappingHelper == null) { throw new ArgumentNullException("mappingHelper"); }
			//Assign params to instance vars.
			_validator = validator;
			_processor = processor;
			_mappingHelper = mappingHelper;
		}

		/// <summary>
		/// Process a GetTransactionInfo Call:
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("payout/getTransactionInfo")]
		[Route("payout/v1/TransactionInfo")]
		[Route("payout/TransactionInfo")]
		public IHttpActionResult GetTransactionInfo(GetTransactionInfoRequest request)
		{
			GetTransactionInfoResponse response = null;

			//Validate incoming request parameters:
			Logging.Log.Info("Validating GetTransactionInfo request...");
			response = _validator.Validate(request);
			if (!response.IsValid)
			{
				return Content(HttpStatusCode.BadRequest, response);
			}

			Logging.Log.Info("for PIN: " + request.OrderPIN + ". AgentID: " + request.RequesterInfo.AgentID);
		
			//Convert request to internal version:
			GetTransactionInfoRequestModel internalRequest =
				_mappingHelper.Map<GetTransactionInfoRequest, GetTransactionInfoRequestModel>(request);

			//Make the call:
			Logging.Log.Info("Processing call...");
			GetTransactionInfoResponseModel responseModel = _processor.GetTransactionInfo(internalRequest);
			Logging.Log.Info("Processed Successfully.");
			response = _mappingHelper.Map<GetTransactionInfoResponseModel, GetTransactionInfoResponse>(responseModel);
			Logging.Log.Info("Returning Response.");
			return Content(HttpStatusCode.OK, response);

		}

		/// <summary>
		/// Process a PayoutTransaction Call:
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("payout/payoutTransaction")]
		public IHttpActionResult PayoutTransaction(PayoutTransactionRequest request)
		{
			PayoutTransactionResponse response = null;

			//Validate incoming request parameters:
			Logging.Log.Info("Validating PayoutTransaction request...");
			response = _validator.Validate(request);
			if (!response.IsValid)
			{
				return Content(HttpStatusCode.BadRequest, response);
			}

			Logging.Log.Info("for PIN: " + request.OrderPIN + ". AgentID: " + request.RequesterInfo.AgentID);

			//Convert request to internal version:
			PayoutTransactionRequestModel internalRequest =
				_mappingHelper.Map<PayoutTransactionRequest, PayoutTransactionRequestModel>(request);

			//Make the call:
			Logging.Log.Info("Processing call...");
			PayoutTransactionResponseModel responseModel = _processor.PayoutTransaction(internalRequest);
			Logging.Log.Info("Processed Successfully.");


			try
			{
				response = _mappingHelper.Map<PayoutTransactionResponseModel, PayoutTransactionResponse>(responseModel);

			}
			catch (Exception ex)
			{
				var newRresponseModel = new PayoutTransactionResponseModel() { PersistenceID = -1, ReturnInfo = new ReturnInfoModel() { ErrorMessage = ex.Message } };
				response = _mappingHelper.Map<PayoutTransactionResponseModel, PayoutTransactionResponse>(newRresponseModel);
			}
			Logging.Log.Info("Returning Response.");
			return Content(HttpStatusCode.OK, response);

		}

	}
}
