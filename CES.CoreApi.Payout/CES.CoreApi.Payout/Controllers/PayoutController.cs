using AutoMapper;
using CES.CoreApi.Payout.Models;
using CES.CoreApi.Payout.Services;
using CES.CoreApi.Payout.ViewModels;
using System.Net;
using System.Web.Http;

namespace CES.CoreApi.Payout.Controllers
{

	[RoutePrefix("MoneyTransfer")]
	public class PayoutController : ApiController
	{
		private PayoutService _payoutService;

		public PayoutController()
		{
			_payoutService = new PayoutService();
		}
				
		[HttpPost]
		[Route("Payout/Transaction")]
		public IHttpActionResult GetTransaction(TransactionInfoRequest request)
		{
			
			/*Request Mapper*/
			var requestModel = Mapper.Map<PayoutOrderRequest>(request);

			var responseModel = _payoutService.GetTransactionInfo(requestModel);
			/*Response Mapper*/
			var response = Mapper.Map<TransactionInfoResponse>(responseModel);
			response.SenderInfo = Mapper.Map<Sender>(responseModel.Transaction);
			response.BeneficiaryInfo = Mapper.Map<Beneficiary>(responseModel.Transaction);
			return  Content(HttpStatusCode.OK, response);
		}

	}
}
