using CES.CoreApi.LimitVerfication.Service.Contract.Models;
using CES.CoreApi.LimitVerfication.Service.Interfaces;

namespace CES.CoreApi.LimitVerfication.Service.Utilities
{
    internal class RequestValidator : IRequestValidator
    {
        // ReSharper disable PossibleNullReferenceException

        public void Validate(CheckPayingAgentLimitsRequest request)
        {
            throw new System.NotImplementedException();
        }

        public void Validate(CheckReceivingAgentLimitsRequest request)
        {
            throw new System.NotImplementedException();
        }

        //public void Validate(GetIdentificationTypeListRequest request)
        //{
        //    //ContractValidation.Requires(request != null, TechnicalSubSystem.ReferenceDataService,
        //    //   SubSystemError.GeneralRequiredParameterIsUndefined, "request");
        //    //ContractValidation.Requires(request.LocationDepartmentId >= 0, TechnicalSubSystem.ReferenceDataService,
        //    //    SubSystemError.GeneralInvalidParameterValue, "request.LocationDepartmentId", request.LocationDepartmentId);
        //}

        //public void Validate(GetIdentificationTypeRequest request)
        //{
        //    ContractValidation.Requires(request != null, TechnicalSubSystem.ReferenceDataService,
        //       SubSystemError.GeneralRequiredParameterIsUndefined, "request");
        //    ContractValidation.Requires(request.LocationDepartmentId >= 0, TechnicalSubSystem.ReferenceDataService,
        //        SubSystemError.GeneralInvalidParameterValue, "request.LocationDepartmentId", request.LocationDepartmentId);
        //    ContractValidation.Requires(request.IdentificationTypeId >= 0, TechnicalSubSystem.ReferenceDataService,
        //        SubSystemError.GeneralInvalidParameterValue, "request.IdentificationTypeId", request.IdentificationTypeId);
        //}

        // ReSharper restore PossibleNullReferenceException
     
    }
}
