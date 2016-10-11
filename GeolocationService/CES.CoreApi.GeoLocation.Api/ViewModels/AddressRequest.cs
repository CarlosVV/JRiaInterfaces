
using FluentValidation;
using FluentValidation.Attributes;

namespace CES.CoreApi.GeoLocation.Api.ViewModels
{
	//[Validator(typeof(AddressRequestValidator))]
	public class AddressRequest 
    {
    
        public string Address1 { get; set; }

 
        public string Address2 { get; set; }

    
        public string Country { get; set; }

     
        public string AdministrativeArea { get; set; }

   
        public string City { get; set; }

   
        public string PostalCode { get; set; }

		public int MaxRecords { get; set; }
		public Confidence MinimumConfidence { get; set; }
	}

	//class AddressRequestValidator : AbstractValidator<AddressRequest>
	//{
	//	public AddressRequestValidator()
	//	{
	//		RuleFor(r => r.Address1).NotEmpty().WithMessage("Address1  is required");
	//		RuleFor(r => r.Country).NotEmpty().WithMessage("Country is required");
	//		RuleFor(r => r.MinimumConfidence).NotEqual(Confidence.Undefined).WithMessage("MinimumConfidence  is required");
	//	}
	//}
}
