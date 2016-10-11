
using CES.CoreApi.Compliance.Alert.Business.Models.DTOs.Validators;
using FluentValidation.Attributes;

namespace CES.CoreApi.Compliance.Alert.Business.Models
{
	[Validator(typeof(ReviewAlertRequestValidator))]
	public class ReviewAlertRequest : BaseRequest
	{
		public string AlertId { get; set; }
		public int ServiceId { get; set; }
		public long TransactionId { get; set; }
		public int PartyId { get; set; }
		public string AlertStatusId { get; set; }
		public string AlertStatusName { get; set; }
		public string UserName { get; set; }
	}
}
