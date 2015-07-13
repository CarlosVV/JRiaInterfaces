using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Foundation.Validation;
using CES.CoreApi.ReferenceData.Service.Contract.Models;
using CES.CoreApi.ReferenceData.Service.Interfaces;

namespace CES.CoreApi.ReferenceData.Service.Utilities
{
    internal class RequestValidator : IRequestValidator
    {
        // ReSharper disable PossibleNullReferenceException

        public void Validate(GetIdentificationTypeListRequest request)
        {
            ContractValidation.Requires(request != null, TechnicalSubSystem.ReferenceDataService,
               SubSystemError.GeneralRequiredParameterIsUndefined, "request");
            ContractValidation.Requires(request.LocationDepartmentId >= 0, TechnicalSubSystem.ReferenceDataService,
                SubSystemError.GeneralInvalidParameterValue, "request.LocationDepartmentId", request.LocationDepartmentId);
        }

        public void Validate(GetIdentificationTypeRequest request)
        {
            ContractValidation.Requires(request != null, TechnicalSubSystem.ReferenceDataService,
               SubSystemError.GeneralRequiredParameterIsUndefined, "request");
            ContractValidation.Requires(request.LocationDepartmentId >= 0, TechnicalSubSystem.ReferenceDataService,
                SubSystemError.GeneralInvalidParameterValue, "request.LocationDepartmentId", request.LocationDepartmentId);
            ContractValidation.Requires(request.IdentificationTypeId >= 0, TechnicalSubSystem.ReferenceDataService,
                SubSystemError.GeneralInvalidParameterValue, "request.IdentificationTypeId", request.IdentificationTypeId);
        }

        // ReSharper restore PossibleNullReferenceException
    }
}
