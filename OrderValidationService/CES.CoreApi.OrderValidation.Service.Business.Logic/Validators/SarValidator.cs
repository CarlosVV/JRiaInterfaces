using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Interfaces;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Enumerations;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Interfaces;
using CES.CoreApi.OrderValidation.Service.Business.Contract.Models;

namespace CES.CoreApi.OrderValidation.Service.Business.Logic.Validators
{
    public class SarValidator : BaseAbstractValidator<SarValidationRequestModel>
    {
        public SarValidator(IValidationReadOnlyRepository repository, IExceptionHelper exceptionHelper)
            : base(exceptionHelper)
        {
            if (repository == null)
                throw new CoreApiException(TechnicalSubSystem.OrderValidationService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "repository");

            Custom(model =>
            {
                var result = repository.ValidateSar(model);

                if (result.ResponseType != SarResponseType.None)
                {
                    result.ExceptionCode = GetCode(SubSystemError.OrderValidationSarValidationFailed);

                    return GetFailure(SubSystemError.OrderValidationSarValidationFailed, result, GetType(),
                        model.CustomerId, model.Beneficiary.Id, model.OrderDate, model.Amount.Total);
                }
                return null;
            });
        }
    }
}