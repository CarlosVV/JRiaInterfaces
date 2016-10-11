using System.Runtime.Serialization;
using CES.CoreApi.Common.Models;
using FluentValidation;
using FluentValidation.Attributes;

namespace CES.CoreApi.GeoLocation.Api.ViewModels
{
	[Validator(typeof(AutocompleteAddressRequestValidator))]
	[DataContract]
	public class AutocompleteAddressRequest 
    {
        [DataMember(IsRequired = true)]
        public AddressRequest Address { get; set; }

        [DataMember]
        public int MaxRecords { get; set; }

        /// <summary>
        /// Specifying the minimum confidence required for the result.
        /// </summary>
        [DataMember(IsRequired = true)]
        public Confidence MinimumConfidence { get; set; }
    }
	class AutocompleteAddressRequestValidator : AbstractValidator<AutocompleteAddressRequest>
	{
		public AutocompleteAddressRequestValidator()
		{
			RuleFor(r => r.Address.Address1).NotEmpty().WithMessage("Address1  is required");
			RuleFor(r => r.Address.Country).NotEmpty().WithMessage("Country is required");
			RuleFor(r => r.MinimumConfidence).NotEqual(Confidence.Undefined).WithMessage("MinimumConfidence  is required");
		}
	}
}