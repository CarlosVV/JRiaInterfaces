using FluentValidation;
using FluentValidation.Attributes;
using System.Text.RegularExpressions;

namespace CES.CoreApi.BankAccount.Api.ViewModels
{
    [Validator(typeof(ServiceBankDepositRequestValidator))]
    public class ServiceBankDepositRequest
    {
        public int AppID { get; set; }
        public int AppObjectID { get; set; }
        public int UserNameID { get; set; }
        public string UserLocale { get; set; }
        public bool CheckIfValidOnly { get; set; }
        public string LocalDate { get; set; }
        public int AgentID { get; set; }
        public int AgentLocID { get; set; }
        public int BankID { get; set; }
        public int BankCountryID { get; set; }
        public string DepositCurrency { get; set; }
        public int ProviderID { get; set; }
        public int BankAccountTypeID { get; set; }
        public string BankAccountNo { get; set; }
        public string UnitaryAccountNo { get; set; }
        public string BankRoutingCode { get; set; }
        public string BIC { get; set; }
        public int BankBranchID { get; set; }
        public string BankBranchName { get; set; }
        public string BankBranchCity { get; set; }
        public string BankBranchNumber { get; set; }
    }

    class ServiceBankDepositRequestValidator : AbstractValidator<ServiceBankDepositRequest>
    {
        public ServiceBankDepositRequestValidator()
        {
            //RuleFor(r => r.AppID).GreaterThan(0).WithMessage("AppID is required");
            //RuleFor(r => r.AppObjectID).GreaterThan(0).WithMessage("AppObjectID is required");
            RuleFor(r => r.BankAccountTypeID).GreaterThan(0).WithMessage("BankAccountTypeID is required");
            RuleFor(r => r.BankAccountNo)
                .NotEmpty().WithMessage("BankAccountNo must not be empty")
                .Length(3, 100).WithMessage("BankAccountNo invalid lenght")
                .NotNull().WithMessage("BankAccountNo is required");
            RuleFor(r => r.BankCountryID).GreaterThan(0).WithMessage("BankCountryID is required");
            RuleFor(r => r.BankID).GreaterThan(0).WithMessage("BankID is required");
            RuleFor(r => r.DepositCurrency).Length(3, 3).WithMessage("DepositCurrency is required");
            RuleFor(r => r.ProviderID).GreaterThan(0).WithMessage("ProviderID is required");
        }
    }
}