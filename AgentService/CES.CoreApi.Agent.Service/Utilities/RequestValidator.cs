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

        public void Validate(GetAgentCurrencyRequest request)
        {
            ContractValidation.Requires(request != null, TechnicalSubSystem.AgentService,
                SubSystemError.GeneralRequiredParameterIsUndefined, "request");
            ContractValidation.Requires(request.AgentId > 0, TechnicalSubSystem.AgentService,
                SubSystemError.GeneralInvalidParameterValue, "request.AgentId", request.AgentId);
            ContractValidation.Requires(!string.IsNullOrEmpty(request.Currency), TechnicalSubSystem.AgentService,
                SubSystemError.GeneralRequiredParameterIsUndefined, "request.Currency", request.Currency);
            ContractValidation.Requires(request.Currency.Length == CommonConstants.StringLength.Currency, TechnicalSubSystem.AgentService,
                SubSystemError.GeneralInvalidStringParameterLength, "request.Currency", CommonConstants.StringLength.Currency, request.Currency.Length);
        }

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
            ContractValidation.Requires(request.DetalizationLevel != AgentInformationGroup.Undefined, TechnicalSubSystem.AgentService,
                SubSystemError.GeneralInvalidParameterValue, "request.DetalizationLevel", request.DetalizationLevel);
            ContractValidation.Requires((request.DetalizationLevel == AgentInformationGroup.Location ||
                                        request.DetalizationLevel == AgentInformationGroup.LocationCurrency ||
                                        request.DetalizationLevel == AgentInformationGroup.Medium ||
                                        request.DetalizationLevel == AgentInformationGroup.Full) &&
                                        request.LocationId > 0, TechnicalSubSystem.AgentService,
                SubSystemError.GeneralInvalidParameterValue, "request.LocationId", request.LocationId);
        }

        // ReSharper restore PossibleNullReferenceException
      
    }
}
