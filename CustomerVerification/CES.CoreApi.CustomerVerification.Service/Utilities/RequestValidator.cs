using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.CustomerVerification.Service.Contract.Models;
using CES.CoreApi.CustomerVerification.Service.Interfaces;
using CES.CoreApi.Foundation.Validation;

namespace CES.CoreApi.CustomerVerification.Service.Utilities
{
    public class RequestValidator : IRequestValidator
    {
        // ReSharper disable PossibleNullReferenceException

        public void Validate(VerifyCustomerIdentityRequest request)
        {
            ContractValidation.Requires(request != null, TechnicalSubSystem.CustomerVerificationService,
                SubSystemError.GeneralRequiredParameterIsUndefined, "request");
            //    ContractValidation.Requires(request.CustomerId != 0, TechnicalSubSystem.CustomerService,
            //        SubSystemError.GeneralInvalidParameterValue, "request.CustomerId", request.CustomerId);
        }

      

        //public void Validate(CustomerProcessSignatureRequest request)
        //{
        //    ContractValidation.Requires(request != null, TechnicalSubSystem.CustomerService,
        //        SubSystemError.GeneralRequiredParameterIsUndefined, "request");
        //    ContractValidation.Requires(request.OrderId > 0, TechnicalSubSystem.CustomerService,
        //        SubSystemError.GeneralInvalidParameterValue, "request.OrderId", request.OrderId);
        //    ContractValidation.Requires(request.Signature != null, TechnicalSubSystem.CustomerService,
        //        SubSystemError.GeneralRequiredParameterIsUndefined, "request.Signature", request.Signature);
        //    ContractValidation.Requires(request.Signature.Length > 0, TechnicalSubSystem.CustomerService,
        //        SubSystemError.GeneralInvalidParameterValue, "request.Signature", request.Signature);
        //}

        // ReSharper restore PossibleNullReferenceException
    }
}
