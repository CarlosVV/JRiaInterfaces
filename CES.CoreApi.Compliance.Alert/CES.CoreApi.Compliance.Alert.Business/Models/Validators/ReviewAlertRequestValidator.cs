using CES.CoreApi.Compliance.Alert.Business.Resources;
using FluentValidation;
using System;

namespace CES.CoreApi.Compliance.Alert.Business.Models.DTOs.Validators
{
	public class ReviewAlertRequestValidator : AbstractValidator<ReviewAlertRequest>
	{
		public ReviewAlertRequestValidator()
		{
			RuleFor(x => x.RequestDateTime).GreaterThan(DateTime.MinValue).WithMessage(AlertsResources.RequiredFieldMsg);
			RuleFor(x => x.AlertId).NotEmpty().WithMessage(AlertsResources.RequiredFieldMsg);
			RuleFor(x => x.ServiceId).GreaterThan(0).WithMessage(AlertsResources.RequiredFieldMsg);
			RuleFor(x => x.TransactionId).Must(x => x > 0).When(y => y.PartyId == 0).WithMessage(AlertsResources.AtLeastTransactionOrPartyRequiredMsg);
			RuleFor(x => x.PartyId).Must(x => x > 0).When(y => y.TransactionId == 0).WithMessage(AlertsResources.AtLeastTransactionOrPartyRequiredMsg);
			RuleFor(x => x.AlertStatusId).NotEmpty().WithMessage(AlertsResources.RequiredFieldMsg);
			RuleFor(x => x.AlertStatusName).NotEmpty().WithMessage(AlertsResources.RequiredFieldMsg);
			RuleFor(x => x.UserName).NotEmpty().WithMessage(AlertsResources.RequiredFieldMsg);
		}
	}
}
