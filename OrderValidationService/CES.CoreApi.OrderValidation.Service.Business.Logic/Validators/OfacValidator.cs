using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Enumerations;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Interfaces;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Models;
using FluentValidation;

namespace CES.CoreApi.OrderValidation.Service.Business.Logic.Validators
{
    public class OfacValidator : BaseAbstractValidator<OfacValidationRequestModel>
    {
        #region Code

        private readonly IValidationReadOnlyRepository _repository;

        public OfacValidator(IValidationReadOnlyRepository repository, IExceptionHelper exceptionHelper)
            : base(exceptionHelper)
        {
            if (repository == null)
                throw new CoreApiException(TechnicalSubSystem.OrderValidationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "repository");

            _repository = repository;

            RuleFor(p => p.CustomerId)
                .GreaterThanOrEqualTo(0)
                    .WithState(p => GetParameterUndefinedCode())
                    .WithMessage(GetParameterUndefinedMessage(GetType().Name, "CustomerId"));

            RuleFor(p => p.FirstName)
                .NotEmpty()
                    .WithState(p => GetParameterUndefinedCode())
                    .WithMessage(GetParameterUndefinedMessage(GetType().Name, "FirstName"));

            RuleFor(p => p.LastName1)
                .NotEmpty()
                    .WithState(p => GetParameterUndefinedCode())
                    .WithMessage(GetParameterUndefinedMessage(GetType().Name, "LastName1"));

            RuleFor(p => p.EntityType)
                .NotEqual(CustomerValidationEntityType.Undefined)
                    .WithState(p => GetParameterUndefinedCode())
                    .WithMessage(GetParameterUndefinedMessage(GetType().Name, "EntityType"));

            RuleFor(p => p.CustomerId)
                .Must(IsOfacMatched)
                    .WithState(p => GetCode(SubSystemError.OrderValidationOfacMatchFound))
                    .WithMessage(GetTemplate(SubSystemError.OrderValidationOfacMatchFound), p => p.EntityType,
                        p => p.CustomerId, p => p.FirstName, p => p.MiddleName, p => p.LastName1, p => p.LastName2);
        } 

        #endregion

        #region Private methods

        private bool IsOfacMatched(OfacValidationRequestModel model, int customerId)
        {
            return _repository.IsOfacWatchListMatched(model);
        } 

        #endregion
    }
}