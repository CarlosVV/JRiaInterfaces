using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Foundation.Validation;
using CES.CoreApi.OrderProcess.Service.Contract.Models;
using CES.CoreApi.OrderProcess.Service.Interfaces;

namespace CES.CoreApi.OrderProcess.Service.Utilities
{
    internal class RequestValidator : IRequestValidator
    {
        // ReSharper disable PossibleNullReferenceException

        public void Validate(OrderGetRequest request)
        {
            ContractValidation.Requires(request != null, TechnicalSubSystem.OrderProcessService,
                SubSystemError.GeneralRequiredParameterIsUndefined, "request");
            ContractValidation.Requires(request.OrderId > 0, TechnicalSubSystem.OrderProcessService,
                SubSystemError.GeneralInvalidParameterValue, "request.OrderId", request.OrderId);
        }
        
        // ReSharper restore PossibleNullReferenceException
    }
}
