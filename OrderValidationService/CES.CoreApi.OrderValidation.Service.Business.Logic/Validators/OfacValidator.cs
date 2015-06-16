using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Enumerations;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Interfaces;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Models;
using FluentValidation;
using FluentValidation.Results;

namespace CES.CoreApi.OrderValidation.Service.Business.Logic.Validators
{
    public class OfacValidator : AbstractValidator<OfacValidationRequestModel>
    {
        public OfacValidator(IValidationReadOnlyRepository repository, IExceptionHelper exceptionHelper)
        {
            if (repository == null)
                throw new CoreApiException(TechnicalSubSystem.OrderValidationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "repository");
            if (exceptionHelper == null)
                throw new CoreApiException(TechnicalSubSystem.OrderValidationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "exceptionHelper");

            RuleFor(p => p.CustomerId).GreaterThanOrEqualTo(0);
            RuleFor(p => p.FirstName).NotEmpty();
            RuleFor(p => p.LastName1).NotEmpty();
            RuleFor(p => p.EntityType).NotEqual(CustomerValidationEntityType.Undefined);

            Custom(model =>
            {
                if (repository.IsOfacWatchListMatched(model))
                {
                    var message = exceptionHelper.GenerateMessage(SubSystemError.OrderValidationOfacMatchFound,
                        model.EntityType, model.CustomerId, model.FirstName, model.MiddleName, model.LastName1, model.LastName2);
                    var exceptionCode = exceptionHelper.GenerateExceptionCode(
                        TechnicalSubSystem.OrderValidationService, SubSystemError.OrderValidationOfacMatchFound);

                    return new ValidationFailure(exceptionCode, message);
                }
                return null;
            });
        }
    }
}