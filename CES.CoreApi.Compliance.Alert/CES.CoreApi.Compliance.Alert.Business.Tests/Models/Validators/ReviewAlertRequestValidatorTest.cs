using System;
using CES.CoreApi.Compliance.Alert.Business.Models.DTOs.Validators;
using Xunit;
using Moq;
using FluentValidation;
using CES.CoreApi.Compliance.Alert.Business.Models;

namespace CES.CoreApi.Compliance.Alert.Business.Tests
{
	
	public class ReviewAlertRequestValidatorTest
	{
		[Theory]
		[AutoMoqData]
		public void ReviewAlertRequestValidator_AllPropertiesAreCorrect_ReturnsSuccess(ReviewAlertRequest reviewAlertRequest, ReviewAlertRequestValidator reviewAlertRequestValidator)
		{
			Assert.True(reviewAlertRequestValidator.Validate(reviewAlertRequest).IsValid);
		}
		
		[Theory]
		[AutoMoqData]
		public void ReviewAlertRequestValidator_WhenRequestDateTimeIsMinDate_ReturnError(ReviewAlertRequest reviewAlertRequest, ReviewAlertRequestValidator reviewAlertRequestValidator)
		{
			reviewAlertRequest.RequestDateTime = DateTime.MinValue;

			Assert.False(reviewAlertRequestValidator.Validate(reviewAlertRequest).IsValid);
		}
		
		[Theory]
		[AutoMoqData]
		public void ReviewAlertRequestValidator_WhenAlertIdIsNull_ReturnError(ReviewAlertRequest reviewAlertRequest, ReviewAlertRequestValidator reviewAlertRequestValidator)
		{
			reviewAlertRequest.AlertId = null;

			Assert.False(reviewAlertRequestValidator.Validate(reviewAlertRequest).IsValid);
		}

		[Theory]
		[AutoMoqData]
		public void ReviewAlertRequestValidator_WhenAlertIdIsEmpty_ReturnError(ReviewAlertRequest reviewAlertRequest, ReviewAlertRequestValidator reviewAlertRequestValidator)
		{
			reviewAlertRequest.AlertId = string.Empty;

			Assert.False(reviewAlertRequestValidator.Validate(reviewAlertRequest).IsValid);
		}

		[Theory]
		[AutoMoqData]
		public void ReviewAlertRequestValidator_WhenServiceIdIsZero_ReturnError(ReviewAlertRequest reviewAlertRequest, ReviewAlertRequestValidator reviewAlertRequestValidator)
		{
			reviewAlertRequest.ServiceId = 0;

			Assert.False(reviewAlertRequestValidator.Validate(reviewAlertRequest).IsValid);
		}

		[Theory]
		[AutoMoqData]
		public void ReviewAlertRequestValidator_WhenTransactionIdIsZeroAndPartyIdIsZero_ReturnError(ReviewAlertRequest reviewAlertRequest, ReviewAlertRequestValidator reviewAlertRequestValidator)
		{
			reviewAlertRequest.TransactionId = 0;
			reviewAlertRequest.PartyId = 0;

			Assert.False(reviewAlertRequestValidator.Validate(reviewAlertRequest).IsValid);
		}

		[Theory]
		[AutoMoqData]
		public void ReviewAlertRequestValidator_WhenTransactionIdIsGreaterThanZeroAndPartyIdIsZero_ReturnSuccess(ReviewAlertRequest reviewAlertRequest, ReviewAlertRequestValidator reviewAlertRequestValidator)
		{
			reviewAlertRequest.PartyId = 0;

			Assert.True(reviewAlertRequestValidator.Validate(reviewAlertRequest).IsValid);
		}

		[Theory]
		[AutoMoqData]
		public void ReviewAlertRequestValidator_WhenPartyIdIsGreaterThanZeroAndTransactionIdIsZero_ReturnSuccess(ReviewAlertRequest reviewAlertRequest, ReviewAlertRequestValidator reviewAlertRequestValidator)
		{
			reviewAlertRequest.TransactionId = 0;

			Assert.True(reviewAlertRequestValidator.Validate(reviewAlertRequest).IsValid);
		}

		[Theory]
		[AutoMoqData]
		public void ReviewAlertRequestValidator_WhenAlertStatusIdIsNull_ReturnError(ReviewAlertRequest reviewAlertRequest, ReviewAlertRequestValidator reviewAlertRequestValidator)
		{
			reviewAlertRequest.AlertStatusId = null;
			
			Assert.False(reviewAlertRequestValidator.Validate(reviewAlertRequest).IsValid);
		}

		[Theory]
		[AutoMoqData]
		public void ReviewAlertRequestValidator_WhenAlertStatusIdIsEmpty_ReturnError(ReviewAlertRequest reviewAlertRequest, ReviewAlertRequestValidator reviewAlertRequestValidator)
		{
			reviewAlertRequest.AlertStatusId = string.Empty;

			Assert.False(reviewAlertRequestValidator.Validate(reviewAlertRequest).IsValid);
		}
		
		[Theory]
		[AutoMoqData]
		public void ReviewAlertRequestValidator_WhenAlertStatusNameIsNull_ReturnError(ReviewAlertRequest reviewAlertRequest, ReviewAlertRequestValidator reviewAlertRequestValidator)
		{
			reviewAlertRequest.AlertStatusName = null;

			Assert.False(reviewAlertRequestValidator.Validate(reviewAlertRequest).IsValid);
		}

		[Theory]
		[AutoMoqData]
		public void ReviewAlertRequestValidator_WhenAlertStatusNameIsEmpty_ReturnError(ReviewAlertRequest reviewAlertRequest, ReviewAlertRequestValidator reviewAlertRequestValidator)
		{
			reviewAlertRequest.AlertStatusName = string.Empty;

			Assert.False(reviewAlertRequestValidator.Validate(reviewAlertRequest).IsValid);
		}
		
		[Theory]
		[AutoMoqData]
		public void ReviewAlertRequestValidator_WhenUserNameIsNull_ReturnError(ReviewAlertRequest reviewAlertRequest, ReviewAlertRequestValidator reviewAlertRequestValidator)
		{
			reviewAlertRequest.UserName = null;

			Assert.False(reviewAlertRequestValidator.Validate(reviewAlertRequest).IsValid);
		}

		[Theory]
		[AutoMoqData]
		public void ReviewAlertRequestValidator_WhenUserNameIsEmpty_ReturnError(ReviewAlertRequest reviewAlertRequest, ReviewAlertRequestValidator reviewAlertRequestValidator)
		{
			reviewAlertRequest.UserName = string.Empty;

			Assert.False(reviewAlertRequestValidator.Validate(reviewAlertRequest).IsValid);
		}
	}
}
