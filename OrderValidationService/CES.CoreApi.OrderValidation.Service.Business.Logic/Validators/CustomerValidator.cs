using System;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Interfaces;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Models;
using CES.CoreApi.OrderValidation.Service.Business.Logic.Constants;
using FluentValidation;

namespace CES.CoreApi.OrderValidation.Service.Business.Logic.Validators
{
    public class CustomerValidator : BaseAbstractValidator<CustomerValidationModel>
    {
        #region Core

        private const string UsCode = "US";
        private const string VietnamCode = "VN";
        private const string ArizonaCode = "AZ";
        private const string OklahomaCode = "OK";
        private const decimal LowerLimit = 1000.00m;
        private const decimal UpperLimit = 3000.00m;

        public CustomerValidator(IExceptionHelper exceptionHelper, ICountryCodeValidator countryCodeValidator)
            : base(exceptionHelper)
        {
            if (countryCodeValidator == null)
                throw new CoreApiException(TechnicalSubSystem.OrderValidationService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "countryCodeValidator");
            
            //Validate CustomerId > 0
            RuleFor(p => p.CustomerId)
                .GreaterThan(0)
                .WithState(p => GetParameterUndefinedCode())
                .WithMessage(GetParameterUndefinedMessage(GetType().Name, "CustomerId"));

            //Validate FirstName is populated and not exceed allowed length
            RuleFor(p => p.FirstName)
                .NotEmpty()
                    .WithState(p => GetCode(SubSystemError.OrderValidationCustomerFirstNameIsRequired))
                    .WithMessage(GetTemplate(SubSystemError.OrderValidationCustomerFirstNameIsRequired))
                .Length(CommonConstants.StringLength.FirstName)
                    .WithState(s => GetCode(SubSystemError.OrderValidationCustomerFirstNameTooLong))
                    .WithMessage(GetTemplate(SubSystemError.OrderValidationCustomerFirstNameTooLong), CommonConstants.StringLength.FirstName);

            //Validate LastName1 is populated and not exceed allowed length
            RuleFor(p => p.LastName1)
                .NotEmpty()
                    .WithState(p => GetCode(SubSystemError.OrderValidationCustomerLastName1IsRequired))
                    .WithMessage(GetTemplate(SubSystemError.OrderValidationCustomerLastName1IsRequired))
                .Length(CommonConstants.StringLength.LastName1)
                    .WithState(s => GetCode(SubSystemError.OrderValidationCustomerLastName1TooLong))
                    .WithMessage(GetTemplate(SubSystemError.OrderValidationCustomerLastName1TooLong), CommonConstants.StringLength.LastName1);

            //Validate Country against: not empty, correct length and existence in database
            RuleFor(p => p.Country)
                .NotEmpty()
                    .WithState(p => GetCode(SubSystemError.OrderValidationCustomerCountryIsRequired))
                    .WithMessage(GetTemplate(SubSystemError.OrderValidationCustomerCountryIsRequired))
                .Length(CommonConstants.StringLength.CountryCode)
                    .WithState(p => GetCode(SubSystemError.OrderValidationCustomerCountryCodeIsEmptyOrInvalid))
                    .WithMessage(GetTemplate(SubSystemError.OrderValidationCustomerCountryCodeIsEmptyOrInvalid), CommonConstants.StringLength.CountryCode)
                .Must(countryCodeValidator.IsCountryCodeValid)
                    .WithState(p => GetCode(SubSystemError.OrderValidationCustomerFinanceCountryFromNotFound))
                    .WithMessage(GetTemplate(SubSystemError.OrderValidationCustomerFinanceCountryFromNotFound), p => p.Country);

            //Validate Address against: not empty and maximum length allowed
            RuleFor(p => p.Address)
                .NotEmpty()
                    .WithState(p => GetCode(SubSystemError.OrderValidationCustomerAddressIsRequired))
                    .WithMessage(GetTemplate(SubSystemError.OrderValidationCustomerAddressIsRequired))
                .Length(CommonConstants.StringLength.Address)
                    .WithState(p => GetCode(SubSystemError.OrderValidationCustomerAddressTooLong))
                    .WithMessage(GetTemplate(SubSystemError.OrderValidationCustomerAddressTooLong), CommonConstants.StringLength.Address);

            //Validate TaxCountry [if it is not empty] against: correct length and existence in database
            RuleFor(p => p.TaxCountry)
                .Length(CommonConstants.StringLength.CountryCode)
                    .When(p => string.IsNullOrEmpty(p.TaxCountry))
                        .WithState(p => GetCode(SubSystemError.OrderValidationCustomerTaxCountryCodeIsInvalid))
                        .WithMessage(GetTemplate(SubSystemError.OrderValidationCustomerTaxCountryCodeIsInvalid), CommonConstants.StringLength.CountryCode)
                .Must(countryCodeValidator.IsCountryCodeValid)
                    .When(p => string.IsNullOrEmpty(p.TaxCountry))
                        .WithState(p => GetCode(SubSystemError.OrderValidationCustomerTaxCountryNotFound))
                        .WithMessage(GetTemplate(SubSystemError.OrderValidationCustomerTaxCountryNotFound), p => p.TaxCountry);

            //Validate customer OnHold status
            RuleFor(p => p.IsOnHold)
                .NotEqual(true)
                    .WithState(p => GetCode(SubSystemError.OrderValidationCustomerIsOnHold))
                    .WithMessage(GetTemplate(SubSystemError.OrderValidationCustomerIsOnHold), p => p.CustomerId);

            //Validate CustomerAmount greater than minimum
            RuleFor(p => p.CustomerAmount)
                .GreaterThanOrEqualTo(1)
                    .WithState(p => GetCode(SubSystemError.OrderValidationFinanceCustomerAmountEmptyOrBelowMin))
                    .WithMessage(GetTemplate(SubSystemError.OrderValidationFinanceCustomerAmountEmptyOrBelowMin), p => p.CustomerAmount);

            //Validate if order is over limit and ID is required
            Custom(
                model => !IsCustomerIdDefined(model) &&
                         !(IsSendingFromUsToVn(model) &&
                           IsGrandTotalWithinRange(model) &&
                           !IsReceivingStateAzOrOk(model))
                    ? GetFailure(SubSystemError.OrderValidationOrderOverLimitRequireId, GetType())
                    : null);

            //Validate customer ID image
            When(p => IsIdRequired(p) &&
                      !p.IsIdentificationImageExempt &&
                      p.RequireScannedId,
                () => RuleFor(p => p.ImageId)
                    .GreaterThan(0)
                        .WithState(p => GetCode(SubSystemError.OrderValidationCustomerIdImageNotFound))
                        .WithMessage(GetTemplate(SubSystemError.OrderValidationCustomerIdImageNotFound)));
        } 

        #endregion

        #region Private methods

        private static bool IsIdRequired(CustomerValidationModel model)
        {
            return model.OrderIdAmount1Setting > 0 || model.LocalAmountUsd < model.OrderIdAmount1Setting;
        }

        private static bool IsCustomerIdDefined(CustomerValidationModel model)
        {
            return !(model.CustomerIdType == -1 || string.IsNullOrEmpty(model.CustomerIdNumber));
        }

        private static bool IsSendingFromUsToVn(CustomerValidationModel model)
        {
            return model.ReceivingAgentCountry.Equals(UsCode, StringComparison.OrdinalIgnoreCase) &&
                   model.PayingAgentCountry.Equals(VietnamCode, StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsReceivingStateAzOrOk(CustomerValidationModel model)
        {
            return model.ReceivingAgentState.Equals(ArizonaCode, StringComparison.OrdinalIgnoreCase) ||
                   model.ReceivingAgentState.Equals(OklahomaCode, StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsGrandTotalWithinRange(CustomerValidationModel model)
        {
            return model.GrandTotal >= LowerLimit && model.GrandTotal < UpperLimit;
        } 

        #endregion
    }
}