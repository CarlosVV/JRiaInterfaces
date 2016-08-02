using Microsoft.VisualStudio.TestTools.UnitTesting;

using CES.CoreApi.Payout.ViewModels;
namespace CES.CoreApi.Payout.Services.Tests
{
	[TestClass()]
	public class PayoutServiceTests
	{
		[TestMethod()]
		public void GetPayoutOrderInfo_IsAvailable()
		{
			var result = PayoutService.GetTransactionInfo(new PayoutOrderRequest
			{
				AgentId = 23392811,
				AgentLocId = 25055911,
				CountryTo = "CL",
				Locale = "en-US",
				OrderId = "",
				OrderPin = "CL1916560955",
				StateTo = "RM",
				UserLoginId = 1

			});
			Assert.AreEqual(true, result.Response.AvailableForPayout);		
		}
	}
}