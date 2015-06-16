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
    public class SarValidator : AbstractValidator<SarValidationRequestModel>
    {
        public SarValidator(IValidationReadOnlyRepository repository, IExceptionHelper exceptionHelper)
        {
            if (repository == null)
                throw new CoreApiException(TechnicalSubSystem.OrderValidationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "repository");
            if (exceptionHelper == null)
                throw new CoreApiException(TechnicalSubSystem.OrderValidationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "exceptionHelper");

            Custom(model =>
            {
                var result = repository.ValidateSar(model);

                if (result.ResponseType != SarResponseType.None)
                {
                    var message = exceptionHelper.GenerateMessage(SubSystemError.OrderValidationSarValidationFailed,
                        model.CustomerId, model.Beneficiary.Id, model.OrderDate, model.Amount.Total);
                    var exceptionCode = exceptionHelper.GenerateExceptionCode(
                        TechnicalSubSystem.OrderValidationService, SubSystemError.OrderValidationSarValidationFailed);

                    return new ValidationFailure(exceptionCode, message)
                    {
                        CustomState = result
                    };
                }
                return null;
            });
        }
    }
}