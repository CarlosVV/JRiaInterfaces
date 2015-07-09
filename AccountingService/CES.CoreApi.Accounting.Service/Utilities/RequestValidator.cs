using System;
using CES.CoreApi.Accounting.Service.Contract.Models;
using CES.CoreApi.Accounting.Service.Interfaces;
using CES.CoreApi.Common.Constants;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Foundation.Validation;

namespace CES.CoreApi.Accounting.Service.Utilities
{
    public class RequestValidator : IRequestValidator
    {
        // ReSharper disable PossibleNullReferenceException

        public void Validate(GetTransactionSummaryRequest request)
        {
            ContractValidation.Requires(request != null, TechnicalSubSystem.AccountingService,
                SubSystemError.GeneralRequiredParameterIsUndefined, "request");
            ContractValidation.Requires(request.AgentId != 0, TechnicalSubSystem.AccountingService,
                SubSystemError.GeneralInvalidParameterValue, "request.AgentId", request.AgentId);
            ContractValidation.Requires(request.LocationId >= 0, TechnicalSubSystem.AccountingService,
               SubSystemError.GeneralInvalidParameterValue, "request.LocationId", request.LocationId);
            ContractValidation.Requires(string.IsNullOrEmpty(request.Currency) ||
                                        (!string.IsNullOrEmpty(request.Currency) && request.Currency.Length == CommonConstants.StringLength.Currency),
                TechnicalSubSystem.AgentService, SubSystemError.GeneralInvalidStringParameterLength, "request.Currency",
                CommonConstants.StringLength.Currency, request.Currency.Length);
            ContractValidation.Requires(request.OrderDate != default(DateTime), TechnicalSubSystem.AccountingService,
               SubSystemError.GeneralInvalidParameterValue, "request.OrderDate", request.OrderDate);
        }

        // ReSharper restore PossibleNullReferenceException
    }
}
