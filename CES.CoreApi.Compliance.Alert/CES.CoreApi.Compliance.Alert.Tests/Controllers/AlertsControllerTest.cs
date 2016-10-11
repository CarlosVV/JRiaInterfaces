
using CES.CoreApi.Compliance.Alert.Business.Interfaces;
using CES.CoreApi.Compliance.Alert.Business.Models;
using CES.CoreApi.Compliance.Alert.Tests.InitTests;
using CES.CoreApi.Compliance.Alert.Controllers;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit2;
using System.Web.Http.Results;
using Xunit;

namespace CES.CoreApi.Compliance.Alert.Tests
{
    public class AlertsControllerTest
    {
		[Theory]
		[AutoMoqData(typeof(WebApiCustomization))]
		public void AlertsController_ReturnsResultCodeProperly([Frozen] Mock<IAlertsService> stubAlertService, ReviewAlertResponse expectedResponse, ReviewAlertRequest reviewAlertRequest, AlertsController alertsController)
		{
			stubAlertService.Setup(s => s.ReviewIssueClear(reviewAlertRequest)).Returns(expectedResponse);

		//	

			var result = ((NegotiatedContentResult<ReviewAlertResponse>)(alertsController.ReviewAlert(reviewAlertRequest))).Content;

			Assert.Equal(expectedResponse.ResultCode, result.ResultCode);
		}

		[Theory]
		[AutoMoqData(typeof(WebApiCustomization))]
		public void AlertsController_ReturnsMessageProperly([Frozen] Mock<IAlertsService> stubAlertService, ReviewAlertResponse expectedResponse, ReviewAlertRequest reviewAlertRequest, AlertsController alertsController)
		{
			stubAlertService.Setup(s => s.ReviewIssueClear(reviewAlertRequest)).Returns(expectedResponse);

			var result = ((NegotiatedContentResult<ReviewAlertResponse>)(alertsController.ReviewAlert(reviewAlertRequest))).Content;

			Assert.Equal(expectedResponse.Message, result.Message);
		}
	}
}
