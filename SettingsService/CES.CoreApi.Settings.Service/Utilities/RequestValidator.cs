using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Foundation.Validation;
using CES.CoreApi.Settings.Service.Contract.Models;
using CES.CoreApi.Settings.Service.Interfaces;

namespace CES.CoreApi.Settings.Service.Utilities
{
    internal class RequestValidator : IRequestValidator
    {
        // ReSharper disable PossibleNullReferenceException

        public void Validate(GetCountryRequest request)
        {
            ContractValidation.Requires(request != null, TechnicalSubSystem.SettingsService,
                SubSystemError.GeneralRequiredParameterIsUndefined, "request");
            ContractValidation.Requires(!string.IsNullOrEmpty(request.CountryAbbreviation), TechnicalSubSystem.SettingsService,
                SubSystemError.GeneralInvalidParameterValue, "request.CountryAbbreviation", request.CountryAbbreviation);
        }

        public void Validate(GetCountryListRequest request)
        {
            ContractValidation.Requires(request != null, TechnicalSubSystem.SettingsService,
                SubSystemError.GeneralRequiredParameterIsUndefined, "request");
        }

        public void Validate(GetCountrySettingsRequest request)
        {
            ContractValidation.Requires(request != null, TechnicalSubSystem.SettingsService,
                 SubSystemError.GeneralRequiredParameterIsUndefined, "request");
            ContractValidation.Requires(!string.IsNullOrEmpty(request.CountryAbbreviation), TechnicalSubSystem.SettingsService,
                SubSystemError.GeneralInvalidParameterValue, "request.CountryAbbreviation", request.CountryAbbreviation);
        }

        // ReSharper restore PossibleNullReferenceException
    }
}
