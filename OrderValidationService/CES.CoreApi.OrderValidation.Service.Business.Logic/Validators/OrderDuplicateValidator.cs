using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Interfaces;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Models;
using FluentValidation;
using FluentValidation.Results;

namespace CES.CoreApi.OrderValidation.Service.Business.Logic.Validators
{
    public class OrderDuplicateValidator : AbstractValidator<OrderDuplicateValidationRequestModel>
    {
        public OrderDuplicateValidator(IValidationMainRepository repository, IExceptionHelper exceptionHelper)
        {
            if (repository == null)
                throw new CoreApiException(TechnicalSubSystem.OrderValidationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "repository");
            if (exceptionHelper == null)
                throw new CoreApiException(TechnicalSubSystem.OrderValidationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "exceptionHelper");

            RuleFor(p => p.CustomerId).GreaterThan(0);
            RuleFor(p => p.PayAgentId).GreaterThan(0);
            RuleFor(p => p.RecAgentId).GreaterThan(0);
            RuleFor(p => p.AmountLocal).GreaterThan(0);
            RuleFor(p => p.Interval).GreaterThan(0);
            RuleFor(p => p.Currency).NotEmpty();

            Custom(model =>
            {
                if (repository.IsDuplicateOrderExist(model))
                {
                    var message = exceptionHelper.GenerateMessage(SubSystemError.OrderValidationOrderDuplicationFound,
                        model.CustomerId, model.PayAgentId, model.RecAgentLocationId, model.RecAgentId,
                        model.AmountLocal, model.Currency);

                    var exceptionCode = exceptionHelper.GenerateExceptionCode(
                        TechnicalSubSystem.OrderValidationService, SubSystemError.OrderValidationOrderDuplicationFound);

                    return new ValidationFailure(exceptionCode, message);
                }
                return null;
            });
        }
    }
}
