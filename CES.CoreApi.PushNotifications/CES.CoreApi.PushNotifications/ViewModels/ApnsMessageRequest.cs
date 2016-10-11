using FluentValidation;
using FluentValidation.Attributes;

namespace CES.CoreApi.PushNotifications.ViewModels
{
	[Validator(typeof(ApnsMessageRequestValidator))]
	public class ApnsMessageRequest
	{
		public int AppId { get; set; }
		public string DeviceToken { get; set; }
		public string Message { get; set; }

		public object RequestId { get; set; }
	}
	class ApnsMessageRequestValidator : AbstractValidator<ApnsMessageRequest>
	{
		public ApnsMessageRequestValidator()
		{
			RuleFor(r => r.AppId).NotEmpty().WithMessage("AppId  is required");
			RuleFor(r => r.DeviceToken).NotEmpty().WithMessage("DeviceToken is required");
			RuleFor(r => r.Message).NotEmpty().WithMessage("Message  is required");
		}
	}
}