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
			var result = PayoutService.GetPayoutOrderInfo(new OrderRequest
			{
				AgentId = 44729311,
				AgentLocId = 59347511,
				CountryTo = "ES",
				Locale = "en-US",
				OrderId = "",
				OrderPin = "00714644257",
				StateTo = "RM",
				UserLoginId = 1

			});


			Assert.AreEqual(true, result.Response.AvailableForPayout);

			Assert.AreEqual(1916206551, result.Transaction.OrderId);
		}
	}
}