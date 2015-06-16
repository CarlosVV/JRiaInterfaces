using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Customer.Service.Contract.Models;
using CES.CoreApi.Customer.Service.Interfaces;
using CES.CoreApi.Foundation.Validation;

namespace CES.CoreApi.Customer.Service.Utilities
{
    public class RequestValidator : IRequestValidator
    {
        // ReSharper disable PossibleNullReferenceException

        public void Validate(CustomerGetRequest request)
        {
            ContractValidation.Requires(request != null, TechnicalSubSystem.CustomerService,
                SubSystemError.GeneralRequiredParameterIsUndefined, "request");
            ContractValidation.Requires(request.CustomerId != 0, TechnicalSubSystem.CustomerService,
                SubSystemError.GeneralInvalidParameterValue, "request.CustomerId", request.CustomerId);
        }

        public void Validate(CustomerCreateRequest request)
        {
            throw new System.NotImplementedException();
        }

        // ReSharper restore PossibleNullReferenceException
    }
}
