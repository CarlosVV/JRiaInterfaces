using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Interfaces;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Models;
using FluentValidation;
using FluentValidation.Results;

namespace CES.CoreApi.OrderValidation.Service.Business.Logic.Validators
{
    public class BeneficiaryStatusValidator : AbstractValidator<BeneficiaryStatusValidationModel>
    {
        public BeneficiaryStatusValidator(IValidationReadOnlyRepository repository, IExceptionHelper exceptionHelper)
        {
            if (repository == null)
                throw new CoreApiException(TechnicalSubSystem.OrderValidationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "repository");
            if (exceptionHelper == null)
                throw new CoreApiException(TechnicalSubSystem.OrderValidationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "exceptionHelper");

            RuleFor(p => p.BeneficiaryId).GreaterThan(0);
            RuleFor(p => p.CorrespondentId).GreaterThan(0);

            Custom(model =>
            {
                if (repository.IsBeneficiaryBlocked(model))
                {
                    var message = exceptionHelper.GenerateMessage(SubSystemError.OrderValidationBeneficiaryIsBlocked,
                        model.BeneficiaryId, model.CorrespondentId);

                    var exceptionCode = exceptionHelper.GenerateExceptionCode(
                        TechnicalSubSystem.OrderValidationService, SubSystemError.OrderValidationBeneficiaryIsBlocked);

                    return new ValidationFailure(exceptionCode, message);
                }
                return null;
            });
        }
    }
}