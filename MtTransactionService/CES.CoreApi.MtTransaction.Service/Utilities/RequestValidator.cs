using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Foundation.Validation;
using CES.CoreApi.MtTransaction.Service.Contract.Enumerations;
using CES.CoreApi.MtTransaction.Service.Contract.Models;
using CES.CoreApi.MtTransaction.Service.Interfaces;

namespace CES.CoreApi.MtTransaction.Service.Utilities
{
    internal class RequestValidator : IRequestValidator
    {
        // ReSharper disable PossibleNullReferenceException

        public void Validate(MtTransactionGetRequest request)
        {
            ContractValidation.Requires(request != null, TechnicalSubSystem.MtTransactionService,
                SubSystemError.GeneralRequiredParameterIsUndefined, "request");
            ContractValidation.Requires(request.TransactionId > 0, TechnicalSubSystem.MtTransactionService,
                SubSystemError.GeneralInvalidParameterValue, "request.TransactionId", request.TransactionId);
            ContractValidation.Requires(request.DetalizationLevel != TransactionInformationGroup.Undefined, TechnicalSubSystem.MtTransactionService,
                SubSystemError.GeneralInvalidParameterValue, "request.DetalizationLevel", request.DetalizationLevel);
        }

        public void Validate(MtTransactionCreateRequest request)
        {
            
        }

        // ReSharper restore PossibleNullReferenceException
    }
}
