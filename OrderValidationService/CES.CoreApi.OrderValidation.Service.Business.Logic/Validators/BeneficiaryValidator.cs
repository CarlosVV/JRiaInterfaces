using System;
using System.Collections.Generic;
using System.Linq;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Interfaces;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Models;
using CES.CoreApi.OrderValidation.Service.Business.Logic.Constants;
using CES.CoreApi.ReferenceData.Service.Contract.Interfaces;
using CES.CoreApi.ReferenceData.Service.Contract.Models;
using FluentValidation;

namespace CES.CoreApi.OrderValidation.Service.Business.Logic.Validators
{
    public class BeneficiaryValidator : BaseAbstractValidator<BeneficiaryValidationModel>
    {
        #region Core

        private readonly IServiceHelper _serviceHelper;
        private readonly IValidationReadOnlyRepository _repository;
        private readonly List<string> _cedulaIdRequiredList = new List<string> { "CPF", "CNPJ" };

        public BeneficiaryValidator(IExceptionHelper exceptionHelper, IServiceHelper serviceHelper, ICountryCodeValidator countryCodeValidator, IValidationReadOnlyRepository repository)
            : base(exceptionHelper)
        {
            if (serviceHelper == null)
                throw new CoreApiException(TechnicalSubSystem.OrderValidationService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "serviceHelper");
            if (countryCodeValidator == null)
                throw new CoreApiException(TechnicalSubSystem.OrderValidationService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "countryCodeValidator");
            if (repository == null)
                throw new CoreApiException(TechnicalSubSystem.OrderValidationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "repository");

            _serviceHelper = serviceHelper;
            _repository = repository;

            //Validate FirstName
            RuleFor(p => p.FirstName)
                .NotEmpty()
                    .WithState(s => GetCode(SubSystemError.OrderValidationBeneficiaryFirstNameIsRequired))
                    .WithMessage(GetTemplate(SubSystemError.OrderValidationBeneficiaryFirstNameIsRequired))
                .Length(CommonConstants.StringLength.FirstName)
                    .WithState(s => GetCode(SubSystemError.OrderValidationBeneficiaryFirstNameTooLong))
                    .WithMessage(GetTemplate(SubSystemError.OrderValidationBeneficiaryFirstNameTooLong), CommonConstants.StringLength.FirstName);

            //Validate LastName1
            RuleFor(p => p.LastName1)
                .NotEmpty()
                    .WithState(s => GetCode(SubSystemError.OrderValidationBeneficiaryLastName1IsRequired))
                    .WithMessage(GetTemplate(SubSystemError.OrderValidationBeneficiaryLastName1IsRequired))
                .Length(CommonConstants.StringLength.LastName1)
                    .WithState(s => GetCode(SubSystemError.OrderValidationBeneficiaryLastName1TooLong))
                    .WithMessage(GetTemplate(SubSystemError.OrderValidationBeneficiaryLastName1TooLong), CommonConstants.StringLength.LastName1);

            //Validate Country Code length and existence in database
            RuleFor(p => p.Country)
                .NotEmpty()
                    .WithState(s => GetCode(SubSystemError.OrderValidationBeneficiaryCountryIsRequired))
                    .WithMessage(GetTemplate(SubSystemError.OrderValidationBeneficiaryCountryIsRequired))
                .Length(CommonConstants.StringLength.CountryCode)
                    .WithState(s => GetCode(SubSystemError.OrderValidationBeneficiaryCountryIsInvalid))
                    .WithMessage(GetTemplate(SubSystemError.OrderValidationBeneficiaryCountryIsInvalid), CommonConstants.StringLength.CountryCode)
                .Must(countryCodeValidator.IsCountryCodeValid)
                    .WithState(s => GetCode(SubSystemError.OrderValidationBeneficiaryFinanceCountryToNotFound))
                    .WithMessage(GetTemplate(SubSystemError.OrderValidationBeneficiaryFinanceCountryToNotFound), p => p.Country);

            //Validate BeneficiaryId > 0 and blocked status
            RuleFor(p => p.BeneficiaryId)
               .GreaterThan(0)
                   .WithState(s => GetParameterUndefinedCode())
                   .WithMessage(GetParameterUndefinedMessage(GetType().Name, "BeneficiaryId"))
               .Must(IsBeneficiaryBlocked)
                   .WithState(s => GetCode(SubSystemError.OrderValidationBeneficiaryIsBlocked))
                   .WithMessage(GetTemplate(SubSystemError.OrderValidationBeneficiaryIsBlocked), p => p.BeneficiaryId, p => p.CorrespondentId);

            //Validate CorrespondentId > 0
            RuleFor(p => p.CorrespondentId)
                .GreaterThan(0)
                    .WithState(s => GetParameterUndefinedCode())
                    .WithMessage(GetParameterUndefinedMessage(GetType().Name, "CorrespondentId"));

            //Validate IdentificationNumber is not empty
            RuleFor(p => p.IdentificationNumber)
                .NotEmpty()
                    .WithState(s => GetCode(SubSystemError.OrderValidationBeneficiaryIdentificationNumberIsRequired))
                    .WithMessage(GetTemplate(SubSystemError.OrderValidationBeneficiaryIdentificationNumberIsRequired));

            //Validate IdentificationType.Name is correct and if Cedula Id is required
            RuleFor(p => p.IdentificationTypeId)
                .Must(IsIdentificationTypeCorrect)
                    .WithState(s => GetCode(SubSystemError.OrderValidationBeneficiaryCedulaIdIsRequired))
                    .WithMessage(GetTemplate(SubSystemError.OrderValidationBeneficiaryCedulaIdIsRequired));
        }
        
        #endregion

        #region Private methods

        private bool IsBeneficiaryBlocked(BeneficiaryValidationModel model, int beneficiaryId)
        {
            return _repository.IsBeneficiaryBlocked(beneficiaryId, model.CorrespondentId);
        }

        private bool IsIdentificationTypeCorrect(int identificationTypeId)
        {
            var request = new GetIdentificationTypeRequest { LocationDepartmentId = 0, IdentificationTypeId = identificationTypeId };
            var response = _serviceHelper.Execute<IIdentificationTypeService, GetIdentificationTypeResponse>(s => s.Get(request));

            return !(response.IdentificationType == null ||
                   _cedulaIdRequiredList.FirstOrDefault(p => p.Equals(response.IdentificationType.Name, StringComparison.OrdinalIgnoreCase)) == null);
        } 

        #endregion
    }
}
