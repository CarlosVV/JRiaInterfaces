using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Interfaces;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Models;
using FluentValidation;

namespace CES.CoreApi.OrderValidation.Service.Business.Logic.Validators
{
    public class OrderDuplicateValidator : BaseAbstractValidator<OrderDuplicateValidationRequestModel>
    {
        #region Core

        private readonly IValidationMainRepository _repository;

        public OrderDuplicateValidator(IValidationMainRepository repository, IExceptionHelper exceptionHelper)
            : base(exceptionHelper)
        {
            if (repository == null)
                throw new CoreApiException(TechnicalSubSystem.OrderValidationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "repository");

            _repository = repository;

            RuleFor(p => p.CustomerId)
                .GreaterThan(0)
                    .WithState(p => GetParameterUndefinedCode())
                    .WithMessage(GetParameterUndefinedMessage(GetType().Name, "CustomerId"));

            RuleFor(p => p.PayAgentId)
                .GreaterThan(0)
                    .WithState(p => GetParameterUndefinedCode())
                    .WithMessage(GetParameterUndefinedMessage(GetType().Name, "PayAgentId"));

            RuleFor(p => p.ReceivingAgentId)
                .GreaterThan(0)
                    .WithState(p => GetParameterUndefinedCode())
                    .WithMessage(GetParameterUndefinedMessage(GetType().Name, "ReceivingAgentId"));

            RuleFor(p => p.AmountLocal)
                .GreaterThan(0)
                    .WithState(p => GetParameterUndefinedCode())
                    .WithMessage(GetParameterUndefinedMessage(GetType().Name, "AmountLocal"));

            RuleFor(p => p.Interval)
                .GreaterThan(0)
                    .WithState(p => GetParameterUndefinedCode())
                    .WithMessage(GetParameterUndefinedMessage(GetType().Name, "Interval"));

            RuleFor(p => p.Currency)
                .NotEmpty()
                    .WithState(p => GetParameterUndefinedCode())
                    .WithMessage(GetParameterUndefinedMessage(GetType().Name, "Currency"));

            RuleFor(p => p.CustomerId)
                .Must(IsDuplicateOrderExist)
                    .WithState(p => GetCode(SubSystemError.OrderValidationOrderDuplicationFound))
                    .WithMessage(GetTemplate(SubSystemError.OrderValidationOrderDuplicationFound), p => p.CustomerId,
                        p => p.PayAgentId, p => p.ReceivingAgentLocationId, p => p.ReceivingAgentId, p => p.AmountLocal,
                        p => p.Currency);
        } 

        #endregion

        #region Private methods

        private bool IsDuplicateOrderExist(OrderDuplicateValidationRequestModel model, int customerId)
        {
            return _repository.IsDuplicateOrderExist(model);
        } 

        #endregion
    }
}
