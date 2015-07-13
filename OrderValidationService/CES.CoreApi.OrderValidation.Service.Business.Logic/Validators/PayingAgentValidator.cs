using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Enumerations;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Models;
using FluentValidation;

namespace CES.CoreApi.OrderValidation.Service.Business.Logic.Validators
{
    public class PayingAgentValidator : BaseAbstractValidator<PayingAgentValidationModel>
    {
        public PayingAgentValidator(IExceptionHelper exceptionHelper) 
            : base(exceptionHelper)
        {
            
            //Validate if paying agent is on hold
            RuleFor(p => p.IsOnHold)
                .Equal(false)
                    .WithState(p => GetCode(SubSystemError.OrderValidationPayingAgentIsOnHold))
                    .WithMessage(GetTemplate(SubSystemError.OrderValidationPayingAgentIsOnHold), p => p.PayingAgentId);

            //Validate if paying agent status is Active
            RuleFor(p => p.Status)
                .Equal(PayingAgentStatusType.Active)
                    .WithState(p => GetCode(SubSystemError.OrderValidationPayingAgentStatusInvalid))
                    .WithMessage(GetTemplate(SubSystemError.OrderValidationPayingAgentStatusInvalid), p => p.PayingAgentId, p => p.Status);

            //Validate if paying agent location is on hold
            RuleFor(p => p.IsLocationOnHold)
                .Equal(false)
                    .WithState(p => GetCode(SubSystemError.OrderValidationPayingAgentLocationOnHold))
                    .WithMessage(GetTemplate(SubSystemError.OrderValidationPayingAgentLocationOnHold), p => p.PayingAgentId);

            //Validate if paying agent location is disabled
            RuleFor(p => p.IsLocationDisabled)
                .Equal(false)
                    .WithState(p => GetCode(SubSystemError.OrderValidationPayingAgentLocationDisabled))
                    .WithMessage(GetTemplate(SubSystemError.OrderValidationPayingAgentLocationDisabled), p => p.PayingAgentId);


        }
    }
}
