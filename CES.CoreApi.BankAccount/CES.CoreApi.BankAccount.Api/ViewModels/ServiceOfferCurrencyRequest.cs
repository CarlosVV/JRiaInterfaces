using FluentValidation;
using FluentValidation.Attributes;
using System.Text.RegularExpressions;
/// <summary>
/// Sample View Model with self Validation (business rule): You can replace it or remove it 
/// </summary>
namespace CES.CoreApi.BankAccount.Api.ViewModels
{
    [Validator(typeof(ServiceOfferCurrencyRequestValidator))]
    public class ServiceOfferCurrencyRequest
    {
        public string CountryFrom { get; set; }
        public string CountryTo { get; set; }
    }

    class ServiceOfferCurrencyRequestValidator : AbstractValidator<ServiceOfferCurrencyRequest>
    {
        public ServiceOfferCurrencyRequestValidator()
        {
            RuleFor(r => r.CountryFrom).NotEmpty().WithMessage("Country from is required");
            RuleFor(r => r.CountryTo).NotEmpty().WithMessage("Country to is required");
            RuleFor(r => r.CountryFrom).Must(OnlyCharacter).WithMessage("Please specify a valid country from");
            RuleFor(r => r.CountryTo).Must(OnlyCharacter).WithMessage("Please specify a valid country from");

        }
        private bool OnlyCharacter(string value)
        {
            if (string.IsNullOrEmpty(value))/*No need to validate if it is empty*/
                return true;
            if (value.Length != 2)
                return false;

            return Regex.IsMatch(value, @"^[a-zA-Z]+$");
        }

    }
}