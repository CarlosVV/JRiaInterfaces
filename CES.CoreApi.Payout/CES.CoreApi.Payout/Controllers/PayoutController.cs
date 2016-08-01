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

		[HttpGet]
		[HttpPost]
		[Route("Transaction")]
		public object GetTransaction(TransactionInfoRequest request)
		{
			/**/
			var requestModel = Mapper.Map<OrderRequest>(request);
			var responseModel = PayoutService.GetPayoutOrderInfo(requestModel);
			/**/
			var response = Mapper.Map<TransactionInfoResponse>(responseModel);
			response.SenderInfo = Mapper.Map<Sender>(responseModel.Transaction);
			response.BeneficiaryInfo = Mapper.Map<Beneficiary>(responseModel.Transaction);
			return response;
		}

	}
}
