using Xunit;
using Moq;
using CES.CoreApi.Compliance.Alert.Business.Models;
using CES.CoreApi.Compliance.Alert.Business.Interfaces;
using Ploeh.AutoFixture.Xunit2;

namespace CES.CoreApi.Compliance.Alert.Business.Tests
{
	public class AlertsServiceTest
	{
		[Theory]
		[AutoMoqData]
		public void ReviewIssueClear_ProperObjectIsReturn(ReviewAlertRequest reviewAlertRequest, ReviewAlertResponse expectedAlertResponse, [Frozen] Mock<IAlertsRepository> alertsRepository, AlertsService alertsService)
		{
			alertsRepository.Setup(s => s.ReviewIssueClear(reviewAlertRequest)).Returns(expectedAlertResponse);

			var result = alertsService.ReviewIssueClear(reviewAlertRequest);

			Assert.Equal(expectedAlertResponse, result);
		}
	}
}
