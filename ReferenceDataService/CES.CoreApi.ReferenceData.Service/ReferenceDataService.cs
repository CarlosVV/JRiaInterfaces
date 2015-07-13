using System.Collections.Generic;
using System.ServiceModel;
using CES.CoreApi.Common.Enumerations;
using CES.CoreApi.Common.Exceptions;
using CES.CoreApi.Common.Models;
using CES.CoreApi.Foundation.Contract.Interfaces;
using CES.CoreApi.ReferenceData.Service.Business.Contract.Interfaces;
using CES.CoreApi.ReferenceData.Service.Business.Contract.Models;
using CES.CoreApi.ReferenceData.Service.Contract.Constants;
using CES.CoreApi.ReferenceData.Service.Contract.Interfaces;
using CES.CoreApi.ReferenceData.Service.Contract.Models;
using CES.CoreApi.ReferenceData.Service.Interfaces;

namespace CES.CoreApi.ReferenceData.Service
{
    [ServiceBehavior(Namespace = Namespaces.ReferenceDataServiceContractNamespace, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ReferenceDataService : IIdentificationTypeService, IHealthMonitoringService
    {
        #region Core

        private readonly IReferenceDataProcessor _referenceDataProcessor;
        private readonly IHealthMonitoringProcessor _healthMonitoringProcessor;
        private readonly IRequestValidator _requestValidator;
        private readonly IMappingHelper _mapper;

        public ReferenceDataService(IReferenceDataProcessor referenceDataProcessor, IHealthMonitoringProcessor healthMonitoringProcessor,
            IRequestValidator requestValidator, IMappingHelper mapper)
        {
            if (referenceDataProcessor == null)
                throw new CoreApiException(TechnicalSubSystem.ReferenceDataService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "countrySettingsProcessor");
            if (healthMonitoringProcessor == null)
                throw new CoreApiException(TechnicalSubSystem.ReferenceDataService,
                   SubSystemError.GeneralRequiredParameterIsUndefined, "healthMonitoringProcessor");
            if (requestValidator == null)
                throw new CoreApiException(TechnicalSubSystem.ReferenceDataService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "requestValidator");
            if (mapper == null)
                throw new CoreApiException(TechnicalSubSystem.ReferenceDataService,
                    SubSystemError.GeneralRequiredParameterIsUndefined, "mapper");

            _referenceDataProcessor = referenceDataProcessor;
            _healthMonitoringProcessor = healthMonitoringProcessor;
            _requestValidator = requestValidator;
            _mapper = mapper;
        }

        #endregion

        #region IIdentificationTypeService implementation

        public GetIdentificationTypeListResponse GetList(GetIdentificationTypeListRequest request)
        {
            _requestValidator.Validate(request);
            var responseModel =  _referenceDataProcessor.GetIdentificationTypeList(request.LocationDepartmentId);
            return _mapper.ConvertToResponse<IEnumerable<IdentificationTypeModel>, GetIdentificationTypeListResponse>(responseModel);
        }

        public GetIdentificationTypeResponse Get(GetIdentificationTypeRequest request)
        {
            _requestValidator.Validate(request);
            var responseModel = _referenceDataProcessor.GetIdentificationType(request.IdentificationTypeId, request.LocationDepartmentId);
            return _mapper.ConvertToResponse<IdentificationTypeModel, GetIdentificationTypeResponse>(responseModel);
        }

        #endregion

        #region IHealthMonitoringService implementation
        
        public ClearCacheResponse ClearCache()
        {
            var responseModel = _healthMonitoringProcessor.ClearCache();
            return _mapper.ConvertToResponse<ClearCacheResponseModel, ClearCacheResponse>(responseModel);
        }

        public PingResponse Ping()
        {
            var responseModel = _healthMonitoringProcessor.Ping();
            return _mapper.ConvertToResponse<PingResponseModel, PingResponse>(responseModel);
        } 

        #endregion
    }
}
