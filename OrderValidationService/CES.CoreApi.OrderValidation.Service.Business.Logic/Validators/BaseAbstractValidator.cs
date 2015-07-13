using System;
using CES.CoreApi.Common.Attributes;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.Common.Tools;
using FluentValidation;
using FluentValidation.Results;

namespace CES.CoreApi.OrderValidation.Service.Business.Logic.Validators
{
    public abstract class BaseAbstractValidator<T> : AbstractValidator<T>
    {
        private readonly IExceptionHelper _exceptionHelper;

        protected BaseAbstractValidator(IExceptionHelper exceptionHelper)
        {
            if (exceptionHelper == null)
                throw new CoreApiException(TechnicalSubSystem.OrderValidationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "exceptionHelper");
            _exceptionHelper = exceptionHelper;
        }


        protected string GetMessage(SubSystemError template, params object[] parameters)
        {
            return _exceptionHelper.GenerateMessage(template, parameters);
        }

        protected string GetParameterUndefinedMessage(params object[] parameters)
        {
            return _exceptionHelper.GenerateMessage(SubSystemError.GeneralRequiredParameterIsUndefinedExtended, parameters);
        }
        
        protected string GetCode(SubSystemError error)
        {
            return _exceptionHelper.GenerateExceptionCode(TechnicalSubSystem.OrderValidationService, error);
        }

        protected string GetParameterUndefinedCode()
        {
            return GetCode(SubSystemError.GeneralRequiredParameterIsUndefinedExtended);
        }

        protected ValidationFailure GetFailure(SubSystemError template, Type validatorType, params object[] parameters)
        {
            var message = GetMessage(template, parameters);
            var exceptionCode = GetCode(template);

            return new ValidationFailure(validatorType.Name, message) { CustomState = exceptionCode };
        }

        protected ValidationFailure GetFailure(SubSystemError template, object customState, Type validatorType, params object[] parameters)
        {
            var message = GetMessage(template, parameters);

            return new ValidationFailure(validatorType.Name, message) { CustomState = customState };
        }

        protected string GetTemplate(SubSystemError error)
        {
            return error.GetAttributeValue<ErrorMessageAttribute, string>(x => x.Message);
        }
    }
}