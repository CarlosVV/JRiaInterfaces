using AutoMapper;
using CES.CoreApi.Payout.Models;
using CES.CoreApi.Payout.Services;
using CES.CoreApi.Payout.ViewModels;
using System.Web.Http;

namespace CES.CoreApi.Payout.Controllers
{

	[RoutePrefix("moneytransfer/payout")]
	public class PayoutController : ApiController
	{
		private PayoutService _payoutService = null;

		public PayoutController()
		{
			_payoutService = new PayoutService();
		}

		//[HttpGet]
		[HttpPost]
		[Route("Transaction")]
		public TransactionInfoResponse GetTransaction(TransactionInfoRequest request)
		{
			/*Request Mapper*/
			var requestModel = Mapper.Map<PayoutOrderRequest>(request);

			var responseModel = _payoutService.GetTransactionInfo(requestModel);
			/*Response Mapper*/
			var response = Mapper.Map<TransactionInfoResponse>(responseModel);
			response.SenderInfo = Mapper.Map<Sender>(responseModel.Transaction);
			response.BeneficiaryInfo = Mapper.Map<Beneficiary>(responseModel.Transaction);
			return response;
		}
	}
}
