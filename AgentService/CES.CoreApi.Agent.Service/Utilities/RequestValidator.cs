using CES.CoreApi.Agent.Service.Contract.Enumerations;
using CES.CoreApi.Agent.Service.Contract.Models;
using CES.CoreApi.Agent.Service.Interfaces;
using CES.CoreApi.Common.Constants;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Foundation.Validation;

namespace CES.CoreApi.Agent.Service.Utilities
{
    public class RequestValidator : IRequestValidator
    {
        // ReSharper disable PossibleNullReferenceException

        public void Validate(ProcessSignatureRequest request)
        {
            ContractValidation.Requires(request != null, TechnicalSubSystem.AgentService,
                 SubSystemError.GeneralRequiredParameterIsUndefined, "request");
            ContractValidation.Requires(request.AgentId > 0, TechnicalSubSystem.AgentService,
                SubSystemError.GeneralInvalidParameterValue, "request.AgentId", request.AgentId);
            ContractValidation.Requires(request.LocationId > 0, TechnicalSubSystem.AgentService,
                SubSystemError.GeneralInvalidParameterValue, "request.LocationId", request.LocationId);
            ContractValidation.Requires(request.UserId > 0, TechnicalSubSystem.AgentService,
                SubSystemError.GeneralInvalidParameterValue, "request.UserId", request.UserId);
            ContractValidation.Requires(request.Signature != null, TechnicalSubSystem.AgentService,
                SubSystemError.GeneralRequiredParameterIsUndefined, "request.Signature", request.Signature);
            ContractValidation.Requires(request.Signature.Length > 0, TechnicalSubSystem.AgentService,
                SubSystemError.GeneralInvalidParameterValue, "request.Signature", request.Signature);
        }

        public void Validate(GetPayingAgentRequest request)
        {
            ContractValidation.Requires(request != null, TechnicalSubSystem.AgentService,
                 SubSystemError.GeneralRequiredParameterIsUndefined, "request");
            ContractValidation.Requires(request.AgentId > 0, TechnicalSubSystem.AgentService,
                SubSystemError.GeneralInvalidParameterValue, "request.AgentId", request.AgentId);
            ContractValidation.Requires(request.DetalizationLevel != PayingAgentInformationGroup.Undefined, TechnicalSubSystem.AgentService,
                SubSystemError.GeneralInvalidParameterValue, "request.DetalizationLevel", request.DetalizationLevel);

            ContractValidation.Requires(!IsLocationIdRequired(request.DetalizationLevel) || request.LocationId > 0,
                TechnicalSubSystem.AgentService, SubSystemError.GeneralInvalidParameterValue, "request.LocationId",
                request.LocationId);

            var isCurrencyRequired = IsCurrencyRequired(request.DetalizationLevel);

            ContractValidation.Requires(!isCurrencyRequired || !string.IsNullOrEmpty(request.Currency), 
                TechnicalSubSystem.AgentService, SubSystemError.GeneralInvalidParameterValue, "request.Currency", request.Currency);

            ContractValidation.Requires(!isCurrencyRequired || request.Currency.Length == CommonConstants.StringLength.Currency,
                TechnicalSubSystem.AgentService, SubSystemError.GeneralInvalidStringParameterLength, "request.Currency",
                CommonConstants.StringLength.Currency, request.Currency.Length);
        }

        public void Validate(GetReceivingAgentRequest request)
        {
            ContractValidation.Requires(request != null, TechnicalSubSystem.AgentService,
                 SubSystemError.GeneralRequiredParameterIsUndefined, "request");
            ContractValidation.Requires(request.AgentId > 0, TechnicalSubSystem.AgentService,
                SubSystemError.GeneralInvalidParameterValue, "request.AgentId", request.AgentId);
            ContractValidation.Requires(request.DetalizationLevel != ReceivingAgentInformationGroup.Undefined, TechnicalSubSystem.AgentService,
                SubSystemError.GeneralInvalidParameterValue, "request.DetalizationLevel", request.DetalizationLevel);
        }

        // ReSharper restore PossibleNullReferenceException

        private static bool IsLocationIdRequired(PayingAgentInformationGroup detalizationLevel)
        {
            return (detalizationLevel & PayingAgentInformationGroup.Location) == PayingAgentInformationGroup.Location ||
                   (detalizationLevel & PayingAgentInformationGroup.LocationWithCurrency) == PayingAgentInformationGroup.LocationWithCurrency;
        }

        private static bool IsCurrencyRequired(PayingAgentInformationGroup detalizationLevel)
        {
            return (detalizationLevel & PayingAgentInformationGroup.AgentCurrency) == PayingAgentInformationGroup.AgentCurrency ||
                   (detalizationLevel & PayingAgentInformationGroup.LocationWithCurrency) == PayingAgentInformationGroup.LocationWithCurrency;
        }
    }
}
