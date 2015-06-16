using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Models;
using FluentValidation;
using FluentValidation.Results;

namespace CES.CoreApi.OrderValidation.Service.Business.Logic.Validators
{
    public class CustomerStatusValidator : AbstractValidator<CustomerStatusValidationModel>
    {
        public CustomerStatusValidator(IExceptionHelper exceptionHelper)
        {
            if (exceptionHelper == null)
                throw new CoreApiException(TechnicalSubSystem.OrderValidationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "exceptionHelper");

            RuleFor(p => p.CustomerId).GreaterThan(0);

            Custom(model =>
            {
                if (model.IsOnHold)
                {
                    var message = exceptionHelper.GenerateMessage(SubSystemError.OrderValidationCustomerIsOnHold,
                        model.CustomerId);

                    var exceptionCode = exceptionHelper.GenerateExceptionCode(
                        TechnicalSubSystem.OrderValidationService, SubSystemError.OrderValidationCustomerIsOnHold);

                    return new ValidationFailure(exceptionCode, message);
                }
                return null;
            });
        }
    }
}