using Xunit;
using CES.CoreApi.Compliance.Alert.Business.Models;
using CES.CoreApi.Compliance.Alert.Business.Repositories;

using AutoMapper;

namespace CES.CoreApi.Compliance.Alert.Business.Tests
{
	public class AlertsRepositoryTest
	{
		public AlertsRepositoryTest()
		{
			Mapper.Initialize(cfg =>
			{
				cfg.AddProfile(new AlertsMapperProfile());
			});
		}

		[Theory]
		[AutoMoqData]
		public void ReviewIssueClear_WhenUserNotFound_Returns20AsResultCode(ReviewAlertRequest reviewAlertRequest, AlertsRepository alertsRepository)
		{
			reviewAlertRequest.UserName = "userNameNotFound";
			var expectedRetVal = 20;

			var result = alertsRepository.ReviewIssueClear(reviewAlertRequest) as ReviewAlertResponse;

			Assert.Equal(expectedRetVal, result.ResultCode);
		}
		
		[Theory]
		[AutoMoqData]
		public void ReviewIssueClear_WhenAlertStatusCodeNotFound_Returns21AsResultCode(ReviewAlertRequest reviewAlertRequest, AlertsRepository alertsRepository)
		{
			reviewAlertRequest.UserName = @"RIAENVIACA\jblanca";
			reviewAlertRequest.AlertStatusId = "123456789";
			var expectedRetVal = 21;

			var result = alertsRepository.ReviewIssueClear(reviewAlertRequest) as ReviewAlertResponse;

			Assert.Equal(expectedRetVal, result.ResultCode);
		}

		[Theory]
		[AutoMoqData]
		public void ReviewIssueClear_WhenAlertNotFound_Returns22AsResultCode(ReviewAlertRequest reviewAlertRequest, AlertsRepository alertsRepository)
		{
			reviewAlertRequest.UserName = @"RIAENVIACA\jblanca";
			reviewAlertRequest.AlertStatusId = "300";
			reviewAlertRequest.AlertId = "alertNotFound";
			var expectedRetVal = 22;

			var result = alertsRepository.ReviewIssueClear(reviewAlertRequest) as ReviewAlertResponse;

			Assert.Equal(expectedRetVal, result.ResultCode);
		}
	}
}
